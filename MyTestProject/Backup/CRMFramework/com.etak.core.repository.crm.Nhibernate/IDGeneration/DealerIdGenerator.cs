using System.Runtime.CompilerServices;
using com.etak.core.model;
using NHibernate.Engine.Transaction;
using NHibernate.Id;

namespace com.etak.core.repository.crm.Nhibernate.IDGeneration
{
    /// <summary>
    /// NHibernate id generator for Dealer entity
    /// JL: I don't have a clue why we need this, it was lincoln code that i'm optimizing.
    /// </summary>
    public class DealerIdGenerator : IIdentifierGenerator
    {
        /// <summary>
        /// Generate a new identifier
        /// </summary>
        /// <param name="session">The <see cref="T:NHibernate.Engine.ISessionImplementor"/> this id is being generated in.</param>
        /// <param name="obj">The entity for which the id is being generated.</param>
        /// <returns>
        /// The new identifier
        /// </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public object Generate(global::NHibernate.Engine.ISessionImplementor session, object obj)
        {
            DealerInfo info = (DealerInfo)obj;
            int dealerId;
            switch (info.DealerTypeID)
            {
                case 1:
                    if (info.DealerID != null)
                    {
                        dealerId = (int)info.DealerID;
                    }
                    else
                    {
                        GetNextVMNO dbGetter = new GetNextVMNO();
                        Isolater.DoNonTransactedWork(dbGetter, session);
                        dealerId = dbGetter.MaxMVNO;
                    }
                    break;
                default:
                    GetNextIdForVMNO dbGetterVMNO = new GetNextIdForVMNO(info.FiscalUnitID.Value);
                    Isolater.DoNonTransactedWork(dbGetterVMNO, session);
                    dealerId = dbGetterVMNO.NextId;
                    break;
            }

            return dealerId;
        }
    }
}
