using NHibernate;

namespace com.etak.core.repository.NHibernate
{
    /// <summary>
    /// Abstraction class from NHibernate ITransaction to CRM IPersistanceTransaction
    /// </summary>
    internal class TransactionToPersistanceTransaction : IPersistanceTransaction
    {
        private ITransaction _undelayingTrx;
        private ISession _session;

        public TransactionToPersistanceTransaction(ITransaction trx)
        {
            _undelayingTrx = trx;
        }

        public TransactionToPersistanceTransaction(ITransaction iTransaction, ISession _session)
        {
            this._undelayingTrx = iTransaction;
            this._session = _session;
        }

        public void Commit()
        {
            _undelayingTrx.Commit();
        }

        public void Rollback()
        {
            _undelayingTrx.Rollback();
            _session.Clear();
        }

        public void Dispose()
        {
            if (!_undelayingTrx.WasCommitted && !_undelayingTrx.WasRolledBack)
            {
                Rollback();
            }
            else
            {
                _undelayingTrx.Dispose();
            }
        }
    }
}