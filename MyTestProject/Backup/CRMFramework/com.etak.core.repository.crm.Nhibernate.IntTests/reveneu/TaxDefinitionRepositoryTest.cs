using System;
using System.Linq;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using FluentNHibernate.Conventions;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.reveneu
{
    [TestFixture]
    public class TaxDefinitionRepositoryTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreateTaxRate()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var taxDefinitionRepo = RepositoryManager.GetRepository<ITaxDefinitionRepository<TaxDefinition>>();
                    var taxes = taxDefinitionRepo.GetDefinitionsByZipCodeLike("17000");
                    if (taxes.IsNotEmpty())
                    {
                        var createTax = new TaxDefinition()
                        {
                            Description = taxes.FirstOrDefault().Description,
                            Rates = null,
                            TaxCategory = taxes.FirstOrDefault().TaxCategory,
                            ZipRanges = null
                        };
                        taxDefinitionRepo.Create(createTax);
                        trx.Commit();
                    }
                   
                }
            }
        }
 
        [Test]
        public void QueryTaxDefinitionById()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var chargeRepo = RepositoryManager.GetRepository<ITaxDefinitionRepository<TaxDefinition>>();
                    var tax = chargeRepo.GetById(1001000000);

                    foreach(var rate in tax.Rates)
                    {
                        Console.WriteLine(" Percentage: {0} Definition.Id:{1}", rate.Percentage, rate.Definition.Id);
                    }
                    trx.Commit();
                }
            }
        }
            

        [Test]
        public void QueryByZipCode()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var chargeRepo = RepositoryManager.GetRepository<ITaxDefinitionRepository<TaxDefinition>>();
                    var taxes = chargeRepo.GetDefinitionsByZipCodeLike("17000");
                    foreach (var tax in taxes)
                    {
                        foreach (var rate in tax.Rates)
                        {
                            Console.WriteLine(" Percentage: {0} Definition.Id:{1}", rate.Percentage, rate.Definition.Id);
                        }
                    }
                    trx.Commit();
                }
            }
        }
    }
}
