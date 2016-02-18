using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.dtoConverters;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using NUnit.Framework;
using com.etak.core.model.inventory;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.reveneu
{
    [TestFixture]
    public class IProductRepositoryTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }    

        [Test]
        public void ProductQuery()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var prodRepo = RepositoryManager.GetRepository<IProductRepository<Product>>();
                        Product productId1 = prodRepo.GetById(1003000004);
                        Console.Write(productId1.Id);

                        foreach (var prodRel in productId1.ProductRelationDependencies)
                        {
                            Console.Write(prodRel.ConflictResolutionStrategy);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ABC" + ex.Message);
                    }
                }
            }
        }

        [Test]
        public void CreateProduct()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    Int32 dealerId = 170000;

                    IDealerInfoRepository<DealerInfo> dealerRepo = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
                    DealerInfo dealer = dealerRepo.GetByDealerIdAndCache(dealerId).FirstOrDefault();

                    IPackageInfoRepository<PackageInfo> packageRepo = RepositoryManager.GetRepository<IPackageInfoRepository<PackageInfo>>();
                    PackageInfo package = packageRepo.GetPackageInfoForDealerId(dealerId).FirstOrDefault();

                    //IRmPromotionGroupInfoRepository<RmPromotionGroupInfo> promoGrouprepo = RepositoryManager.GetRepository<IRmPromotionGroupInfoRepository<RmPromotionGroupInfo>>();
                    //RmPromotionGroupInfo promoGroup = promoGrouprepo.GetByMvnos(new List<Int32> { dealerId }).FirstOrDefault();

                    //IRmPromotionPlanInfoRepository<RmPromotionPlanInfo> promoPlanRepo = RepositoryManager.GetRepository<IRmPromotionPlanInfoRepository<RmPromotionPlanInfo>>();
                    //RmPromotionPlanInfo promoPlan = promoPlanRepo.GetAllRmPromotionPlanForDealerId(dealerId).FirstOrDefault();

                    IBundleInfoRepository<BundleInfo> bundleRepo = RepositoryManager.GetRepository<IBundleInfoRepository<BundleInfo>>();
                    BundleInfo bundle = bundleRepo.GetAll().FirstOrDefault();

                    var prodRepo = RepositoryManager.GetRepository<IProductRepository<Product>>();

                    Product incomProduct = prodRepo.GetById(1003000010);

                    Product ProductId1 = new Product
                    {
                        AssociatedBundle = bundle,
                        AssociatedPackage = package,
                        //AssociatedPrmotionGroup = promoGroup,
                        //AssociatedPrmotionPlan = promoPlan,
                        VMO = dealer,
                        Type = new ProductType { Description = " Test product description" },
                        ProductRelationDependencies = new List<ProductDependencyRelation>(),
                    };

                    MultiLingualDescription NameForProduct = new MultiLingualDescription
                    {
                        DefaultMessage = "this is a test product",

                    };
                    NameForProduct.Texts = new List<LanguageSpecificText>
                    {
                        { new LanguageSpecificText { Description = NameForProduct, Language = ISO639LanguageCodes.aar, Text = "aar sample text 3" }},
                        { new LanguageSpecificText { Description = NameForProduct, Language = ISO639LanguageCodes.zza, Text = "zza sample text 3" }}
                    };
                    ProductId1.Names = NameForProduct;

                    MultiLingualDescription DescriptionForProduct = new MultiLingualDescription
                    {
                        DefaultMessage = "this is a test product",

                    };
                    DescriptionForProduct.Texts = new List<LanguageSpecificText>
                    {
                        { new LanguageSpecificText { Description = DescriptionForProduct, Language = ISO639LanguageCodes.aar, Text = "aar sample text 2" }},
                        { new LanguageSpecificText { Description = DescriptionForProduct, Language = ISO639LanguageCodes.zza, Text = "zza sample text 2" }}
                    };
                    ProductId1.Description = DescriptionForProduct;

                    prodRepo.Create(ProductId1);
                    trx.Commit();

                }
            }
        }

        [Test]
        public void CreatePhysicalProduct()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    Int32 dealerId = 170000;

                    IDealerInfoRepository<DealerInfo> dealerRepo = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
                    DealerInfo dealer = dealerRepo.GetByDealerIdAndCache(dealerId).FirstOrDefault();

                    var prodRepo = RepositoryManager.GetRepository<IProductRepository<PhysicalProduct>>();
                    var prodResSpecRepo = RepositoryManager.GetRepository<IPhysicalResourceSpecificationRepository<PhysicalResourceSpecification>>();
                    var spec = prodResSpecRepo.GetAll().FirstOrDefault();

                    PhysicalProduct incomProduct = prodRepo.GetById(1003000010);

                    

                    PhysicalProduct ProductId1 = new PhysicalProduct
                    {

                        VMO = dealer,
                        Type = new ProductType { Description = " Test product description" },
                        ProductRelationDependencies = new List<ProductDependencyRelation>(),
                    };

                    MultiLingualDescription NameForProduct = new MultiLingualDescription
                    {
                        DefaultMessage = "this is a test product",

                    };
                    NameForProduct.Texts = new List<LanguageSpecificText>
                    {
                        { new LanguageSpecificText { Description = NameForProduct, Language = ISO639LanguageCodes.aar, Text = "aar sample text 3" }},
                        { new LanguageSpecificText { Description = NameForProduct, Language = ISO639LanguageCodes.zza, Text = "zza sample text 3" }}
                    };
                    ProductId1.Names = NameForProduct;
                    MultiLingualDescription DescriptionForProduct = new MultiLingualDescription
                    {
                        DefaultMessage = "this is a test product",

                    };
                    DescriptionForProduct.Texts = new List<LanguageSpecificText>
                    {
                        { new LanguageSpecificText { Description = DescriptionForProduct, Language = ISO639LanguageCodes.aar, Text = "aar sample text 2" }},
                        { new LanguageSpecificText { Description = DescriptionForProduct, Language = ISO639LanguageCodes.zza, Text = "zza sample text 2" }}
                    };
                    ProductId1.Description = DescriptionForProduct;
                    ProductId1.PhysicalResourceSpecification = spec;
                    prodRepo.Create(ProductId1);
                    trx.Commit();

                }
            }
        }

        [Test]
        public void AssignIncompatibleProduct()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var prodRepo = RepositoryManager.GetRepository<IProductRepository<Product>>();
                    Product ProductId1 = prodRepo.GetById(1003000000);
                    Product incomProduct = prodRepo.GetById(1003000010);
                    ProductId1.ProductRelationDependencies.Add(
                        new ProductDependencyRelation
                        {   
                            SourceProduct = ProductId1,
                            RelatedProduct = incomProduct,
                            ConflictResolutionStrategy = ProductConflictResolutionsStrategies.Reject,
                            MinOccurs = 0,
                            MaxOccurs = 0,
                            RelationType = ProductRelationTypes.Incompatible
                        });

                    prodRepo.Update(ProductId1);
                    Console.Write(ProductId1.Id);
                }
            }
        }

         [Test]
        public void CreateProductHierachy()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    try
                    {
                        Int32 dealerId = 170000;

                        IDealerInfoRepository<DealerInfo> dealerRepo = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
                        DealerInfo dealer = dealerRepo.GetById(dealerId);

                        var prodRepo = RepositoryManager.GetRepository<IProductRepository<Product>>();

                        Product ProductId1 = new Product
                        {
                            VMO = dealer,
                            Type = new ProductType { Description = " Test product description" },
                            ProductRelationDependencies = new List<ProductDependencyRelation>(),
                        };

                        Product ProductId2 = new Product
                        {
                            VMO = dealer,
                            Type = new ProductType { Description = " Test product description" },
                            ProductRelationDependencies = new List<ProductDependencyRelation>(),
                        };

                        Product ProductId3 = new Product
                        {
                            VMO = dealer,
                            Type = new ProductType { Description = " Test product description" },
                            ProductRelationDependencies = new List<ProductDependencyRelation>(),
                        };

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
                        DescriptionForProduct.Texts = new List<LanguageSpecificText>
                    {
                        { new LanguageSpecificText { Description = DescriptionForProduct, Language = ISO639LanguageCodes.aar, Text = "aar sample text 2" }},
                        { new LanguageSpecificText { Description = DescriptionForProduct, Language = ISO639LanguageCodes.zza, Text = "zza sample text 2" }}
                    };

                        ProductId1.Names = NameForProduct;
                        ProductId1.Description = DescriptionForProduct;
                        ProductId2.Names = NameForProduct;
                        ProductId2.Description = DescriptionForProduct;
                        ProductId3.Names = NameForProduct;
                        ProductId3.Description = DescriptionForProduct;

                        prodRepo.Create(ProductId1);
                        prodRepo.Create(ProductId2);
                        prodRepo.Create(ProductId3);
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
         public void ProductQueryToDto()
         {
             using (var conn = RepositoryManager.GetNewConnection())
             {
                 using (var tran = conn.BeginTransaction())
                 {
                     try
                     {
                         int productId = 1020000669;
                         var prodRepo = RepositoryManager.GetRepository<IProductRepository<Product>>();
                         Product productId1 = prodRepo.GetById(productId);
                         if (productId1 != null)
                         {
                             Console.Write(productId1.Id);

                             var dto = productId1.ToDto();

                             Assert.AreEqual(dto.Id, productId1.Id);
                         }
                         
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine("ABC" + ex.Message);
                     }
                 }
             }
         }
    }
}
