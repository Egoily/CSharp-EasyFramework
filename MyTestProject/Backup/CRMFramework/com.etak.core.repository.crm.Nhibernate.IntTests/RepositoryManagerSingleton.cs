using com.etak.core.repository.crm.Nhibernate.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    public class RepositoryManagerSingleton
    {
        private static RepositoryManagerSingleton instance;

        private RepositoryManagerSingleton()
        {
            RepositoryManager.AddAssemby(typeof(SessionFactoryHelper).Assembly);
        }

        public static RepositoryManagerSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RepositoryManagerSingleton();
                }
                return instance;
            }
        }
    }
}
