using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using NUnit.Framework;
using com.etak.core.model.inventory;
using System.Collections.Generic;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.device
{
    [TestFixture]
    public class DeviceTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreatePhysicalProduct()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                //using (var trx = conn.BeginTransaction())
                //{
                //    var repoPP =
                //        RepositoryManager.GetRepository<IPhysicalProductRepository<PhysicalProduct>>();

                //    var repoD = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();

                //    DealerInfo delaer = repoD.GetById(190000);

                //    PhysicalProduct cc = new PhysicalProduct()
                //    {
                //        VMO = delaer,
                //        //GenericDescription = new MultiLingualDescription() { DefaultMessage = "IPhone6s Plus", Texts = new List<LanguageSpecificText>() {new LanguageSpecificText(){ Language= ISO639LanguageCodes.eus, Text = "IPhone6s"}  } },
                //        //ShortName = new MultiLingualDescription() { DefaultMessage = "IPhone6p", Texts = new List<LanguageSpecificText>() {new LanguageSpecificText(){ Language= ISO639LanguageCodes.eus, Text = "IPhone6p"}  } },
                //        Code = "5634A",
                //        ModelNumber = "5634B",
                //        Number = "ABCD"
                //    };

                //    try
                //    {
                //        repoPP.Create(cc);
                //        trx.Commit();
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.Message);
                //    }
                //}

                using (var trx = conn.BeginTransaction())
                {
                    var repoPP =
                        RepositoryManager.GetRepository<IProductRepository<Product>>();

                    var repoD = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();

                    DealerInfo delaer = repoD.GetById(190000);

                    PhysicalProduct cc = new PhysicalProduct()
                    {
                        VMO = delaer,
                        //GenericDescription = new MultiLingualDescription() { DefaultMessage = "IPhone6s Plus", Texts = new List<LanguageSpecificText>() {new LanguageSpecificText(){ Language= ISO639LanguageCodes.eus, Text = "IPhone6s"}  } },
                        //ShortName = new MultiLingualDescription() { DefaultMessage = "IPhone6p", Texts = new List<LanguageSpecificText>() {new LanguageSpecificText(){ Language= ISO639LanguageCodes.eus, Text = "IPhone6p"}  } },
                        //Code = "5634A",
                        //ModelNumber = "5634D",
                        //Number = "ABCD"
                    };

                    try
                    {
                        repoPP.Create(cc);
                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

    }
}
