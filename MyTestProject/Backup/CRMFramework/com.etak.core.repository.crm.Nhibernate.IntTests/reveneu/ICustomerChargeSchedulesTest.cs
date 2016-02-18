using System;
using System.Collections.Generic;
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
    public class ICustomerChargeSchedulesTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreateCustomerChargeSchedules()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    int customerId = 1020000001;
                    var customerRepo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                    var customer = customerRepo.GetById(customerId);

                    var repoCCS =
                        RepositoryManager.GetRepository<ICustomerChargeScheduleRepository<CustomerChargeSchedule>>();

                    var repoAcc = RepositoryManager.GetRepository<IAccountRepository<Account>>();
                    Account acc = repoAcc.GetById(1020000000000000001);
                    
                    var repoCh = RepositoryManager.GetRepository<IChargeRepository<Charge>>();
                    Charge ch = repoCh.GetById(1005000000);

                    CustomerChargeSchedule ccs = new CustomerChargeSchedule()
                    {
                        ChargedAccount = acc,
                        ChargeDefinition = ch,
                        Charges = new List<CustomerCharge>(),
                        CreateDate = DateTime.Now,
                        CurrentCyclenumber = 5,
                        Customer = customer,
                        NextChargeDate = DateTime.Now.AddDays(15),
                        NextPeriodNumber = 5,
                        PriceEffectiveDate = null,
                        UpdateDate = DateTime.Now,
                    };

                    try
                    {
                        repoCCS.Create(ccs);
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
        public void QueryCustomerChargeSchedules()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoCCS = RepositoryManager.GetRepository<ICustomerChargeScheduleRepository<CustomerChargeSchedule>>();
                var customerRepo =  RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                Int32 id = 1020000001;
                var customer = customerRepo.GetById(id);
                var css = repoCCS.GetChargeSchedulesByCustomer(customer);
                var charges = css.FirstOrDefault();
                Console.Write(charges);
            }
        }
    }
}
