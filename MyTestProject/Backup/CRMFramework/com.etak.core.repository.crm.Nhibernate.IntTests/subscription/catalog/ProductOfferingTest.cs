using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.dtoConverters;
using com.etak.core.repository.crm.Nhibernate.subscription.catalog.Mapping;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.subscription.catalog
{
    [TestFixture]
    public class ProductOfferingTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreateProductOffering()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repoProductOffering =
                        RepositoryManager.GetRepository<IProductOfferingRepository<ProductOffering>>();

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

                    var productOffering = CreateDefaultObject.Create<ProductOffering>();
                    productOffering.Description = DescriptionForProduct;
                    productOffering.Names = NameForProduct;
                    productOffering.OfferedProduct.Id = 1020000001;
                    productOffering.Group.Id = 1020000001;
                    var producOffering1 = repoProductOffering.GetById(1020000001);
                    var producOffering2 = repoProductOffering.GetById(1020000002);
                    productOffering.OfferingChildTemplates = new List<ProductOffering>() { producOffering1, producOffering2 };


                    var productOfferingSpecificationOption = new ProductOfferingSpecificationOption()
                    {
                        MaxOccurs = 1,
                        MinOccurs = 1,
                        SpecifiedProductOffering = producOffering1,
                        RelatedProductOffering = productOffering,

                    };
                    var productOfferingGroupOption = new ProductOfferingGroupOption()
                    {
                        Group = new ProductOfferingGroup()
                        {
                            Description = DescriptionForProduct,
                            Names = NameForProduct
                        }
                        ,
                        MaxOccurs = 1,
                        MinOccurs = 1,
                        RelatedProductOffering = productOffering,
                    };

                    productOffering.Options = new List<ProductOfferingOption>() { productOfferingSpecificationOption, productOfferingGroupOption };

                    productOffering.ProductOfferingTimeRanges = new List<ProductOfferingTimeRange>()
                    {
                        new ProductOfferingTimeRange()
                        {
                            EndDate = DateTime.Now,
                            StartDate = DateTime.Now,
                            Status = ProductOfferingTimeRangeStatus.Active,
                            ProductOffering = productOffering,
                        }
                    };
                    try
                    {
                        repoProductOffering.Create(productOffering);
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
        public void QueryProductOffering()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoProductOffering =
                        RepositoryManager.GetRepository<IProductOfferingRepository<ProductOffering>>();

                int id = 1020000055;
                try
                {
                    var css = repoProductOffering.GetById(id);
                    if (css != null)
                    {
                        Console.Write(css.Id);
                        var product = css.OfferedProduct;

                        Assert.IsNotNullOrEmpty(product.ExternalReference);
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
        public void QueryProductOfferingGroup()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoProductOffering =
                        RepositoryManager.GetRepository<IProductOfferingRepository<ProductOffering>>();

                int groupId = 1020000001;
                try
                {
                    var css = repoProductOffering.GetByGroupId(groupId);
                    if (css != null && css.Any())
                    {
                        Console.Write(css.FirstOrDefault().Id);
                        var productoffering = css.FirstOrDefault();

                        Assert.AreEqual(productoffering.Group.Id, groupId);

                        String ids = String.Join(";", css.Select(x => x.Id));
                        
                        Console.Write(ids);
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
        public void QueryProductOffering_toDto()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoProductOffering =
                        RepositoryManager.GetRepository<IProductOfferingRepository<ProductOffering>>();

                int id = 1020000004;
                try
                {
                    var css = repoProductOffering.GetById(id);
                    if (css != null)
                    {
                        Console.Write(css.Id);
                        var dtoProduct = css.ToDto();
                        
                        Assert.AreEqual(css.Id, dtoProduct.Id);
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
