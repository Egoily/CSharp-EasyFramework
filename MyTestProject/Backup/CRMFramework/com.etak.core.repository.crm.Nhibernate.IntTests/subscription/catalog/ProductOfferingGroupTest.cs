using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.subscription.catalog;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.subscription.catalog
{
    [TestFixture]
    public class ProductOfferingGroupTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreateProductOfferingGroup()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repoProductOfferingGroup =
                        RepositoryManager.GetRepository<IProductOfferingGroupRepository<ProductOfferingGroup>>();

                    var productOfferingGroup = new ProductOfferingGroup();
                    MultiLingualDescription NameForProduct = new MultiLingualDescription
                    {
                        DefaultMessage = "this is a test product",

                    };
                    NameForProduct.Texts = new List<LanguageSpecificText>
                    {
                        { new LanguageSpecificText { Description = NameForProduct, Language = ISO639LanguageCodes.aar, Text = "aar sample text 3" }},
                        { new LanguageSpecificText { Description = NameForProduct, Language = ISO639LanguageCodes.zza, Text = "zza sample text 3" }}
                    };

                    MultiLingualDescription DescriptionForProduct = new MultiLingualDescription
                    {
                        DefaultMessage = "this is a test product",

                    };
                    productOfferingGroup.Description = DescriptionForProduct;
                    productOfferingGroup.Names = NameForProduct;

                    
                    try
                    {
                        repoProductOfferingGroup.Create(productOfferingGroup);
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
        public void QueryProductOfferingGroup()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoProductOfferingGroup =
                        RepositoryManager.GetRepository<IProductOfferingGroupRepository<ProductOfferingGroup>>();

                int id = 1020000001;
                try
                {
                    var css = repoProductOfferingGroup.GetById(id);
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

    }
}
