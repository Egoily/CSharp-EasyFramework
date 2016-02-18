using System;
using System.Data;
using com.etak.core.repository.NHibernate;
using NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Sequence provider based on SqlServer native sequences.
    /// </summary>
    class SqlServerNativeSequenceProvider : NHibernateRepository<Object, Int32>, ISequenceProvider
    {
        /// <summary>
        /// Gets the next number for the given sequence
        /// </summary>
        /// <param name="sequence">the name of the sequence from which next value will be gathered</param>
        /// <returns>the next number in the sequence</returns>
        public int GetNextSequence(String sequence)
        {
            Int32 NextSequence = -1;
            using (IDbCommand c = _session.Connection.CreateCommand())
            {
                String SQL = String.Format("SELECT next value FOR {0}", sequence);
                ISQLQuery query = _session.CreateSQLQuery(SQL);             
                Object result = query.UniqueResult();
                NextSequence = (Int32)result;
            }
            return (NextSequence);
        }

        /// <summary>
        /// Gets the increment size on the storage engine
        /// </summary>
        /// <param name="sequence">the name of the sequence</param>
        /// <returns>the size of the increment in the storage engine</returns>
        public int GetStepSize(String sequence)
        {

            Int32 SeqStepSize = -1;
            using (IDbCommand c = _session.Connection.CreateCommand())
            {
                String SQL = "SELECT CAST( INCREMENT AS int ) as INCREMENT FROM SYS.SEQUENCES with (nolock) where name = :SeqName";
                ISQLQuery query = _session.CreateSQLQuery(SQL);
                query.SetString("SeqName", sequence);
                Object result = query.UniqueResult();
                SeqStepSize = (Int32)result;               
            }
            return (SeqStepSize);
        }
    }
}
