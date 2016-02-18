using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using log4net;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class RawDbAccess
    {
        static private ILog log;
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();           
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        [Test]
        public void TestMethod1()
        {
            String queryString = @"SELECT * FROM CRM_CUSTOMERS crmCust inner join CRM_CUSTOMERS_PROPERTY prop on crmCust.CustomerID=prop.CUSTOMERID WHERE prop.EXTERNAL_CUSTOMERID = @p0;";
            
            using (SqlConnection connection =  new SqlConnection(ConfigurationManager.ConnectionStrings["CRM25"].ConnectionString))
            {
                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    //Ensure the connections is open and everything is initialized
                    SqlCommand initCommand = new SqlCommand("Select GETDATE()", connection);
                    Stopwatch initC = Stopwatch.StartNew();
                    SqlDataReader initReader = initCommand.ExecuteReader();
                    while (initReader.Read())
                    {
                        Console.WriteLine("Row read");
                    }
                    initReader.Close();
                    log.InfoFormat("InitTook {0} ms", initC.ElapsedMilliseconds);

                    //Execute 10 times the magic statemnt.
                    for (int i=0; i<10; i++)
                    {
                        // Create the Command and Parameter objects.
                        SqlCommand command = new SqlCommand(queryString, connection);
                        var param = command.CreateParameter();
                        param.Value = "1001000014";
                        param.ParameterName = "@p0";
                        param.SqlDbType = System.Data.SqlDbType.VarChar;
                        //param.Size = 64;
                        command.Parameters.Add(param);
                            
                        //command.Parameters.AddWithValue("@p0", "1001000014");
                        Stopwatch c = Stopwatch.StartNew();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            log.Info("Row read");
                        }

                        reader.Close();
                        log.InfoFormat("Execution took {0} ms", c.ElapsedMilliseconds);
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                 log.Info("allDone");
            }
        }
    }
}
