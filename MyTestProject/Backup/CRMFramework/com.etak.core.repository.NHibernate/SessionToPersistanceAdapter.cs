using System;
using System.Data;
using NHibernate;

namespace com.etak.core.repository.NHibernate
{
    /// <summary>
    /// Implementes IPersistanceConnection using and internal NHibernate session.
    /// The purpose of this layer, is be able to abstract CRM code from NHibernate.
    /// </summary>
    internal class SessionToPersistanceAdapter : IPersistanceConnection
    {
        ISession _session;
        ClearSessionForThread _notifier;
     
        public SessionToPersistanceAdapter(ISession iSession)
        {
            this._session = iSession;
        }
        
        public void Close()
        {
            _session.Close();
        }

        public void Dispose()
        {
            _notifier.Invoke();
            _session.Dispose();
        }

        public ISession GetUndelayingSession()
        {
            return (_session);
        }

        public IPersistanceTransaction BeginTransaction()
        {           
            return (new TransactionToPersistanceTransaction(_session.BeginTransaction(), _session));
        }

        public IPersistanceTransaction BeginTransaction(IsolationLevel level)
        {            
            return (new TransactionToPersistanceTransaction(_session.BeginTransaction(level), _session));
        }

        public void SetNotifier(ClearSessionForThread notifier)
        {
            if (notifier == null)
                throw new ArgumentNullException("notifier");

            _notifier = notifier;
        }       
    }
}
