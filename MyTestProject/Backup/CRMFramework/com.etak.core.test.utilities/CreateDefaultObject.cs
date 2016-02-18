using System;
using System.Collections;
using FizzWare.NBuilder;
using PropertyInfo = System.Reflection.PropertyInfo;

namespace com.etak.core.test.utilities
{
    /// <summary>
    /// Creates an instance of an object 
    /// </summary>
    public class CreateDefaultObject
    {

        private static readonly Type BuilderClassType = typeof(Builder<>);
       
        /// <summary>
        /// Creates an instance of an object with type T, filling all the "primitive" properties (that means, Strings and DateTimes as well)
        /// and complex objects with null.
        /// </summary>
        /// <typeparam name="T">Type of object to be created</typeparam>
        /// <param name="nMembersInLists">By default is 2. Number of elements in the properties that are List</param>
        /// <returns>an object of type T filled</returns>
        public static T Create<T>(int nMembersInLists = 2)
        {
            var returnObject = Builder<T>.CreateNew().Build();

            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                #region Lists 
                if (typeof(IEnumerable).IsAssignableFrom(pi.PropertyType))
                {
                    var piProperty = pi.PropertyType.GetProperties()[0];

                    if (!piProperty.PropertyType.IsPrimitive && !piProperty.PropertyType.IsAbstract)
                    {
                        Type[] args = { piProperty.PropertyType };

                        var genericBuilderType = BuilderClassType.MakeGenericType(args);

                        //create a new instance of Builder<entityType>
                        var builder = Activator.CreateInstance(genericBuilderType);

                        //retrieve the "CreateNew" method, which belongs to Builder<T> class
                        var createNewMethodInfo = builder.GetType()
                            .GetMethod("CreateListOfSize", new Type[] { typeof(int) });

                        //invoke "CreateNew" from our builder instance which gives us an ObjectBuilder<T>, so now an ObjectBuilder<entityType> (well as an ISingleObjectBuilder<entityType>, but... who minds ;))
                        var objectBuilder = createNewMethodInfo.Invoke(builder, new object[] { nMembersInLists });

                        //retrieve the "Build" method, which belongs to ObjectBuilder<T> class
                        var buildMethodInfo = objectBuilder.GetType().GetMethod("Build");

                        //finally, invoke "Build" from our ObjectBuilder<entityType> instance, which will give us... our entity !
                        var result = buildMethodInfo.Invoke(objectBuilder, null);

                        returnObject.GetType().GetProperty(pi.Name).SetValue(returnObject, result, null);
                    }
                }
                #endregion

                #region ETAK objects
                else if (pi.PropertyType.Namespace.StartsWith("com.etak") && !pi.PropertyType.IsAbstract && pi.CanWrite)
                {
                    var builderForTypeStaticMethod = BuilderClassType.MakeGenericType(pi.PropertyType).GetMethod("CreateNew");

                    var objectBuilder = builderForTypeStaticMethod.Invoke(null,  new object[] {  });

                    var entityCreated = objectBuilder.GetType().GetMethod("Build").Invoke(objectBuilder, new object[] { });

                    var setMethod = pi.GetSetMethod();
                      setMethod.Invoke(returnObject, new object[] { entityCreated} );
                }
                #endregion
            }

            return returnObject;
        }
    }
}
