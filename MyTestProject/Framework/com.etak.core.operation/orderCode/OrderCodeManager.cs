using System;
using System.Collections.Generic;
using System.Threading;
using com.etak.core.model;
using com.etak.core.repository;
using com.etak.core.repository.crm.configuration;

namespace com.etak.core.operation.orderCode
{
    /// <summary>
    /// Class to manage the OrderCodes generated.
    /// </summary>
    public class OrderCodeManager
    {
        private static SpinLock _sequenceManagerLock = new SpinLock();
        private static readonly IDictionary<Int32, ISequenceManager> _mvnoTosequenceManagerMapper = new Dictionary<Int32, ISequenceManager>();

        /// <summary>
        /// Gets New Sequence
        /// </summary>
        /// <param name="vmnoId">The VMNO to get the next sequence for</param>
        /// <returns>the generated order code</returns>
        public static int GenerateNextOrderCode(int vmnoId)
        {
            ISequenceManager manager = GetSequenceManagerForVmno(vmnoId);
            return manager.GetNextSequence();
        }

        /// <summary>
        /// Gets the secuence manager for that VMNO
        /// </summary>
        /// <param name="vmno"></param>
        /// <returns>the sequence manager</returns>
        private static ISequenceManager GetSequenceManagerForVmno(Int32 vmno)
        {
            ISequenceManager manager;
            Boolean lockTaken = false;
            try
            {
                _sequenceManagerLock.Enter(ref lockTaken);
                //If the sequence manager does not know the VMNOID, create a new one.
                if (!_mvnoTosequenceManagerMapper.TryGetValue(vmno, out manager))
                {
                    //Get the sequence name for this VMNO
                    ISystemConfigDataInfoRepository<SystemConfigDataInfo> repo = RepositoryManager.GetRepository<ISystemConfigDataInfoRepository<SystemConfigDataInfo>>();
                    String sequenceConfigKey = String.Format("OrderCode_Sequence_Name_{0}", vmno);
                    SystemConfigDataInfo vmnoSequenceConfig = repo.GetById(sequenceConfigKey);

                    if (vmnoSequenceConfig == null)
                        throw new Exception(String.Format("Unable to find sequence name for VMNO:{0}, No row in SystemConfigDataInfo for ITEM:{1}", vmno, sequenceConfigKey));

                    String sequenceNameForVmno = vmnoSequenceConfig.Value;

                    CachedSequenceManager sequecenManager = new CachedSequenceManager();
                    sequecenManager.SetSequenceName(sequenceNameForVmno);
                    manager = sequecenManager;

                    //add the sequence manager to the dictonary
                    _mvnoTosequenceManagerMapper.Add(vmno, manager);
                }
            }
            finally
            {
                if (lockTaken) _sequenceManagerLock.Exit();
            }

            return (manager);
        }
    }
}
