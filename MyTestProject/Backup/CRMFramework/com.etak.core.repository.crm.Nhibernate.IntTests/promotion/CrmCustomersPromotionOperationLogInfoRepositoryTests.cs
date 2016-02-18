using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.promotion;
using com.etak.core.repository.NHibernate;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.promotion
{
    [TestFixture]
    public class CrmCustomersPromotionOperationLogInfoRepositoryTests
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;

        }

        [Test]
        public void CreateLogTest()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<ICrmCustomersPromotionOperationLogInfoRepository<CrmCustomersPromotionOperationLogInfo>>();
                
                using (var trx = conn.BeginTransaction())
                {
                    repo.Create(new CrmCustomersPromotionOperationLogInfo()
                    {
                        OPERATIONDATE = System.DateTime.Now
                    });
                    trx.Commit();
                }
            }
        }

        [Test]
        public void BulkCreateLogTest()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var bulkRepo = new NHibernateBulkCopyRepository<CrmCustomersPromotionOperationLogInfo, Int64>();

                var listToInsert = new List<CrmCustomersPromotionOperationLogInfo>()
                {
                    new CrmCustomersPromotionOperationLogInfo(){ OPERATIONDATE = System.DateTime.Now},
                    new CrmCustomersPromotionOperationLogInfo(){ OPERATIONDATE = System.DateTime.Now},
                    new CrmCustomersPromotionOperationLogInfo(){ OPERATIONDATE = System.DateTime.Now},
                };
                

                using (var trx = conn.BeginTransaction())
                {
                    try
                    {
                        bulkRepo.BulkCreate(listToInsert);
                        trx.Commit();
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("debug point");
                        throw;
                    }
                }
            }
        }
    }
}
