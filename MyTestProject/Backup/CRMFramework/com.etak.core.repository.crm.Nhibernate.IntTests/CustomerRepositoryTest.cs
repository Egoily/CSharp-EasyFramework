using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.Nhibernate.Factory;
using log4net;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class CustomerRepositoryTest
    {
        static private ILog log;
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        [Test]
        public void CreateCustomer()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    try
                    {
                        var repocust = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();

                        BankInfo bankInfo = new BankInfo()
                        {
                            ABI = "1231",
                            IBAN = "ES9865656565",
                            CreateDate = DateTime.Now,
                            UserID = 14,

                        };

                        PropertyInfo prop = new PropertyInfo()
                        {
                            Email = "Fulanito@gmail.com",
                            CustomerTypeID = (int)CustomerBusinessType.Private,
                            PaymentMethodID = (int)PaymentType.Postpayment,
                            AcceptNews = true,
                            InvoiceDetails = true,
                            UserName = "SSSS",
                            CreateDate = DateTime.Now,
                            UserID = 14,

                        };

                        IList<PropertyInfo> listProp = new List<PropertyInfo>();

                        listProp.Add(prop);

                        AddressInfo add = new AddressInfo()
                        {
                            Address = "sss",
                            Area = "ccc",
                            Block = "2",
                            City = "Girona",
                            CountryId = 34,
                            CreateDate = DateTime.Now,
                            CreateUser = 1,
                            HouseNo = "23",
                            ZipCode = "17002",

                        };

                        IList<BankInfo> bankinfoList = new List<BankInfo>();
                        bankinfoList.Add(bankInfo);

                        CustomerInfo custTest = new CustomerInfo()
                        {
                            ActivedDate = DateTime.Now,
                            PropertyInfo = listProp,
                            StatusID = 1,
                            BankInfo = bankinfoList,
                            DealerID = 170000,
                            CreateDate = DateTime.Now,
                            UserID = 14,
                            FirstName = "CCCC",
                            LastName = "FFFF",

                        };

                        CustomerAddress custAddress = new CustomerAddress()
                        {
                            Address = add,
                            UsageType = AddressUsages.PersonalAddress,
                            Customer = custTest
                        };

                        custTest.Addresses.Add(custAddress);
                        bankInfo.CustomerInfo = custTest;
                        prop.CustomerInfo = custTest;

                        repocust.Create(custTest);

                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw ex;
                    }

                }
            }
        }

        [Test]
        public void QueryCustomerById()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();

                int customerId = 5205217;

                CustomerInfo cust = repo.GetById(customerId);

                Console.WriteLine(cust.CustomerID);
                Console.WriteLine(cust.Company);

            }
        }

        [Test]
        public void QueryCustomerByParentId()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();

                int parentId = 1673000034;

                IList<CustomerInfo> custList = repo.GetByParentId(parentId).ToList();

                Console.WriteLine(custList.Count);

            }
        }

        [Test]
        public void QueryCustomerByExternalId()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();

                var custDummy = CreateCustomerDummy(conn);

                var externalId = custDummy.PropertyInfo.FirstOrDefault().ExternalId;
                var customerId = custDummy.CustomerID.Value;

                var customerInfo = repo.GetById(customerId);
                log.Debug("Customer info: " + customerInfo.FirstName);
                try
                {
                    var dt = System.DateTime.Now;
                    CustomerInfo cust = repo.GetByExternalId(externalId).FirstOrDefault();
                    log.DebugFormat("duration:{0}", (System.DateTime.Now - dt).TotalMilliseconds);
                    log.Info("Loop done");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            log.Info("Test done");
        }

        [Test]
        public void DeleteCustomer()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                    BankInfo bankInfo = new BankInfo()
                    {
                        ABI = "1231",
                        IBAN = "ES9865656565",
                        CreateDate = DateTime.Now,
                        UserID = 14,

                    };

                    PropertyInfo prop = new PropertyInfo()
                    {
                        Email = "Fulanito@gmail.com",
                        CustomerTypeID = (int)CustomerBusinessType.Private,
                        PaymentMethodID = (int)PaymentType.Postpayment,
                        AcceptNews = true,
                        InvoiceDetails = true,
                        UserName = "SSSS",
                        CreateDate = DateTime.Now,
                        ExternalId = "12345",
                        UserID = 14,

                    };

                    IList<PropertyInfo> listProp = new List<PropertyInfo>();

                    listProp.Add(prop);

                    AddressInfo add = new AddressInfo()
                    {
                        Address = "sss",
                        Area = "ccc",
                        Block = "2",
                        City = "Girona",
                        CountryId = 34,
                        CreateDate = DateTime.Now,
                        CreateUser = 1,
                        HouseNo = "23",
                        ZipCode = "17002",

                    };

                    IList<BankInfo> bankinfoList = new List<BankInfo>();
                    bankinfoList.Add(bankInfo);

                    CustomerInfo custTest = new CustomerInfo()
                    {
                        ActivedDate = DateTime.Now,
                        PropertyInfo = listProp,
                        StatusID = 1,
                        BankInfo = bankinfoList,
                        DealerID = 170000,
                        CreateDate = DateTime.Now,
                        UserID = 14,
                        FirstName = "CCCC",
                        LastName = "FFFF",

                    };

                    CustomerAddress custAddress = new CustomerAddress()
                    {
                        Address = add,
                        UsageType = AddressUsages.PersonalAddress,
                        Customer = custTest
                    };

                    custTest.Addresses.Add(custAddress);
                    bankInfo.CustomerInfo = custTest;
                    prop.CustomerInfo = custTest;
                    var cust = repo.Create(custTest);

                    var custToBeDeleted = repo.GetById(cust.CustomerID.Value);

                    repo.Delete(custToBeDeleted);

                    trx.Commit();
                }


            }
        }
        [Test]
        public void UpdateCustomer()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                    BankInfo bankInfo = new BankInfo()
                    {
                        ABI = "1231",
                        IBAN = "ES9865656565",
                        CreateDate = DateTime.Now,
                        UserID = 14,

                    };

                    PropertyInfo prop = new PropertyInfo()
                    {
                        Email = "Fulanito@gmail.com",
                        CustomerTypeID = (int)CustomerBusinessType.Private,
                        PaymentMethodID = (int)PaymentType.Postpayment,
                        AcceptNews = true,
                        InvoiceDetails = true,
                        UserName = "SSSS",
                        CreateDate = DateTime.Now,
                        ExternalId = "12345",
                        UserID = 14,

                    };

                    IList<PropertyInfo> listProp = new List<PropertyInfo>();

                    listProp.Add(prop);

                    AddressInfo add = new AddressInfo()
                    {
                        Address = "sss",
                        Area = "ccc",
                        Block = "2",
                        City = "Girona",
                        CountryId = 34,
                        CreateDate = DateTime.Now,
                        CreateUser = 1,
                        HouseNo = "23",
                        ZipCode = "17002",

                    };

                    IList<BankInfo> bankinfoList = new List<BankInfo>();
                    bankinfoList.Add(bankInfo);

                    CustomerInfo custTest = new CustomerInfo()
                    {
                        ActivedDate = DateTime.Now,
                        PropertyInfo = listProp,
                        StatusID = 1,
                        BankInfo = bankinfoList,
                        DealerID = 170000,
                        CreateDate = DateTime.Now,
                        UserID = 14,
                        FirstName = "CCCC",
                        LastName = "FFFF",

                    };

                    CustomerAddress custAddress = new CustomerAddress()
                    {
                        Address = add,
                        UsageType = AddressUsages.PersonalAddress,
                        Customer = custTest
                    };

                    custTest.Addresses.Add(custAddress);
                    bankInfo.CustomerInfo = custTest;
                    prop.CustomerInfo = custTest;
                    var cust = repo.Create(custTest);
                    


                    #region Adding a new BankInfo
                    if (cust.BankInfo.Any())
                    {
                        BankInfo custBank = cust.BankInfo[0];
                        custBank.EndDate = DateTime.Now;
                    }
                    BankInfo newBankInfo = new BankInfo()
                    {
                        ABI = "5",
                        IBAN = "ES99999999",
                        CreateDate = DateTime.Now,
                        UserID = 14,
                    };
                    newBankInfo.CustomerInfo = cust;
                    cust.BankInfo.Add(newBankInfo);

                    #endregion

                    #region Adding a new Address
                    AddressInfo newAddress= new AddressInfo()
                    {
                        Address = "Sesam Street",
                        Area = "TV Show",
                        City = "London",
                        CountryId = 44,
                        CreateDate = DateTime.Now,
                        CreateUser = 1,
                        ZipCode = "17000",

                    };

                    AddressInfo add1 = new AddressInfo() { Address = "address" };

                    CustomerAddress usageAdd = new CustomerAddress()
                    {
                        Address = add,
                        Customer = cust,
                        UsageType = AddressUsages.FiscalAddress
                    };
                    usageAdd.Customer = cust;

                    add1.AddressUsages.Add(usageAdd);
                    cust.Addresses.Add(usageAdd);

                    #endregion

                    repo.Update(cust);

                    trx.Commit();



                }
            }
        }

        public CustomerInfo CreateCustomerDummy(IPersistanceConnection conn)
        {
            using (var trx = conn.BeginTransaction())
            {
                var repocust = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();

                BankInfo bankInfo = new BankInfo()
                {
                    ABI = "1231",
                    IBAN = "ES9865656565",
                    CreateDate = DateTime.Now,
                    UserID = 14,

                };

                PropertyInfo prop = new PropertyInfo()
                {
                    Email = "Fulanito@gmail.com",
                    CustomerTypeID = (int)CustomerBusinessType.Private,
                    PaymentMethodID = (int)PaymentType.Postpayment,
                    AcceptNews = true,
                    InvoiceDetails = true,
                    UserName = "SSSS",
                    CreateDate = DateTime.Now,
                    ExternalId = "12345",
                    UserID = 14,

                };

                IList<PropertyInfo> listProp = new List<PropertyInfo>();

                listProp.Add(prop);

                AddressInfo add = new AddressInfo()
                {
                    Address = "sss",
                    Area = "ccc",
                    Block = "2",
                    City = "Girona",
                    CountryId = 34,
                    CreateDate = DateTime.Now,
                    CreateUser = 1,
                    HouseNo = "23",
                    ZipCode = "17002",

                };

                IList<BankInfo> bankinfoList = new List<BankInfo>();
                bankinfoList.Add(bankInfo);

                CustomerInfo custTest = new CustomerInfo()
                {
                    ActivedDate = DateTime.Now,
                    PropertyInfo = listProp,
                    StatusID = 1,
                    BankInfo = bankinfoList,
                    DealerID = 170000,
                    CreateDate = DateTime.Now,
                    UserID = 14,
                    FirstName = "CCCC",
                    LastName = "FFFF",

                };

                CustomerAddress custAddress = new CustomerAddress()
                {
                    Address = add,
                    UsageType = AddressUsages.PersonalAddress,
                    Customer = custTest
                };

                custTest.Addresses.Add(custAddress);
                bankInfo.CustomerInfo = custTest;
                prop.CustomerInfo = custTest;
                var cust = repocust.Create(custTest);
                trx.Commit();
                return cust;

            }
        }
    }
}
