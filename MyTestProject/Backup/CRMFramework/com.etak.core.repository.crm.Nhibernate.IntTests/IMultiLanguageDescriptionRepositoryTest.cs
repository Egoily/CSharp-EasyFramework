using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class IMultiLanguageDescriptionRepositoryTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreateMultiLingualDescription()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var chargeRepo = RepositoryManager.GetRepository<IMultiLingualDescriptionRepository<MultiLingualDescription>>();
                    MultiLingualDescription c = new MultiLingualDescription
                    {
                        DefaultMessage = "Default Text",
                    };

                    c.Texts = new List<LanguageSpecificText>
                        {
                            {new LanguageSpecificText () { Description = c, Language = ISO639LanguageCodes.eng, Text = "My Text" } },
                            {new LanguageSpecificText () { Description = c, Language = ISO639LanguageCodes.spa, Text = "My Texto" } }
                        };

                    chargeRepo.Create(c);

                    trx.Commit();
                }
            }
        }
 
        [Test]
        public void QueryMultiLingualDescription()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var chargeRepo = RepositoryManager.GetRepository<IMultiLingualDescriptionRepository<MultiLingualDescription>>();
                    var description = chargeRepo.GetById(1020000001);

                    foreach(var text in description.Texts)
                    {
                        Console.WriteLine(" Lang: {0} Text:{1}", text.Language, text.Text);
                    }
                    trx.Commit();
                }
            }
        }
    }
}
