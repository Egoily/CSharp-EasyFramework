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
    public class IInvoiceRepositoryTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreateInvoice()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repoInvoice =
                        RepositoryManager.GetRepository<IInvoiceRepository<Invoice>>();

                    Int64 accId = 1020000000000000001;
                    var repoAcc = RepositoryManager.GetRepository<IAccountRepository<Account>>();
                    Account acc = repoAcc.GetById(accId);

                    var repo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                    int customerId = 1042089;
                    CustomerInfo cust = repo.GetById(customerId);

                    Int64 chId1 = 1;
                    Int64 chId2 = 2;
                    var repoCCh = RepositoryManager.GetRepository<ICustomerChargeRepository<CustomerCharge>>();
                    CustomerCharge ch1 = repoCCh.GetById(chId1);
                    CustomerCharge ch2 = repoCCh.GetById(chId2);
                    IList<CustomerCharge> listCc = new List<CustomerCharge>();
                    listCc.Add(ch1);
                    listCc.Add(ch2);

                    Int32 brId = 1;
                    var repoBR = RepositoryManager.GetRepository<IBillRunRepository<BillRun>>();
                    BillRun br = repoBR.GetById(brId);

                    Invoice invoice = new Invoice()
                    {
                        ChargingAccount = acc,
                        ChargedCustomer = cust,
                        Charges = listCc,
                        GeneratingBillRun = br,
                        EndDate = DateTime.Now.AddDays(10),
                        StartDate = DateTime.Now,
                        Status = InvoiceStatus.Drafted,
                    };

                    try
                    {
                        repoInvoice.Create(invoice);
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
        public void QueryInvoice()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoInvoice = RepositoryManager.GetRepository<IInvoiceRepository<Invoice>>();

                Int64 id = 1020000000000000001;
                try
                {
                    var invoice = repoInvoice.GetById(id);
                    if(invoice!=null)
                    Console.WriteLine(invoice.Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }

            }
        }

        [Test]
        public void DeleteInvoice()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoInvoice =
                        RepositoryManager.GetRepository<IInvoiceRepository<Invoice>>();

                Int64 id = 1;
                try
                {
                    var invoice = repoInvoice.GetById(id);
                    if (invoice != null)
                    repoInvoice.Delete(invoice);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        [Test]
        public void GetLastNInvoiceTest()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoInvoice =
                        RepositoryManager.GetRepository<IInvoiceRepository<Invoice>>();

                Int32 cid = 1541000024;
                try
                {
                    var list = repoInvoice.GetLastNInvoices(cid, 10);
                    if (list != null)
                    Console.Write(list.ToList().Count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        [Test]
        public void IncredulosTest()
        {
            using (RepositoryManager.GetNewConnection())
            {
                var repoInvoice = RepositoryManager.GetRepository<IInvoiceRepository<Invoice>>();
                try
                {
                    repoInvoice.GetLastNInvoicesAndStatusIn(3415, 2, new List<InvoiceStatus?>
                    {InvoiceStatus.Closed, InvoiceStatus.Drafted}).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        [Test]
        public void GetByLegalInvoiceNumberTest()
        {
            using (RepositoryManager.GetNewConnection())
            {
                var repoInvoice = RepositoryManager.GetRepository<IInvoiceRepository<Invoice>>();

                Int32 cid = 1857000324;
                try
                {
                    repoInvoice.GetByLegalInvoiceNumber(cid, "10").FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }
}
