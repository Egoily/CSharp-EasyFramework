using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.reveneu
{
    [TestFixture]
    public class IBillCycleRepositoryTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void QueryBillRun()
        {
            Int32 BillRunId = 1010000001;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                try
                {
                    var prodRepo = RepositoryManager.GetRepository<IBillCycleRepository<BillCycle>>();
                    BillCycle billCycle1 = prodRepo.GetById(BillRunId);
                    Assert.IsNotNull(billCycle1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }              
            }

            using (var conn = RepositoryManager.GetNewConnection())
            {
                try
                {
                    //this second query should not hit the DB as it is already cached. 
                    var prodRepo = RepositoryManager.GetRepository<IBillCycleRepository<BillCycle>>();
                    BillCycle billCycle1 = prodRepo.GetById(BillRunId);
                    //Assert.IsNotNull(billCycle1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [Test]
        public void CreateBillCycle()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    Int32 dealerId = 170000;

                    IDealerInfoRepository<DealerInfo> dealerRepo = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
                    DealerInfo dealer = dealerRepo.GetByDealerIdAndCache(dealerId).FirstOrDefault();

                    MultiLingualDescription DescriptionForProduct = new MultiLingualDescription
                    {
                        DefaultMessage = "this is a test product",

                    };
                    DescriptionForProduct.Texts = new List<LanguageSpecificText>
                    {
                        { new LanguageSpecificText { Description = DescriptionForProduct, Language = ISO639LanguageCodes.aar, Text = "aar sample text 2" }},
                        { new LanguageSpecificText { Description = DescriptionForProduct, Language = ISO639LanguageCodes.zza, Text = "zza sample text 2" }}
                    };

                    var repo = RepositoryManager.GetRepository<IBillCycleRepository<BillCycle>>();
                    BillCycle billCycle = new BillCycle()
                    {
                        VMNO    = dealer,
                        CutOffDay = 5,
                        DaysUntilLate = 1,
                        Description = DescriptionForProduct,
                        PeriodQuantity = 1,
                        PeriodUnit = TimeUnits.Hour,
                    };

                    try
                    {
                        repo.Create(billCycle);
                        trx.Commit();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }                   
                }
            }
        }
    }
}
