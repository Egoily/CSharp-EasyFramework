using System;
using System.Linq;
using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using Ninject;

namespace com.etak.core.operation.manager
{
    /// <summary>
    /// Manager for the dependency injection in micro service layer
    /// </summary>
    public static class MicroServiceManager
    {
        private static readonly  IKernel Kernel = new StandardKernel();
        /// <summary>
        /// Maps the Micro service interface for the TInput and TOutput
        /// </summary>
        /// <typeparam name="TInput">The input type of the micro service</typeparam>
        /// <typeparam name="TOutput">The output type of the micro service</typeparam>
        /// <returns>fluent configuration for map</returns>
        public static RegisterMicroServicePart<TInput, TOutput> RegisterMicroService<TInput, TOutput>()
            where TInput : RequestBase
            where TOutput : ResponseBase
        {
            return new RegisterMicroServicePart<TInput, TOutput>(Kernel);
        }

        /// <summary>
        /// Map the microservices with the specific Types
        /// </summary>
        /// <param name="typeOfInputParameter">The input type of the micro service </param>
        /// <param name="typeOfOuputParameter">The output type of the micro service</param>
        /// <param name="implementationType">The implementation of the micro service</param>
        internal static void RegisterMicroService(Type typeOfInputParameter, Type typeOfOuputParameter, Type implementationType)
        {
            var microServiceGeneric = typeof (IMicroService<,>);
            var microServiceGenericWithTypes = microServiceGeneric.MakeGenericType(new[] { typeOfInputParameter, typeOfOuputParameter });

            Kernel.Rebind(microServiceGenericWithTypes).To(implementationType);
        }


        /// <summary>
        /// Register all Microservice with specific assembly
        /// </summary>
        /// <param name="assembly">Assembly to re</param>
        static public void RegisterMicroServicesByAssemby(Assembly assembly)
        {
            var microServiceInterface = typeof(IMicroService<,>);

            foreach (var type in assembly.GetTypes())
            {
                var interfacesOfType = type.GetInterfaces().
                    Where(
                        x =>
                            x.IsGenericType &&
                            x.GetGenericArguments().Count() == microServiceInterface.GetGenericArguments().Count())
                    .ToList();

                foreach (var interfaceImplemented in interfacesOfType)
                {
                    var genericArguments = interfaceImplemented.GetGenericArguments();
                    if (microServiceInterface.IsAssignableFrom(interfaceImplemented.GetGenericTypeDefinition()))
                    {
                        RegisterMicroService(genericArguments[0], genericArguments[1], type);
                    }
                }
            }
        }



        /// <summary>
        /// Maps the Micro service interface for the TInput and TOutput, providing a logged wrapper/decorator
        /// </summary>
        /// <typeparam name="TInput">The input type of the micro service</typeparam>
        /// <typeparam name="TOutput">The output type of the micro service</typeparam>
        /// <returns>fluent configuration for map</returns>
        public static RegisterTrazedMicroServicePart<TInput, TOutput> RegisterTrazedMicroServiceWith<TInput, TOutput>()
            where TInput : RequestBase
            where TOutput : ResponseBase
        {
            return new RegisterTrazedMicroServicePart<TInput, TOutput>(Kernel);
        }

        /// <summary>
        /// gets the configured implementation of the microservices for
        /// the types TInput, TOutput
        /// </summary>
        /// <typeparam name="TInput">The input type of the micro service</typeparam>
        /// <typeparam name="TOutput">The output type of the micro service</typeparam>
        /// <returns>The implementation for the types</returns>
        public static IMicroService<TInput, TOutput> GetMicroService<TInput, TOutput>()
            where TInput : RequestBase
            where TOutput : ResponseBase
        {
            return Kernel.Get<IMicroService<TInput, TOutput>>();
        }
    }
}
