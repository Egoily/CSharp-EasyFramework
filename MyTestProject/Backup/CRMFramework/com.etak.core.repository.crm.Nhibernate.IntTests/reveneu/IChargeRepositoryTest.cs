using System;
using System.Collections.Generic;
using System.Reflection;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using log4net;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.reveneu
{
    [TestFixture]
    public class IChargeRepositoryTest
    {
        private static ILog log;
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
            log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);
        }

        [Test]
        public void Query()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var ChargeRepo = RepositoryManager.GetRepository<IChargeRepository<Charge>>();
                    var charge = ChargeRepo.GetById(1005000016);
                    if (charge != null)
                    {
                        Console.Write(charge.Id);

                        foreach (var price in charge.Prices)
                        {
                            Console.WriteLine("Price Amount:{0} DateStart:{1} DateEnd:{2}", price.Amount, price.Id.StartDate, price.EndDate);
                        }
                    }
                    trx.Commit();
                }
            }

            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var ChargeRepo = RepositoryManager.GetRepository<IChargeRepository<Charge>>();
                    var charge = ChargeRepo.GetById(1005000016);
                    if (charge != null)
                    {
                        Console.Write(charge.Id);

                        foreach (var price in charge.Prices)
                        {
                            Console.WriteLine("Price Amount:{0} DateStart:{1} DateEnd:{2}", price.Amount,
                                price.Id.StartDate, price.EndDate);
                        }
                    }
                    trx.Commit();
                }
            }
        }

        [Test]
        public void TestCache()
        {
            Int32 ChargeId = 1005000000;
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var ChargeRepo = RepositoryManager.GetRepository<IChargeRepository<Charge>>();
                    var charge = ChargeRepo.GetById(ChargeId);
                    Console.Write(charge.Id);
                    //force the initialization of lazy props
                    log.InfoFormat("Description default message: {0}", charge.Description.DefaultMessage);

                    foreach (var msg in charge.Description.Texts)
                        log.InfoFormat("Description in lang: {0} text:{1}", msg.Language, msg.Text);

                    log.InfoFormat("Name default message: {0}", charge.Name.DefaultMessage);
                    foreach (var msg in charge.Name.Texts)
                        log.InfoFormat("Name in lang: {0} text:{1}", msg.Language, msg.Text);

                    log.InfoFormat("Price count: {0}", charge.Prices.Count);
                    log.InfoFormat("Price ReferencedCharges: {0}", charge.ReferencedCharges.Count);
                    log.InfoFormat("Price ReferencingCharges: {0}", charge.ReferencingCharges.Count);
                    log.InfoFormat("Price ReferencingOptions: {0}", charge.ReferencingOptions.Count);

                    trx.Commit();
                }
            }

            //This second iteration should not hit the DB. 
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var ChargeRepo = RepositoryManager.GetRepository<IChargeRepository<Charge>>();
                    var charge = ChargeRepo.GetById(ChargeId);
                    if (charge != null)
                    {
                        Console.Write(charge.Id);
                        //force the initialization of lazy props
                        log.InfoFormat("Description default message: {0}", charge.Description.DefaultMessage);

                        foreach (var msg in charge.Description.Texts)
                            log.InfoFormat("Description in lang: {0} text:{1}", msg.Language, msg.Text);

                        log.InfoFormat("Name default message: {0}", charge.Name.DefaultMessage);
                        foreach (var msg in charge.Name.Texts)
                            log.InfoFormat("Name in lang: {0} text:{1}", msg.Language, msg.Text);

                        log.InfoFormat("Price count: {0}", charge.Prices.Count);
                        log.InfoFormat("Price ReferencedCharges: {0}", charge.ReferencedCharges.Count);
                        log.InfoFormat("Price ReferencingCharges: {0}", charge.ReferencingCharges.Count);
                        log.InfoFormat("Price ReferencingOptions: {0}", charge.ReferencingOptions.Count);

                        trx.Commit();
                    }
                    
                }
            }
        }

        [Test]
        public void CreateNonRecurringCharge()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var chargeRepo = RepositoryManager.GetRepository<IChargeRepository<Charge>>();
                    var chargePriceRepo = RepositoryManager.GetRepository<IChargePriceRepository<ChargePrice>>();
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
                    //c.Prices = new List<ChargePrice> 
                    //{
                    //    new ChargePrice { Amount = 2, Id = { ChargeId = c.Id,  StartDate = DateTime.Now.AddDays(10)} ,Currency= ISO4217CurrencyCodes.EUR, EndDate = null },
                    //    new ChargePrice { Amount = 1, Id = { ChargeId = c.Id,  StartDate = DateTime.Now},  Currency= ISO4217CurrencyCodes.EUR, EndDate = null},
                    //};

                    //ProductChargeOption opt1 = new ProductChargeOption()
                    //{
                    //    Charges = new List<Charge> { c },
                    //    CreateDate = DateTime.Now,
                    //    StartDate = DateTime.Now.AddMinutes(1),
                    //    EndDate = DateTime.Now.AddYears(2),
                    //    IsDefaultOption = DefaultOptions.Y,
                    //    Status = ProductPurchaseStatus.Default,
                    //};
                    //c.ReferencingOptions.Add(opt1);

                    chargeRepo.Create(c);

                    trx.Commit();
                }
            }
        }


        [Test]
        public void CreateRecurringCharge()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var chargeRepo = RepositoryManager.GetRepository<IChargeRepository<Charge>>();
                    Charge c = new ChargeRecurring
                   {
                       Category = null,
                       CreateTime = DateTime.Now,
                       InformationalOnly = InformationalTypes.N,
                       ProrateQty = 0,
                       ProrateUnit = TimeUnits.Minute,
                       Status = ChargeStatus.Active,


                   };
                    //c.Prices = new List<ChargePrice> 
                    //{
                    //    new ChargePrice { Amount = 1, Id = { ChargeId = c.Id, StartDate = DateTime.Now } ,Currency= ISO4217CurrencyCodes.EUR,  EndDate = null},
                    //    new ChargePrice { Amount = 2, Id = { ChargeId = c.Id, StartDate = DateTime.Now.AddDays(10) }, Currency= ISO4217CurrencyCodes.EUR,  EndDate = null}
                    //};

                    chargeRepo.Create(c);
                    trx.Commit();
                }
            }
        }

    }
}
