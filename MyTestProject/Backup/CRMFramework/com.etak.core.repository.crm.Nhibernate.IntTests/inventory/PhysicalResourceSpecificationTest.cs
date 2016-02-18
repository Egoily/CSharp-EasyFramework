using System;
using NUnit.Framework;
using com.etak.core.model.inventory;
using com.etak.core.model;
using System.Collections.Generic;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.inventory
{
    [TestFixture]
    public class PhysicalResourceSpecificationTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }
        [Test]
        public void CreatePhysicalResourceSpecificationTest()
        {
            MultiLingualDescription NameForResource = new MultiLingualDescription
            {
                DefaultMessage = "this is a test sku",

            };
            NameForResource.Texts = new List<LanguageSpecificText>
                    {
                        { new LanguageSpecificText { Description = NameForResource, Language = ISO639LanguageCodes.aar, Text = "aar sample text 3" }},
                        { new LanguageSpecificText { Description = NameForResource, Language = ISO639LanguageCodes.zza, Text = "zza sample text 3" }}
                    };
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repoCC =
                        RepositoryManager.GetRepository<IPhysicalResourceSpecificationRepository<PhysicalResourceSpecification>>();
                    PhysicalResourceSpecification cc = new PhysicalResourceSpecification()
                    {
                        SKU = "SKUUUUUUU",
                        Name = NameForResource,
                        ModelNumber = "MN112233"
                    };

                    try
                    {
                        repoCC.Create(cc);
                        
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
        public void UpdatePhysicalResourceSpecificationTest()
        {

            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    try
                    {
                        var repoCC =
                            RepositoryManager.GetRepository<IPhysicalResourceSpecificationRepository<PhysicalResourceSpecification>>();
                        PhysicalResourceSpecification cc = repoCC.GetById(7000000000000001);

                        var repoMl =
    RepositoryManager.GetRepository<IMultiLingualDescriptionRepository<MultiLingualDescription>>();

                        var color = repoMl.GetById(7000029);

                        cc.Color = color;
                        repoCC.Update(cc);
                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
