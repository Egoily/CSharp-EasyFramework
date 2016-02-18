using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.reveneu
{
    [TestFixture]
    public class CustomerChargeTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreateCustomerCharge()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repoCC =
                        RepositoryManager.GetRepository<ICustomerChargeRepository<CustomerCharge>>();
                    Int64 accId = 1020000000000000001;
                    var repoAcc = RepositoryManager.GetRepository<IAccountRepository<Account>>();
                    Account acc = repoAcc.GetById(accId);

                    Int32 chId = 1005000000;
                    var repoCh = RepositoryManager.GetRepository<IChargeRepository<Charge>>();
                    Charge ch = repoCh.GetById(chId);

                    Int64 cpaId = 1;
                    var repoCPA = RepositoryManager.GetRepository<ICustomerProductAssignmentRepository<CustomerProductAssignment>>();
                    CustomerProductAssignment cpa = repoCPA.GetById(cpaId);

                    var repo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                    int customerId = 1042089;
                    CustomerInfo cust = repo.GetById(customerId);

                    CustomerCharge cc = new CustomerCharge()
                    {
                        Amount = 100,
                        ChargingAccount = acc,
                        ChargeDefinition = ch,
                        ChargingDate = DateTime.Now,
                        Customer = cust,
                        Product = cpa,
                    };

                    try
                    {
                        repoCC.Create(cc);
                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        [Test]
        public void QueryCustomerCharge()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoCC =
                        RepositoryManager.GetRepository<ICustomerChargeRepository<CustomerCharge>>();

                Int64 id = 1;
                try
                {
                    var css = repoCC.GetById(id);
                    if (css != null)
                    {
                        Console.Write(css.Id);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }

            }
        }

        [Test]
        public void GetByCustomerIdAndInvoice()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoCC =
                        RepositoryManager.GetRepository<ICustomerChargeRepository<CustomerCharge>>();

                Int32 customerId = 1042089;
                try
                {
                    var css = repoCC.GetByCustomerIdAndInvoice(customerId, null);
                    Console.Write(css.Any());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }

            }
        }
    }
}
