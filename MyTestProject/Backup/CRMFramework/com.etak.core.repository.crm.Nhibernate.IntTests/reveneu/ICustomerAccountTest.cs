using System;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.reveneu
{
    [TestFixture]
    public class ICustomerAccountTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void UpdateCustomerAccount()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var accountRepo = RepositoryManager.GetRepository<IAccountRepository<Account>>();
                    var account = accountRepo.GetById(1020000000000000006);
                    if (account != null)
                    {
                        account.Balance.Balance = 2.0M;
                        accountRepo.Update(account);
                    }
                    trx.Commit();
                }
            }
        }

        [Test]
        public void DeleteCustomerAccount()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var accountRepo = RepositoryManager.GetRepository<IAccountRepository<Account>>();
                    var account = accountRepo.GetById(1020000000000000007);
                    if (account != null)
                        accountRepo.Delete(account);
                    trx.Commit();
                }
            }
        }

        [Test]
        public void QueryCustomerAccount()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var accountRepo = RepositoryManager.GetRepository<IAccountRepository<Account>>();
                    var account = accountRepo.GetById(1020000000000000006);
                    if (account != null)
                    {
                        Decimal balance = account.Balance.Balance;    
                    }
                    
                    trx.Commit();
                }
            }
        }

        [Test]
        public void CreateAccount()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    try
                    {
                        var accountRepo = RepositoryManager.GetRepository<IAccountRepository<Account>>();
                        Account data = new AccountData
                        {
                            BalanceUnit = DataUnits.GigaBit,
                            Balance = new BalanceForAccount { Balance = 3.14M },
                        };
                        data.Balance.Account = data;
                        accountRepo.Create(data);


                        Account Time = new AccountTime
                        {
                            BalanceUnit = TimeUnits.Minute,
                            Balance = new BalanceForAccount { Balance = 3.14M },
                        };
                        Time.Balance.Account = Time;
                        accountRepo.Create(Time);

                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ex" + ex.Message);
                    }
                }
            }
        }

        [Test]
        public void CreateCustomerAssociation()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repoAss =
                        RepositoryManager
                            .GetRepository<ICustomerAccountAssociationRepository<CustomerAccountAssociation>>();
                    var repoCust = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                    var repoAcc = RepositoryManager.GetRepository<IAccountRepository<Account>>();
                    CustomerInfo cust = repoCust.GetById(5000204);
                    Account acc = repoAcc.GetById(1020000000000000001);

                    CustomerAccountAssociation caa = new CustomerAccountAssociation()
                    {
                        Account = acc,
                        Customer = cust,
                        EndTime = DateTime.Now.AddDays(30),
                        StartDate = DateTime.Now.AddDays(-1),
                    };
                    repoAss.Create(caa);
                    trx.Commit();
                }
            }
        }
    }
}
