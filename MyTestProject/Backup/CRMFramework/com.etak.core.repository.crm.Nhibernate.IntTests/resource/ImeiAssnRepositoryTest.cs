using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.resource
{
    [TestFixture()]
    public class ImeiAssnRepositoryTests : AbstractRepositoryTest<IImeiAssnRepository<ImeiAssn>,ImeiAssn,Int32>
    {
        [Test]
        public override void GetById()
        {
            DoTransacted(repo =>
            {
                ImeiAssn nInfo =
                repo.GetById(ExistingId);
                if (nInfo != null)
                {
                    Console.Write("Accessing ImeiAssn: ");

                    Console.WriteLine(" Value " + nInfo.Imei);
                }
            });
        }

        [Test]
        public void GetByImei()
        {
            DoTransacted(repo =>
            {
                ImeiAssn nInfo =
                repo.GetByImei("354081052446600").FirstOrDefault();
                if (nInfo != null)
                {
                    Console.Write("Accessing NumberInfo: ");
                    Console.WriteLine(" Value " + nInfo.Imei);
                }
            });
        }

        protected override int ExistingId
        {
            get { return (1955000958); }
        }
    }
}
