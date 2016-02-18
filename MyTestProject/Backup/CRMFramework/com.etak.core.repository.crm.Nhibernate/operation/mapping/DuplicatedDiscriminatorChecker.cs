using System;
using System.Collections.Generic;
using com.etak.core.model.operation;


namespace com.etak.core.repository.crm.Nhibernate.operation.mapping
{
    internal static class DuplicatedDiscriminatorChecker
    {
        private static readonly IDictionary<String, Type> BizOpsDict = new Dictionary<string, Type>();
        private static readonly IDictionary<String, Type> OrderDict = new Dictionary<string, Type>();

        private static void AddToColl<T>(String discriminator, IDictionary<String, Type> dict)
        {
            try
            {
                dict.Add(discriminator, typeof(T));
            }
            catch (Exception ex)
            {
                Type existingType = dict[discriminator];
                //Due to the fact that there are 3 session factories (which I think we shouldn't) 
                //The same type is loaded for multiple session factories, if it's the same
                //type we can ignore it.
                if (existingType != typeof(T))
                    throw new Exception(String.Format("The discriminator {0} was declared for multy types: '{1}' and '{2}'", discriminator, typeof(T).Name, existingType.Name), ex);
            }
        }

        internal static void AddBizOp<T>(String discriminator) where T : BusinessOperation
        {
            AddToColl<T>(discriminator, BizOpsDict);
        }



        internal static void AddOrder<T>(String discriminator) where T : Order
        {
            AddToColl<T>(discriminator,OrderDict);
        }

        internal static ICollection<Type> GetAllOrders()
        {
            return OrderDict.Values;
        }

        internal static ICollection<Type> GetAllBizOps()
        {
            return BizOpsDict.Values;
        }

       
    }
}
