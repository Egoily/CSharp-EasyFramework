using System;
using System.Reflection;
using com.etak.core.repository;
using log4net;
using log4net.Config;

namespace com.etak.core.app.BenefitsRenewalEngine.API
{
    public class Global : System.Web.HttpApplication
    {
        static ILog Log;

        protected void Application_Start(object sender, EventArgs e)
        {
            //Initialize Log4net
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            RepositoryManager.AddAssemby(typeof(com.etak.core.repository.crm.Nhibernate.Factory.SessionFactoryHelper).Assembly);
            //RepositoryManager.AddAssemby(typeof(Program).Assembly);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}