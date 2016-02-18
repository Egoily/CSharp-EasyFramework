using System;
using System.Linq;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.crm.operation;
using NHibernate;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.operation
{


    [TestFixture]
    public class BusinessOperationExecutionTest :
        AbstractRepositoryTest<IBusinessOperationExecutionRepository<BusinessOperationExecution>, BusinessOperationExecution, Int64>
    {
        protected override Int64 ExistingId
        {
            get { return 1; }
        }

        [TestFixtureSetUp]
        public static void EnsureInitializeSessionFactoryAndLogging()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public void SaveParents()
        {
            Int64 opId = 0;
            Action<IBusinessOperationExecutionRepository<BusinessOperationExecution>> func = repo =>
            {
                BusinessOperationExecution grandParent = CreateValid();
                BusinessOperationExecution parent = CreateValid();
                BusinessOperationExecution child1 = CreateValid();
                BusinessOperationExecution child2 = CreateValid();
                BusinessOperationExecution grandChild1_1 = CreateValid();

                parent.ParentBusinessOperation = grandParent;
                child1.ParentBusinessOperation = parent;
                child2.ParentBusinessOperation = parent;
                grandChild1_1.ParentBusinessOperation = child1;

                repo.Create(grandChild1_1);
                repo.Create(parent);
                repo.Create(grandParent);
                repo.Create(child1);
                repo.Create(child2);



                //grandParent.OperationCode = "grandParent";
                //parent.OperationCode = "parent";
                //child1.OperationCode = "child1";
                //child2.OperationCode = "child2";
                //grandChild1_1.OperationCode = "grandChild1_1";

                //grandParent.ProcessorDiscriminator = "NEW";
                //parent.ProcessorDiscriminator = "NEW";
                //child.ProcessorDiscriminator = "NEW";

                //repo.Update(grandParent);
                //repo.Update(parent);
                //repo.Update(child);
                //repo.Create(grandParent);
                //repo.Create(parent);
                //repo.Create(child);

                //grandParent.OperationCode = "grandParent_B";
                //parent.OperationCode = "parent_B";
                //child1.OperationCode = "child1_B";
                //child2.OperationCode = "child2_B";
                //grandChild1_1.OperationCode = "grandChild1_1_B";
                opId = grandParent.Id;
            };

            DoTransacted(func);


        }

        [Test]
        public void GetByDatesAndStatus()
        {
            DoTransacted(repo => repo.GetOperationsWithinDatesWithResultTypesIn(DateTime.Now.AddDays(1), DateTime.Now.AddDays(-1), new[] { ResultTypes.Queued }).Any());
        }

        [Test]
        public void Thing()
        {
            Int64 id = 6145408875504926721;
            using (var conn = RepositoryManager.GetNewConnection())
            {
                IBusinessOperationExecutionRepository<BusinessOperationExecution> repo =
               RepositoryManager.GetRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
                var bizop = repo.GetById(id);
                if (bizop != null)
                {
                    Console.WriteLine(bizop.Id);

                }
                else
                {
                    Console.WriteLine("Caca");
                }
            }
        }

        [Test]
        public void Chicken()
        {
            
            using (var conn = RepositoryManager.GetNewConnection())
            {
                IBusinessOperationExecutionRepository<BusinessOperationExecution> repo =
                RepositoryManager.GetRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();

                //Ensure create valid is valid
                using (var trx = conn.BeginTransaction())
                {
                    BusinessOperationExecution parent = CreateValid();
                    repo.Create(parent);
                    trx.Commit();
                }


                using (var trx = conn.BeginTransaction())
                {
                    try
                    {
                        BusinessOperationExecution parent = CreateValid();
                        parent.StartTime = DateTime.MinValue;
                        repo.Create(parent);
                        trx.Commit();
                    }
                    catch (Exception)
                    {
                        Log.Debug("Throws exception as expected");
                    }
                }

                try
                {
                    using (var trx = conn.BeginTransaction())
                    {
                        BusinessOperationExecution parent = CreateValid();
                        repo.Create(parent);
                        trx.Commit();
                    }
                }
                catch(Exception)
                {
                    //After an exception, the session is in an invalid state and should just be disposed. It will always throw exception
                    Log.Debug("This should happen because there's an exception occured that causes all the transaction throws exception");
                    //Log.Debug("This should not happen as we rollbacked the trx");
                    //throw;
                }
            }
        }

        private BusinessOperationExecution CreateValid()
        {
            BusinessOperationExecution op = new BusinessOperationExecution();
            op.StartTime = DateTime.Now;
            op.EndDate = DateTime.Now.AddDays(1);
            op.ResultType = ResultTypes.Ok;
            //op.OperationCode = "DEFAULT";
            //op.ProcessorDiscriminator = "DEFAULT";
            return (op);
        }


    }
}
