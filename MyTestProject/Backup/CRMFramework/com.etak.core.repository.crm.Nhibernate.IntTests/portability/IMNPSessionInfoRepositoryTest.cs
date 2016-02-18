using System;
using System.Linq;
using com.etak.core.model;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.portability
{
    [TestFixture]
    public class IMNPSessionInfoRepositoryTest :
        AbstractRepositoryTest<IMNPSessionInfoRepository<MNPSessionInfo>, MNPSessionInfo, Int32>

    {
        [TestFixtureSetUp]
        public static void Init()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public void GetLastNByOperatorCode()
        {
            String opCode = "813";
            DoTransacted(repo => 
            { 
                var prod = repo.GetLastNByOperatorCode(opCode, 1);
            });

            
        }
        [Test]
        public void CreateNewSession()
        {
            MNPSessionInfo newSession = new MNPSessionInfo()
            {
                CreateTime = DateTime.Now,
                OperatorCode = "813",
                SessionId = "repositoryTest"
            };

            DoTransacted(repo => repo.Create(newSession));
        }

        [Test]
        public void DeleteSession()
        {
            String opCode = "813";
            int lastSessions = 1;

            DoTransacted(repo =>
            {
                var session = repo.GetLastNByOperatorCode(opCode, lastSessions).First();
                repo.Delete(session);
            });


        }

        [Test]
        public void GetByUserName()
        {
            DoTransacted(repo =>
            {
                var session = repo.GetLastNByOperatorCodeAndUsername("813", null, 2);
                Assert.IsNotNull(session.FirstOrDefault().ID);
            });
        }

        protected override Int32 ExistingId
        {
            get { return (123456); }
        }

    }
}
