using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.crm.subscription.catalog;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.reveneu
{
    [TestFixture]
    public class IProductChargeOptionRepositoryTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void ProductChargeOptionQuery()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<IProductChargeOptionRepository<ProductChargeOption>>();
                try
                {
                    var prod = repo.GetById(1006000000);
                    Console.WriteLine(prod.Id);
                }
               catch(Exception e)
                {
                    Console.Write("" + e.Message); 
                }
            }
        }
    
        [Test]
        public void CreateProductChargeOptions()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {

                    var productChargeOptRepo =
                        RepositoryManager.GetRepository<IProductChargeOptionRepository<ProductChargeOption>>();
                    var productRepo = RepositoryManager.GetRepository<IProductRepository<Product>>();
                    var productOfferingRepo = RepositoryManager.GetRepository<IProductOfferingRepository<ProductOffering>>();
                    var chargeRepo = RepositoryManager.GetRepository<IChargeRepository<Charge>>();
                    Product product = productRepo.GetById(1003000000);
                    if (product != null)
                    {
                        ChargeNonRecurring c = new ChargeNonRecurring
                        {
                            Category = null,
                            CreateTime = DateTime.Now,
                            InformationalOnly = InformationalTypes.N,
                            ProrateQty = 0,
                            ProrateUnit = TimeUnits.Minute,
                            Status = ChargeStatus.Active,
                            TypeOfTimeOfCharge = TimesOfCharge.Arrear,

                        };
                        chargeRepo.Create(c);
                        c.Prices = new List<ChargePrice>
                        {
                            new ChargePrice
                            {
                                Amount = 1,
                                Id = new ChargePriceId {ChargeId = c.Id, StartDate = DateTime.Now},
                                Currency = ISO4217CurrencyCodes.EUR,
                                EndDate = null
                            },
                            new ChargePrice
                            {
                                Amount = 2,
                                Id = new ChargePriceId {ChargeId = c.Id, StartDate = DateTime.Now.AddDays(10)},
                                Currency = ISO4217CurrencyCodes.EUR,
                                EndDate = null
                            }
                        };

                        ProductOffering offering = new ProductOffering()
                        {
                            ChargingOptions = new List<ProductChargeOption>(),
                            OfferedProduct = product,
                        };
                        productOfferingRepo.Create(offering);

                        ProductChargeOption opt1 = new ProductChargeOption()
                        {
                            Charges = new List<Charge> {c},
                            CreateDate = DateTime.Now,
                            StartDate = DateTime.Now.AddMinutes(1),
                            EndDate = DateTime.Now.AddYears(2),
                            IsDefaultOption = DefaultOptions.Y,
                            Status = ProductPurchaseStatus.Default,
                            ProductOffering = offering,
                        };
                        c.ReferencingOptions.Add(opt1);
                        offering.ChargingOptions.Add(opt1);
                        productChargeOptRepo.Create(opt1);
                        trx.Commit();
                    }
                    
                }
            }
        }
   
    }
}
