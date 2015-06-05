using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}
