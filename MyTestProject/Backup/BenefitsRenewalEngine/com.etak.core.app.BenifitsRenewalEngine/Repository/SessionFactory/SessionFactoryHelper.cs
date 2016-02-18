using com.etak.core.app.BenifitsRenewalEngine.DTO;
using Ninject.Modules;

namespace com.etak.core.app.BenifitsRenewalEngine.Repository.SessionFactory
{
    public class SessionFactoryHelper
    {
    }

    public class RealHelper : NinjectModule
    {
        public override void Load()
        {

            Bind<IBenifitsRenewalRepository<RenewalCandidates>>().To<BenifitsRenewalRepositoryNH<RenewalCandidates>>();
        }
    }
}
