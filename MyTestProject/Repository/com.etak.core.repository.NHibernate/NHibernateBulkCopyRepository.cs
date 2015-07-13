using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Impl;
using NHibernate.Metadata;
using NHibernate.Persister.Entity;
using NHibernate.Tuple;
using NHibernate.Tuple.Entity;
using nType = NHibernate.Type;

namespace com.etak.core.repository.NHibernate
{
    /// <summary>
    /// Repository that extends NHibernate repository, and implements BulkRepository. It reads the
    /// configuration of the schema from NHibernate but implements the operation with BulkCopy API
    /// SQL server operation, this repository only works with MS SQL Server
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that this repository manages</typeparam>
    /// <typeparam name="TKey">The type of the key of the entity, tipically Int64 or Int32</typeparam>
    public class NHibernateBulkCopyRepository<TEntity, TKey> :
        NHibernateRepository<TEntity, TKey>,
        IBulkRepository<TEntity, TKey> where TEntity : class , new()
    {
        /// <summary>
        /// Session obtained in the constructor via the repository manger
        /// </summary>
        protected IClassMetadata metaData;

        /// <summary>
        /// The NHibernate persister
        /// </summary>
        protected AbstractEntityPersister persister;

        /// <summary>
        /// The underlaying NHibernate session factory
        /// </summary>
        protected ISessionFactory sessionFactory;

        /// <summary>
        /// The type of the entity for this bulk repository
        /// </summary>
        protected Type entityType;

        /// <summary>
        /// The real table name where (that is fixed in case of synonim table)
        /// </summary>
        protected String TableName;

        /// <summary>
        /// constructor, gets the session from the repository manager.
        /// </summary>
        public NHibernateBulkCopyRepository()
        {
            SessionToPersistanceAdapter session = RepositoryManager.GetConnection() as SessionToPersistanceAdapter;

            entityType = typeof(TEntity);

            _session = session.GetUndelayingSession();

            //Get the session factory
            sessionFactory = _session.SessionFactory;

            // Get the entity's NHibernate metadata
            metaData = sessionFactory.GetClassMetadata(entityType.ToString());

            // Gets the entity's persister
            persister = (AbstractEntityPersister)metaData;
        }

        /// <summary>
        /// Resolves the real table name in case it's a synonim
        /// </summary>
        /// <returns></returns>
        private String GetFullTableName()
        {
            String configuredTableName = persister.TableName;
            String synonymName = GetSynonymRealTable(persister.TableName);
            String tableName = synonymName ?? configuredTableName;
            return (tableName);
        }

        /// <summary>
        /// Get the real table from the dynamic view of synonims
        /// </summary>
        /// <param name="tableName">the table name to resolve</param>
        /// <returns>the table name with full namespace</returns>
        private String GetSynonymRealTable(String tableName)
        {
            return _session.CreateSQLQuery("SELECT	base_object_name FROM sys.synonyms WITH (NOLOCK) WHERE name = ?")
                 .AddScalar("base_object_name", NHibernateUtil.String)
                 .SetString(0, tableName)
                 .FutureValue<String>().Value;
        }

        #region IBulkRepository<TEntity,TKey> Members

        /// <summary>
        /// Creates a list of entities with bulk copy
        /// </summary>
        /// <param name="entities">the entities to be created</param>
        /// <returns>the created entities with the Generated ID</returns>
        public IEnumerable<TEntity> BulkCreate(IEnumerable<TEntity> entities)
        {
            IList<TEntity> newList = new List<TEntity>();
            SqlBulkCopy bulkCopy = GetBulkCopy();
            //Generate the IDs using Nhibernate session.
            foreach (TEntity entity in entities)
            {
                Object id = persister.IdentifierGenerator.Generate(_session.GetSessionImplementation(), entity);
                persister.SetIdentifier(entity, id, EntityMode.Poco);
                newList.Add(entity);
            }

            DataTable table = GetDataRowsFromEntitiesAndAddMaping(newList, bulkCopy, null);
            bulkCopy.WriteToServer(table);
            return (entities);
        }

        /// <summary>
        /// Updates some fields of a set of entities
        /// </summary>
        /// <param name="entities">the list of enties to be updated</param>
        /// <param name="path">
        /// an array of lambda expression to get the fields that needs to be updated
        /// </param>
        /// <returns>the updated entities</returns>
        public IEnumerable<TEntity> BulkUpdate(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>>[] path)
        {
            SqlBulkCopy bulkCopy = GetBulkCopy();
            DataTable table = GetDataRowsFromEntitiesAndAddMaping(entities, bulkCopy, path);
            bulkCopy.BulkUpdate(table);
            return (entities);
        }

        /// <summary>
        /// Deletes the entities from the DB in bulk way
        /// </summary>
        /// <param name="entities">the entities to be deleted</param>
        /// <returns>The list of deleted entities</returns>
        public IEnumerable<TEntity> BulkDelete(IEnumerable<TEntity> entities)
        {
            SqlBulkCopy bulkCopy = GetBulkCopy();
            //TODO: this needs to be fixed and update only the ID column.
            DataTable Table = GetDataRowsFromEntitiesAndAddMaping(entities, bulkCopy, null);
            bulkCopy.BulkDelete(Table);
            return (entities);
        }

        #endregion

        private SqlBulkCopy GetBulkCopy()
        {
            if (!_session.Transaction.IsActive)
                throw new Exception("Can't start a bulk copy without a transaction");

            SqlTransaction trx = GetTransaction(_session);
            SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)_session.Connection, SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers, trx);

