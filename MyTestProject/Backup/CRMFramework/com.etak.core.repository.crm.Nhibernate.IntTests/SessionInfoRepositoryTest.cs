
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.test.utilities;
using FluentNHibernate.Conventions;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    
    [TestFixture]
    public class SessionInfoRepositoryTest :
        AbstractRepositoryTest<ISessionInfoRepository<SessionInfo>, SessionInfo, Int32>
    {
        [TestFixtureSetUp]
        public static void Init()
        {
        }

        [Test]
        public void GetBySessionInfo()
        {
            //DoTransacted(repo =>
            //{
            //    var prod = repo.GetById("aa");
            //});
        }

        [Test]
        public void Cose()
        {
            foreach (var counter in new List<Int32> { 1, 2, 3 })
            {
                Log.InfoFormat("Running {0} iteration", counter);
                IEnumerable<SessionInfo> sessionInfosResult = null;
                Array sessionInfosResultArray = null;
                DoTransacted(repo =>
                {
                    sessionInfosResult = repo.GetBySessionId("6211004940368609281");
                    sessionInfosResultArray = sessionInfosResult.ToArray();
                });
                 
                Assert.IsTrue(sessionInfosResultArray != null);
            }
        }

        //[Test]
        //public void Save()
        //{
        //    foreach (var counter in new List<Int32> { 1, 2, 3 })
        //    {
        //        SessionInfo sessionInfoResult = null;
        //        Log.InfoFormat("Running {0} iteration", counter);
        //        var sessionInfo = new SessionInfo();
        //        sessionInfo.IdleTimeoutDate = DateTime.UtcNow;
        //        sessionInfo.IdleTimoutMinutes = 1;
        //        sessionInfo.LoginInfo = CreateDefaultObject.Create<LoginInfo>(); ;
        //        DoTransacted(repo =>
        //        {
        //            sessionInfoResult = repo.Create(sessionInfo);
        //        });

        //        Assert.IsTrue(sessionInfoResult.SessionID.IsNotEmpty());
        //    }
        //}


        protected String SessionId
        {
            get { return ("100000"); }
        }

        protected override int ExistingId
        {
            get { return (1); }
        }
    }
}
