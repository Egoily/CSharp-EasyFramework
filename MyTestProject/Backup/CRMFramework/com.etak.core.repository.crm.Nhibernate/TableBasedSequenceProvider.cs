using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;
using com.etak.core.repository.NHibernate;
using NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Table based sequence provider (where Native Sequences are not generated)
    /// </summary>
    public class TableBasedSequenceProvider : NHibernateRepository<Object, Int32>, ISequenceProvider
    {

        /// <summary>
        /// key:MVNOID, value:END
        /// </summary>
        private static IDictionary<int, long> mvnoSequenceDic;

        /// <summary>
        /// default constructor, creates the Dictionary for sequences
        /// </summary>
        public TableBasedSequenceProvider()
        {
            if (mvnoSequenceDic == null)
            {
                mvnoSequenceDic = new ConcurrentDictionary<int, long>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence">sequence format for mvno order code:CRM_MVNO_OPERATION_LOG.ORDERCODE:[MVNOID], eg: CRM_MVNO_OPERATION_LOG.ORDERCODE:700000</param>
        /// <returns></returns>
        public int GetNextSequence(String sequence)
        {
            if (string.IsNullOrEmpty(sequence))
                throw new ArgumentException("invalid Sequence.");

            var arguList = sequence.Split(':');

            if (arguList.Length != 2)
                throw new ArgumentException(string.Format("{0} is not supported on TableBasedSequenceProvider at this momment", sequence));

            var dealerId = Convert.ToInt32(arguList[1]);
            string pkName = arguList[0];

            int NextSequence = 0;
            using (IDbCommand c = _session.Connection.CreateCommand())
            {
                if(!mvnoSequenceDic.ContainsKey(dealerId))
                {
                    StringBuilder getEndSeqbuilder = new StringBuilder();
                    getEndSeqbuilder.AppendLine("DECLARE @END BIGINT");
                    getEndSeqbuilder.AppendLine("SELECT @END=[END] FROM CRM_MVNO_SEQUENCE WHERE DEALERID={0} AND PKNAME='{1}'");
                    getEndSeqbuilder.AppendLine("IF @@ROWCOUNT= 0 ");
                    getEndSeqbuilder.AppendLine("BEGIN");
                    getEndSeqbuilder.AppendLine("INSERT INTO CRM_MVNO_SEQUENCE(DEALERID,PKNAME,START,[END],CURRENTSEQ) VALUES ({0},'{1}',1000,{2},1000)");
                    getEndSeqbuilder.AppendLine("END SELECT @END");
                    ISQLQuery getEndQuery = _session.CreateSQLQuery(string.Format(getEndSeqbuilder.ToString(), dealerId, pkName, Int32.MaxValue));
                    Object end = getEndQuery.UniqueResult();
                    mvnoSequenceDic.Add(dealerId, System.Convert.ToInt32(end));
                }

                StringBuilder builder = new StringBuilder();
                builder.Append("DECLARE @TEMP TABLE(CURRENTSEQ BIGINT) ");
                builder.Append("DECLARE @NEXT BIGINT ");
                builder.AppendFormat("UPDATE CRM_MVNO_SEQUENCE WITH (ROWLOCK) SET CURRENTSEQ=CURRENTSEQ+1 OUTPUT INSERTED.CURRENTSEQ INTO @TEMP WHERE DEALERID={0} AND PKNAME='{1}' ", dealerId, pkName);
                builder.Append("SET @NEXT=(SELECT TOP 1 CURRENTSEQ FROM @TEMP  ORDER BY CURRENTSEQ DESC) SELECT @NEXT ");
                ISQLQuery query = _session.CreateSQLQuery(builder.ToString());
                Object result = query.UniqueResult();
                NextSequence = System.Convert.ToInt32(result);

                if (NextSequence == mvnoSequenceDic[dealerId])
                {
                    //Need to reset current seq
                    StringBuilder resetbuilder = new StringBuilder();
                    resetbuilder.AppendFormat(" UPDATE CRM_MVNO_SEQUENCE WITH (ROWLOCK) SET CURRENTSEQ=1000 WHERE DEALERID={0} AND PKNAME='{1}' ", dealerId, pkName);
                    ISQLQuery resetQuery = _session.CreateSQLQuery(builder.ToString());
                    resetQuery.ExecuteUpdate();                    
                }
            }

            return NextSequence;
        }
       
        /// <summary>
        /// Current table based sequence provider does not support caching
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public int GetStepSize(String sequence)
        {
            return 1;
        }
    }
}
