using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for ReportResourceInfo
    /// </summary>
    /// <typeparam name="TReportResourceInfo">The type of the entity managed by the repository, is or extends ReportResourceInfo</typeparam>
    public interface IReportResourceInfoRepository<TReportResourceInfo> : IRepository<TReportResourceInfo, Int64>
            where TReportResourceInfo : ReportResourceInfo
    {
      
    }
}
