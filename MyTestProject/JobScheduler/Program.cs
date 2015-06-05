using System.Reflection;

namespace JobScheduler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var currentMethod = MethodBase.GetCurrentMethod();
            Tasks tasks = new Tasks();
            tasks.Start();
        }
    }
}