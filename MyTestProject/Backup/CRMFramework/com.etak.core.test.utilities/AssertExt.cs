using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace com.etak.core.test.utilities
{
    /// <summary>
    /// Core Assert
    /// </summary>
    public class AssertExt : NUnit.Framework.Assert
    {
        /// <summary>
        /// If objects are equal return true
        /// </summary>
        /// <param name="self"></param>
        /// <param name="to"></param>
        /// <param name="ignore"></param>
        public static void ObjectPropertiesAreEqual(Object self, Object to, params string[] ignore)
        {
            #region Check nulls
            if (self == null && to == null)
            {
                return;
            }

            if (self == null || to == null)
            {
                throw new Exception("The Objects aren't equal.");
            } 
            #endregion

            if (self is IList)
            {
                CheckList(self, to);
            }
            else
            {
                Type typeSelf = self.GetType();

                List<string> ignoreList = new List<string>(ignore);
                //If we need to specify a different criteria to get the properties...
                //foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                foreach (PropertyInfo pi in typeSelf.GetProperties())
                {
                    if (ignoreList.Contains(pi.Name)) continue;
                    object selfValue = typeSelf.GetProperty(pi.Name).GetValue(self, null);
                    object toValue = typeSelf.GetProperty(pi.Name).GetValue(to, null);

                    #region Check nulls
                    if (selfValue == null && toValue == null)
                        continue;

                    if (selfValue == null || toValue == null)
                    {
                        throw new Exception("The Objects aren't equal.");
                    } 
                    #endregion

                    if (selfValue.GetType().IsPrimitive || selfValue is String || selfValue is DateTime)
                    {
                        if (selfValue != toValue && (!selfValue.Equals(toValue)))
                        {
                            throw new Exception("The Objects aren't equal.");
                        }
                    }
                    else if (selfValue is IList)
                    {
                        CheckList(selfValue, toValue);
                    }
                    else
                    {
                        ObjectPropertiesAreEqual(selfValue, toValue);
                    }
                }   
            }
        }

        /// <summary>
        /// Check if the objects are equal lists 
        /// </summary>
        /// <param name="selfObject"></param>
        /// <param name="toObject"></param>
        private static void CheckList(object selfObject, object toObject)
        {
            var selfList = (IList) selfObject;
            var toList = (IList) toObject;

            for (var pos = 0; pos < selfList.Count; pos++)
            {
                ObjectPropertiesAreEqual(selfList[0], toList[0]);
            }
        }

        /// <summary>
        /// Compare all the properties with the same name between two Properties.
        /// They are compared without taking into account the different between capital letters
        /// </summary>
        /// <typeparam name="T1">The type of the first object</typeparam>
        /// <typeparam name="T2">The type of the second object</typeparam>
        /// <param name="self">The first object to be compared</param>
        /// <param name="to">The second object to be compared</param>
        public static void ComparePropertiesByName<T1, T2>(T1 self, T2 to)
        {
            Type typeSelf = self.GetType();
            Type typeTo = to.GetType();
            
            //Get a list of all the properties in both sides
            Dictionary<String, String> selfProperties = typeSelf.GetProperties().ToDictionary(x => x.Name.ToUpper(), x => x.Name);
            Dictionary<String, String> toProperties = typeTo.GetProperties().ToDictionary(x => x.Name.ToUpper(), x => x.Name);

            foreach (var piName in selfProperties)
            {
                String upperName = piName.Key;
                if (toProperties.ContainsKey(upperName))
                {
                    String selfRealName = piName.Value;
                    String toRealName = toProperties[upperName];
                    object selfValue = typeSelf.GetProperty(selfRealName).GetValue(self, null);
                    object toValue = typeTo.GetProperty(toRealName).GetValue(to, null);

                    if (selfValue.GetType().IsPrimitive || selfValue is String || selfValue is DateTime)
                    {
                        if (selfValue != toValue && (!selfValue.Equals(toValue)))
                        {
                            throw new Exception(string.Format("Property {0} is not equal between the objects. \nValue 1: [{1}] \nValue 2: [{2}]",
                                    piName, selfValue, toValue));
                        }
                    }
                }
            }

        }
    }
}
