using System;
using System.Threading;
using com.etak.core.repository;
using com.etak.core.repository.crm;

namespace com.etak.core.operation.orderCode
{
    class CachedSequenceManager : ISequenceManager
    {
        private Object _Updatelock = new Object();
        private Int32 _SequenceCacheSize = Int32.MinValue;
        private CachedSequence _currentCache = null;
        private String _sequenceName = "";

        public int GetNextSequence()
        {
            if (_currentCache == null)
            {
                _currentCache = CacheNextSequence();
            }
            if (_currentCache.IsExhausted)
            {
                _currentCache = CacheNextSequence();
            }

            return (_currentCache.Next);
        }

        public void SetSequenceName(String sequenceName)
        {
            _sequenceName = sequenceName;
        }

        private CachedSequence CacheNextSequence()
        {
            lock (_Updatelock)
            {
                ISequenceProvider provider = RepositoryManager.GetRepository<ISequenceProvider>();
                if (_SequenceCacheSize == Int32.MinValue)
                    _SequenceCacheSize = provider.GetStepSize(_sequenceName);

                CachedSequence newSec = new CachedSequence();
                newSec.Min = provider.GetNextSequence(_sequenceName);
                newSec.Current = newSec.Min;
                newSec.Max = newSec.Min + _SequenceCacheSize;
                return (newSec);
            }
        }
    }

    class CachedSequence
    {
        private SpinLock _cacheAccessLock = new SpinLock();

        internal Int32 Min { get; set; }
        internal Int32 Max { get; set; }
        internal Int32 Current { get; set; }

        public bool IsExhausted
        {

            get
            {
                Boolean exhausted;
                bool lockTaken = false;
                try
                {
                    _cacheAccessLock.Enter(ref lockTaken);
                    exhausted = (Current == Max);
                }
                finally
                {
                    if (lockTaken) _cacheAccessLock.Exit(false);
                }
                return exhausted;
            }
        }

        public Int32 Next
        {
            get
            {
                Int32 Value;
                bool lockTaken = false;
                try
                {
                    _cacheAccessLock.Enter(ref lockTaken);
                    Value = Current++;
                }
                finally
                {
                    if (lockTaken) _cacheAccessLock.Exit(false);
                }
                return Value;
            }
        }
    }
}
