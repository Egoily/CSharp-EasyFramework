using System;
using System.Data;
using NHibernate;
using NHibernate.Engine.Transaction;
using NHibernate.SqlCommand;

namespace com.etak.core.repository.NHibernate.IDGeneration
{
    class MaxInRangeGetter : IIsolatedWork
    {
        private String tableName;
        private String idColumnName;
        private Int64 minValue;
        private Int64 maxValue;

        public Int64 LastId { get; private set; }

        public MaxInRangeGetter(String tableName, String idColumnName, Int64 minValue, Int64 maxValue)
        {
            this.tableName = tableName;
            this.idColumnName = idColumnName;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public void DoWork(IDbConnection connection, IDbTransaction transaction)
        {
           
            String SelectQuery = String.Format("Select MAX({0}) from {1} where {0}>{2} and {0}<{3}", idColumnName,
                tableName, minValue, maxValue);
            SqlString sql = SqlString.Parse(SelectQuery);


            try
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = SelectQuery;
                cmd.CommandType = CommandType.Text;
                Object result = cmd.ExecuteScalar();

                if (result is DBNull)
                    LastId = minValue;
                else
                    LastId = result.ToInt64();
            }
            catch (Exception ex)
            {
                throw new QueryException(String.Format("Unable to execute query {0} to get the Id", SelectQuery), ex);
            }
        }
    }
}
