using System;
using System.Data;
using NHibernate.Engine.Transaction;

namespace com.etak.core.repository.crm.Nhibernate.IDGeneration
{
// ReSharper disable once InconsistentNaming
    class GetNextVMNO : IIsolatedWork
    {
        public Int32 MaxMVNO { get; set; }

        public void DoWork(IDbConnection connection, IDbTransaction transaction)
        {
            IDbCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = "select MAX(DEALERID) from CRM_DEALERS where DEALERTYPEID = 1 and ParentID = -1";


            int j;
            int.TryParse(sqlCommand.ExecuteScalar().ToString(), out j);

            if (j != 0)
            {
                j = j + 10000;
            }
            MaxMVNO = j;
        }
    }
}
