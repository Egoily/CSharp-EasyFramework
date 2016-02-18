using System;
using com.etak.core.model;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class TransactionTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void InConnectionRollbackTest()
        {
            Int32 generatedId = 0;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {                 
                    var repoAddress = RepositoryManager.GetRepository<IAddressInfoRepository<AddressInfo>>();

                    AddressInfo add = new AddressInfo()
                    {
                        Address = "jaume",
                        Area = "Girona",
                        Block = "2",
                        City = "Girona",
                        CountryId = 34,
                        CreateDate = DateTime.Now,
                        CreateUser = 1,
                        HouseNo = "23",
                        ZipCode = "17002",

                    };
                    repoAddress.Create(add);
                    generatedId = add.Id;
                }
            }

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoAddress = RepositoryManager.GetRepository<IAddressInfoRepository<AddressInfo>>();
                var add = repoAddress.GetById(generatedId);
                Assert.AreEqual(add, default(AddressInfo), "THe object was found and the transaction was not commited");
            }
        }

        [Test]
        public void TestErrorTransactionInSameConnection()
        {
            var rnd = new Random();
            Int32 inDate = rnd.Next(Int32.MaxValue);
            bool firstException = false;
            bool secondException = false;
            bool thridException = false;

            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoHoli = RepositoryManager.GetRepository<IHolidayInfoRepository<HolidayInfo>>();
                //First transaction that works
                using (var trx = RepositoryManager.GetConnection().BeginTransaction())
                {
                    try
                    {
                        var holiday = new HolidayInfo()
                        {
                            HolidayDescription = "Test Holiday",
                            InDate = inDate,
                        };

                        repoHoli.Create(holiday);

                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("First exception: " + ex.Message);
                        firstException = true;
                    }
                    
                }

                //Second transaction and will throw error
                using (var trx = RepositoryManager.GetConnection().BeginTransaction())
                {
                    try
                    {
                        var holiday = new HolidayInfo()
                        {
                            HolidayDescription = "Test Holiday",
                            InDate = inDate,
                        };

                        repoHoli.Create(holiday);

                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Second exception: " + ex.Message);
                        secondException = true;
                    }
                }

                //First transaction that works
                using (var trx = RepositoryManager.GetConnection().BeginTransaction())
                {
                    try
                    {
                        var holiday = new HolidayInfo()
                        {
                            HolidayDescription = "Test Holiday",
                            InDate = inDate + 1,
                        };

                        repoHoli.Create(holiday);

                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Third exception: " + ex.Message);
                        thridException = true;
                    }
                }
            }

            Assert.IsFalse(firstException);
            Assert.IsTrue(secondException);
            Assert.IsFalse(thridException);
        }
    }
}