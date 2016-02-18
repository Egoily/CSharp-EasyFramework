using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.repository;
using com.etak.core.repository.crm.configuration;
using FrontEndServiceContract;

namespace com.etak.core.microservices.helper
{ 
    /// <summary>
    /// HPS Connector
    /// </summary>
    public abstract class HPSConnector
    {
        /// <summary>
        /// Configuration value to get the HPS subscriber
        /// </summary>
        private const string UseHPSTestUrl = "UseHPSTestUrl";
        private const string HPS_SubscriberServiceUrl = "HPS_SubscriberServiceUrl";
        private const string Test_HPS_SubscriberServiceUrl = "Test_HPS_SubscriberServiceUrl";

        /// <summary>
        /// Get Provision Service Interface
        /// </summary>
        public virtual IProvisionService GetProvisioningInterface()
        {
            string useHPSTestUrlFlag = ConfigurationManager.AppSettings.Get(UseHPSTestUrl);
            string configName = (!String.IsNullOrWhiteSpace(useHPSTestUrlFlag) && useHPSTestUrlFlag.ToUpper() == "TRUE")
                                                ? Test_HPS_SubscriberServiceUrl : HPS_SubscriberServiceUrl;
            string hpsUrl = GetHPSUrlConfigFromDatabase(configName);
            return HPS.WCFConnector.Factory.GetProvisionProxy(hpsUrl);
        }

        /// <summary>
        /// Get HPS Url Config From Database
        /// </summary>
        /// <param name="configName">Config Name</param>
        /// <returns>HPS Url</returns>
        private static string GetHPSUrlConfigFromDatabase(string configName)
        {
            var systemConfigRepo = RepositoryManager.GetRepository<ISystemConfigDataInfoRepository<SystemConfigDataInfo>>();
            var configs = systemConfigRepo.GetSystemConfigDateInfoByItem(configName);
            if (configs != null && configs.Any())
                return configs.First().Value;
            return null;
        }
    }
}
