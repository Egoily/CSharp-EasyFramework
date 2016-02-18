using System;
using System.ServiceProcess;
using com.etak.core.repository;
using log4net;
using log4net.Core;

namespace com.etak.core.app.BenifitsRenewalEngine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static ILog log = log4net.LogManager.GetLogger("CRM-BENIFITSRENEWAL-ENGINE");
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            Level originalLevel = ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = Level.Info;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
            log.Info("Initializing Session factory");
            RepositoryManager.AddAssemby(typeof(com.etak.core.repository.crm.Nhibernate.Factory.SessionFactoryHelper).Assembly);
            RepositoryManager.AddAssemby(typeof(Program).Assembly);
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = originalLevel;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);

            try
            {
#if DEBUG
                args = new string[] { "-c" };
#endif

                if (args.Length > 0 && args[0] == "-c")
                {
                    Console.WriteLine("Press any key to continue.....");
                    Console.ReadLine();
                    Tasks tsk = new Tasks();
                    tsk.StartJob();
                    Console.WriteLine("one more click to exit");
                    Console.Read();
                    tsk.Stop();
                }
                else
                {
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[] 
                    { 
                        new Tasks() 
                    };
                    ServiceBase.Run(ServicesToRun);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
