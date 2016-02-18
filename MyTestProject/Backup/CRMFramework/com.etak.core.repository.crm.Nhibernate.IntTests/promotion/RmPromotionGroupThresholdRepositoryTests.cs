using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.promotion;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.promotion
{
    [TestFixture]
    public class RmPromotionGroupThresholdRepositoryTests
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreateThresholdTest()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<IRmPromotionGroupThresholdRepository<RmPromotionGroupThreshold>>();
                var groupRepo = RepositoryManager.GetRepository<IRmPromotionGroupInfoRepository<RmPromotionGroupInfo>>();
                var firstGroup = groupRepo.GetAll().FirstOrDefault();
                using (var trx = conn.BeginTransaction())
                {
                    repo.Create(new RmPromotionGroupThreshold() {
                        PromotionGroup = firstGroup,
                        Direction = ThresholdDirection.U,
                        ThresholdType = ThresholdType.P,
                        ThresholdValue = 1001.1M,
                        GenerateEventId = 1                    
                    });
                    trx.Commit();
                }
                

            }
        }


        [Test]
        public void GetThresholdTest()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<IRmPromotionGroupThresholdRepository<RmPromotionGroupThreshold>>();
                using (var trx = conn.BeginTransaction())
                {
                    var firstThreshold = repo.GetById(1020000001);
                    Console.Out.WriteLine(firstThreshold.PromotionGroup.GroupName);
                    Console.Out.WriteLine(firstThreshold.ThresholdValue);
                }


            }
        }

    }
}
