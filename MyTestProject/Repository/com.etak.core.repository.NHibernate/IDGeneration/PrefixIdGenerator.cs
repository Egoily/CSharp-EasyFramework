using System;
using System.Configuration;
using System.Globalization;
using System.Runtime.CompilerServices;
using NHibernate.Engine;
using NHibernate.Engine.Transaction;
using NHibernate.Id;
using NHibernate.Metadata;
using NHibernate.Persister.Entity;
using NHibernate.Type;

namespace com.etak.core.repository.NHibernate.IDGeneration
{
    /// <summary>
    /// NHibernate Identifier generator, based on a given prefix configured in app.config settings
    /// </summary>
    public class PrefixIdGenerator : IIdentifierGenerator
    {
        private Int64 _lastId = -1;
        private IType _idColumnType;

        /// <summary>
        /// Generates an Id for the given prefix configured in the DB.
        /// </summary>
        /// <param name="session">the session requesting a new Id for obj</param>
        /// <param name="obj">the object that needs and Id</param>
        /// <returns>the id generated</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public object Generate(ISessionImplementor session, object obj)
        {
            //this is the initialization of the prefix, this ontly run the
            //first time the ID is requested for EACH entity.
            if (_lastId == -1)
            {
                String prefixConfig = ConfigurationManager.AppSettings["dbprefix"];
                if (prefixConfig == null)
                {
                    throw new ConfigurationErrorsException("Could not find dbprefix in app settings");
                }

                //Get the session factory
                IClassMetadata metaData = session.Factory.GetClassMetadata(obj.GetType());

                // Gets the entity's persister
                AbstractEntityPersister persister = (AbstractEntityPersister)metaData;

                //we need to keep track of the underlying type to know the cast we need to do.
                _idColumnType = persister.IdentifierType;

                //get the table and column information
                String idColumnName = persister.IdentifierColumnNames[0];
                String tableName = persister.TableName;

                //Calculate what are the min or max values for the given prefix
                Int64 minValue;
                Int64 maxValue;
                Int32 strMaxLen;

                if (_idColumnType is Int32Type)
                    strMaxLen = Int32.MaxValue.ToString(CultureInfo.InvariantCulture).Length;
                else if (_idColumnType is Int64Type)
                    strMaxLen = Int64.MaxValue.ToString(CultureInfo.InvariantCulture).Length;
                else
                    throw new ConfigurationErrorsException(String.Format("The Type of the Id:{0} for etity:{1} is not supported", _idColumnType.GetType(), persister.EntityName));

                string strMinValue = new string('0', strMaxLen);
                string strMaxValue = new string('9', strMaxLen);

                strMinValue = prefixConfig + strMinValue.Substring(prefixConfig.Length);
                strMaxValue = prefixConfig + strMaxValue.Substring(prefixConfig.Length);

                try
                {
                    minValue = Int64.Parse(strMinValue);
                    maxValue = Int64.Parse(strMaxValue);
                }
                catch (Exception ex)
                {
                    throw new ConfigurationErrorsException(
                        String.Format("The dbprefix {0} generates min or max Id that excceds tha type range",
                            prefixConfig), ex);
                }

                //Execute the query to get the max
                MaxInRangeGetter getter = new MaxInRangeGetter(tableName, idColumnName, minValue, maxValue);
                Isolater.DoNonTransactedWork(getter, session);
                _lastId = getter.LastId;
            }

            _lastId++;

            if (_idColumnType is Int32Type)
                return (_lastId.ToInt32());

            return (_lastId);
        }
    }
}