using System;
using com.etak.core.model;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.test.utilities;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture()]
    public class AddressInfoTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreateAddress()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    Int32 CustomerId = 1042089;
                    CustomerInfo customer =
                        RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>().GetById(CustomerId);
                    var repoAddress = RepositoryManager.GetRepository<IAddressInfoRepository<AddressInfo>>();

                    AddressInfo add = new AddressInfo
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

                    CustomerAddress usage1 = new CustomerAddress
                    {
                        Address = add,
                        Customer = customer,
                        UsageType = AddressUsages.DeliveryAddress
                    };

                   
                    try
                    {   
                        customer.Addresses.Add(usage1);
                        repoAddress.Create(add);

                        trx.Commit();
                    }
                    catch (Exception ex)
                    { 
                        Console.WriteLine(ex.Message);
                        throw;
                    }

                }
            }
        }

        [Test]
        public void QueryAddressById()
        {
            using (RepositoryManager.GetNewConnection())
            {
                var repoAddress = RepositoryManager.GetRepository<IAddressInfoRepository<AddressInfo>>();

                repoAddress.GetById(1);

            }
        }

       

        [Test]
        public void UpdateAddress()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    Int32 id = 1;
                    var repoAddress = RepositoryManager.GetRepository<IAddressInfoRepository<AddressInfo>>();
                    
                    AddressInfo add = repoAddress.GetById(id);
                    add.Address = "FRUTA";
                    try
                    {
                        repoAddress.Update(add);
                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }                   

                }
            }
        }

        [Test]
        public void DeleteAddress()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repoAddress = RepositoryManager.GetRepository<IAddressInfoRepository<AddressInfo>>();
                    AddressInfo add = CreateDefaultObject.Create<AddressInfo>();
                    repoAddress.Create(add);
                    repoAddress.Delete(add);

                    trx.Commit();

                }
            }
        }
    }
}