            TableName = GetFullTableName();
            bulkCopy.DestinationTableName = TableName;
            bulkCopy.BatchSize = 5000;
            return (bulkCopy);
        }

        private SqlTransaction GetTransaction(ISession session)
        {
            using (System.Data.IDbCommand command = session.Connection.CreateCommand())
            {
                session.Transaction.Enlist(command);
                return command.Transaction as SqlTransaction;
            }
        }

        /// <summary>
        /// Transforms a set of entities to a DataTable, it also fills the mapping in the bulk copy object
        /// </summary>
        /// <param name="entities">the list of entities to be transformed</param>
        /// <param name="blkCopy">the bulk copy with the mappings to be added</param>
        /// <param name="paths">
        /// the columns that needs to be added to the data table as lambda expressions
        /// </param>
        /// <returns>the DataTable with the transformed entities</returns>
        private DataTable GetDataRowsFromEntitiesAndAddMaping(IEnumerable<TEntity> entities, SqlBulkCopy blkCopy, Expression<Func<TEntity, object>>[] paths)
        {
            EntityMetamodel model = persister.EntityMetamodel;

            //create the data table, first the column definition, using Nhibernate configuration and maping to map it.
            DataTable dTformat = new DataTable(TableName);
            dTformat.TableName = TableName;

            //Fill table name and primary keys of the table, also the column containing the primary key
            String idColumname = persister.IdentifierColumnNames.First();
            DataColumn pkey = new DataColumn(idColumname, model.IdentifierProperty.Type.ReturnedClass);
            dTformat.Columns.Add(pkey);
            dTformat.PrimaryKey = new DataColumn[] { pkey };
            blkCopy.ColumnMappings.Add(idColumname, idColumname);

            //FilterProperties in case a reduced list is provided
            List<KeyValuePair<StandardProperty, Int32>> modelProperties = new List<KeyValuePair<StandardProperty, Int32>>();

            int idx = 0;
            foreach (var prop in model.Properties)
            {
                if (!prop.Type.IsCollectionType && !(prop.Type is nType.OneToOneType))
                {
                    if (paths != null && paths.Any())
                    {
                        if (paths.Any(x => ExpressionProcessor.FindPropertyExpression(x.Body) == prop.Name))
                        {
                            modelProperties.Add(new KeyValuePair<StandardProperty, Int32>(prop, idx));
                        }
                    }
                    else
                    {
                        modelProperties.Add(new KeyValuePair<StandardProperty, Int32>(prop, idx));
                    }
                }
                idx++;
            }

            //create the columns for the datatable
            foreach (var propertyPair in modelProperties)
            {
                StandardProperty property = propertyPair.Key;

                String propertyName = property.Name;
                String propertyColumnName = persister.GetPropertyColumnNames(propertyName).FirstOrDefault();
                Type propertyColumnType = property.Type.ReturnedClass;

                if (property.Type.IsEntityType && property.Type.IsAssociationType && property.IsInsertable)
                {
                    IClassMetadata metaDataAsoc = sessionFactory.GetClassMetadata(propertyColumnType);
                    propertyColumnType = metaDataAsoc.IdentifierType.ReturnedClass;
                }

                blkCopy.ColumnMappings.Add(propertyColumnName, propertyColumnName);
                DataColumn column = new DataColumn(propertyColumnName, propertyColumnType);
                dTformat.Columns.Add(column);
            }

            //fill the rows with the collection
            foreach (TEntity entity in entities)
            {
                DataRow row = dTformat.NewRow();
                //Get the IDs to fill it.
                Object[] values = persister.GetPropertyValuesToInsert(entity, null, _session.GetSessionImplementation());
                Object PrimaryKey = persister.GetIdentifier(entity, EntityMode.Poco);
                row[idColumname] = PrimaryKey;

                foreach (var propertyPair in modelProperties)
                {
                    Int32 i = propertyPair.Value;
                    StandardProperty property = propertyPair.Key;
                    Object Value = values[i];
                    String ColumnName = persister.GetPropertyColumnNames(i).First();

                    //if value is null, it's no need to get identitier for entity.
                    if (Value != null && property.Type.IsEntityType && property.Type.IsAssociationType && property.IsInsertable)
                    {
                        Value = sessionFactory.GetClassMetadata(property.Type.ReturnedClass).GetIdentifier(Value, EntityMode.Poco);
                    }
                    row[ColumnName] = Value ?? DBNull.Value;
                }
                dTformat.Rows.Add(row);
            }
            dTformat.AcceptChanges();

            return (dTformat);
        }
    }
}