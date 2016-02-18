using System;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for ReportResourceInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TReportResourceInfo">the type of entity managed, is or extends ReportResourceInfo</typeparam>
    public class ReportResourceInfoRepositoryNH<TReportResourceInfo> : NHibernateRepository<TReportResourceInfo, Int64>,
        IReportResourceInfoRepository<TReportResourceInfo> where TReportResourceInfo : ReportResourceInfo
    {
    }
}
