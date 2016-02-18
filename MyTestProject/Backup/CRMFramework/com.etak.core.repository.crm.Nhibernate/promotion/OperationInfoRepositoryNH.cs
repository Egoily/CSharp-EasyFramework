
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for OperationInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TOperationInfo">the type of entity managed, is or extends OperationInfo</typeparam>
    public class OperationInfoRepositoryNH<TOperationInfo> : NHibernateRepository<TOperationInfo, long>,
       IOperationInfoRepository<TOperationInfo> where TOperationInfo : OperationInfo
    { 

        
    }
}
