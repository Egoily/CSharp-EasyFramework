using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.resource
{
    [TestFixture]
    public class INumberInfoRepositoryTest :
        AbstractRepositoryTest<INumberInfoRepository<NumberInfo>, NumberInfo, String>
    {
        [TestFixtureSetUp]
        public static void Init()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public override void GetById()
        {
            DoTransacted(repo =>
            {
                NumberInfo nInfo =
                repo.GetById(ExistingId);
                if (nInfo != null)
                {
                    Console.Write("Accessing NumberProperty: ");
                    Console.WriteLine(" Value " + nInfo.NumberProperty.StatusID);

                    Console.Write("Accessing NumberDealerSharing: ");
                    Console.WriteLine(" Value " + nInfo.NumberDealerSharing.Count());
                }
            });
        }

        [Test]
        public void CreateTest()
        {
            DoTransacted(repo =>
            {
                var rand = new Random();
                var msisdn =  
                    "6000000"+rand.Next(1,100);

                NumberInfo nInfo = new NumberInfo()
                {
                    Resource = msisdn,
                    CategoryID = (int)CategoryType.Normal,
                    DataStatus = null,
                    TrafficTypeID = 1,
                    CreateDate = System.DateTime.Now,
                    UpdateDate = System.DateTime.Now,
                    NumberDealerSharing = null,
                    NumberProperty = null,
                };

                NumberPropertyInfo prop = new NumberPropertyInfo()
                {
                    Resource = msisdn,
                    StatusID = (int)ResourceStatus.Init,
                    CreateDate = System.DateTime.Now,
                    UpdateDate = System.DateTime.Now,
                    ChangeStatusDate = DateTime.Now,
                    NumberInfo = null,
                };

                DealerNumberInfo dlr = new DealerNumberInfo()
                {
                    Resource = nInfo,
                    DealerID = 400000,
                    ShareType = 0
                };

                //nInfo.NumberDealerSharing = new List<DealerNumberInfo>() { dlr };
                nInfo.NumberProperty = prop;
                prop.NumberInfo = nInfo;


                repo.Create(nInfo);


            });
        }

        [Test]
        public void GetByCategoryAndVmnoAndStatusIdIn()
        {
            DoTransacted(repo =>
            {
                try
                {
                    List<int> mvnoIds = new List<int>();
                    mvnoIds.Add(190000);
                    IEnumerable<int> mvnoIdsIEnumerable = mvnoIds;
                    IEnumerable<NumberInfo> nInfo =
                    repo.GetByCategoryAndVmnoAndStatusIdIn(mvnoIdsIEnumerable, 4, new Int32[] { 8 }, 100);

                    foreach (var numberInfo in nInfo)
                    {
                        Console.Write("Accessing NumberProperty: ");
                        Console.WriteLine(" Value " + numberInfo.NumberProperty.StatusID);

                        Console.Write("Accessing NumberDealerSharing: ");
                        Console.WriteLine(" Value " + numberInfo.NumberDealerSharing.Count());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("AA" + ex);
                    throw;
                }

            });
        }

        protected override string ExistingId
        {
            get { return ("34600000016"); }
        }
    }
}
