using System;
using System.Data;
using NHibernate.Engine.Transaction;

namespace com.etak.core.repository.crm.Nhibernate.IDGeneration
{
    class GetNextIdForVMNO : IIsolatedWork
    {
        private Int32 MVNOID;
        public Int32 NextId;

        public GetNextIdForVMNO(Int32 MVNOID)
        {
            this.MVNOID = MVNOID;
        }

        public void DoWork(IDbConnection connection, IDbTransaction transaction)
        {
            IDbCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = connection;
            string sqlSelectCommand = string.Format("select MAX(DEALERID) from CRM_DEALERS where FiscalUnitID = {0}",
                MVNOID);
            sqlCommand.CommandText = sqlSelectCommand;

            int j = 0;
            int.TryParse(sqlCommand.ExecuteScalar().ToString(), out j);

            if (j != 0)
            {
                j = j + 1;
            }
            else
            {
                j = MVNOID + 1;
            }

            NextId = j;
        }
    }
}
