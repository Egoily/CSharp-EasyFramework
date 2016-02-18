using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate.IDGeneration;
using com.etak.core.test.utilities;
using FluentNHibernate.Conventions;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.reveneu
{
    [TestFixture]
    public class PaymentInfoRepositoryTest
    {
        private static long? PaymentInfoId;

        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreatePaymentInfo()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var paymentRepo = RepositoryManager.GetRepository<IPaymentInfoRepository<PaymentInfo>>();

                    var newPayment = new PaymentInfo()
                    {
                        //Id = PaymentInfoId,
                        Amount = 100,
                        Currency = ISO4217CurrencyCodes.EUR,
                        Discount = 10,
                        ExternalPayment = "Test Payment",
                        Invoice = CreateDefaultObject.Create<Invoice>(),
                        Order = null,
                        PaymentInfoText = "Test Payment",
                        PaymentMethod = 1,
                        Status = 0,
                        TaxInfo = null,
                    };
                    
                    
                    paymentRepo.Create(newPayment);
                    PaymentInfoId = newPayment.Id;
                    trx.Commit();
                }
            }
        }

        [Test]
        public void QueryPaymentInfoById()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    //If the Create Test has been executed, let's use the Id created to get an existing PaymentInfo
                    var paymentId = PaymentInfoId.HasValue ? PaymentInfoId.Value : 143;
                    
                    var paymentRepo = RepositoryManager.GetRepository<IPaymentInfoRepository<PaymentInfo>>();
                    var payment = paymentRepo.GetById(paymentId);

                    if (payment != null)
                    {
                        Assert.AreEqual(payment.Id, paymentId);
                    }

                }
            }
        }

    }
}
