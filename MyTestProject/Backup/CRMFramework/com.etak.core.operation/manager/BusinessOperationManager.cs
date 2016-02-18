using System;
using System.Linq;
using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using Ninject;
using System.Collections.Generic;

namespace com.etak.core.operation.manager
{
    /// <summary>
    /// Manager for the business operation injection in micro service layer
    /// </summary>
    public static class BusinessOperationManager
    {
        private static readonly IDictionary<Int32, IKernel> KernesOfDealers = new Dictionary<Int32, IKernel>();
        private static readonly Type TypeOfBusinessOperationInterface = typeof (IBusinessOperation<,,,>);
        private static readonly Type TypeOfCoreBusinessOperationInterface = typeof(ICoreBusinessOperation<,>);
        private static readonly Type TypeOfDTOBusinessOperationInterface = typeof(IDTOBusinessOperation<,>);

        private static IKernel GetKernelForDealer(int dealerId)
        {
            IKernel kernel;
            if (!KernesOfDealers.TryGetValue(dealerId, out kernel))
            {
                kernel = new StandardKernel();
                KernesOfDealers.Add(dealerId, kernel);
            }
            return kernel;
        }

        /// <summary>
        /// Starts binding a business operation just based on the types, the implementation 
        /// is requested by the BusinessOperationBuildPart
        /// </summary>
        /// <typeparam name="TReq">The type of the Request in DTO form</typeparam>
        /// <typeparam name="TRes">The type of the Response in DTO form</typeparam>
        /// <typeparam name="TReqCore">The type of the Request in core form</typeparam>
        /// <typeparam name="TResCore">The type of the Response in core form</typeparam>
        /// <param name="dealerId">The dealer to which this bindings apply</param>
        /// <returns>the helper builder to complete the binding</returns>
        public static BusinessOperationBuildPart<TReq, TRes, TReqCore, TResCore> BindBusinessOperation<TReq, TRes, TReqCore, TResCore>(Int32 dealerId) 
            where TReq : RequestBaseDTO  
            where TRes : ResponseBaseDTO  
            where TReqCore : RequestBase 
            where TResCore : ResponseBase
        {
            return new BusinessOperationBuildPart<TReq, TRes, TReqCore, TResCore>(dealerId);
        }

        /// <summary>
        /// Auto register all BusinessOperation implementaions in the Assembly provided, if bind alredy exist then Rebind
        /// </summary>
        /// <param name="assmb">the assembly to load the types to bind</param>
        /// <param name="dealerId">the dealer to which the assemblies are loaded</param>
        public static void RebindTypesInAssemblyForDealer(Assembly assmb, Int32 dealerId)
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            foreach (Type type in assmb.GetTypes())
            {
                Type ifIntype = type.GetInterfaces().FirstOrDefault(iftype => iftype.IsGenericType && iftype.GetGenericTypeDefinition() == TypeOfBusinessOperationInterface);
              
                if (ifIntype != null)
                {
                    Type [] typesOfMessages = ifIntype.GetGenericArguments();
                    Type tRequestDTO = typesOfMessages[0];
                    Type tResponseDTO = typesOfMessages[1];
                    Type tRequestCore = typesOfMessages[2];
                    Type tResponseCore = typesOfMessages[3];

                    Type bizOpIf = TypeOfBusinessOperationInterface.MakeGenericType(new[]
                                            {   tRequestDTO, 
                                                tResponseDTO, 
                                                tRequestCore, 
                                                tResponseCore
                                            });

                    kernel.Rebind(bizOpIf).To(type);

                    Type bizDtoIf = TypeOfDTOBusinessOperationInterface.MakeGenericType(new[]
                    {
                            tRequestDTO, tResponseDTO
                    });
                    kernel.Rebind(bizDtoIf).To(type);

                    Type bizCoreIf =
                      TypeOfCoreBusinessOperationInterface.MakeGenericType(new[] { tRequestCore, tResponseCore });
                    kernel.Rebind(bizCoreIf).To(type);
                }
            }
        }

        /// <summary>
        /// Rebinds a DTO interface of given types to the implementation 
        /// </summary>
        /// <typeparam name="TRequestDTO">The type of the DTO request of the interface</typeparam>
        /// <typeparam name="TResponseDTO">The type of the DTO response of the interface</typeparam>
        /// <typeparam name="TImplementation">The type implementing the interface</typeparam>
        /// <param name="dealerId">the dealer id to which this implementation needs to be rebind</param>
        public static void BindDTOInterface<TRequestDTO, TResponseDTO, TImplementation>(int dealerId)
            where TRequestDTO : RequestBaseDTO 
            where TResponseDTO : ResponseBaseDTO 
            where TImplementation : IDTOBusinessOperation<TRequestDTO, TResponseDTO>
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            kernel.Bind<IDTOBusinessOperation<TRequestDTO, TResponseDTO>>().To<TImplementation>();
        }


        /// <summary>
        /// Binds a Core interface of given types to the implementation 
        /// </summary>
        /// <typeparam name="TRequestCore">The type of the Core request of the interface</typeparam>
        /// <typeparam name="TResponseCore">The type of the Core response of the interface</typeparam>
        /// <typeparam name="TImplementation">The type implementing the interface</typeparam>
        /// <param name="dealerId">the dealer id to which this implementation needs to be rebind</param>
        public static void BindCoreInterface<TRequestCore, TResponseCore, TImplementation>(int dealerId) 
            where TRequestCore : RequestBase where TResponseCore : ResponseBase 
            where TImplementation : ICoreBusinessOperation<TRequestCore, TResponseCore>
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            kernel.Bind<ICoreBusinessOperation<TRequestCore, TResponseCore>>().To<TImplementation>();
        }

        /// <summary>
        /// Binds a IBusinessOpeartion to a given implementation, constrained/defined by 
        /// the types of the input output
        /// </summary>
        /// <typeparam name="TRequestDTO">The type of the Request in DTO form</typeparam>
        /// <typeparam name="TResponseDTO">The type of the Response in DTO form</typeparam>
        /// <typeparam name="TRequestCore">The type of the Core request of the interface</typeparam>
        /// <typeparam name="TResponseCore">The type of the Core response of the interface</typeparam>
        /// <typeparam name="TImplementation">The type implemeting the operation</typeparam>
        /// <param name="dealerId">the dealer to which this bind needs to be applied</param>
        public static void BindBusinessInterface<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore, TImplementation>(int dealerId) 
            where TRequestDTO : RequestBaseDTO 
            where TResponseDTO : ResponseBaseDTO 
            where TRequestCore : RequestBase 
            where TResponseCore : ResponseBase 
            where TImplementation : IBusinessOperation<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore>
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            kernel.Bind<IBusinessOperation<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore>>().To<TImplementation>();
        }


        /// <summary>
        /// Gets the Business operation for all the given types as signature of input ouput
        /// </summary>
        /// <typeparam name="TRequestDTO">The type of the Request in DTO form</typeparam>
        /// <typeparam name="TResponseDTO">The type of the Response in DTO form</typeparam>
        /// <typeparam name="TRequestCore">The type of the Request in core form</typeparam>
        /// <typeparam name="TResponseCore">The type of the Response in core form</typeparam>
        /// <param name="dealerId">The dealer to which this bindings apply</param>
        /// <returns>the IBusinessOperation for the types requested</returns>
        public static IBusinessOperation<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore> GetBusinessOperation
            <TRequestDTO, TResponseDTO, TRequestCore, TResponseCore>(Int32 dealerId)
            where TRequestDTO : RequestBaseDTO
            where TResponseDTO : ResponseBaseDTO
            where TRequestCore : RequestBase
            where TResponseCore : ResponseBase
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            return kernel.Get<IBusinessOperation<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore>>();
        }

        /// <summary>
        /// Gets the IDTOBusinessOperation for all the given types as signature of input ouput
        /// </summary>
        /// <typeparam name="TRequestDTO">The type of the Request in DTO form</typeparam>
        /// <typeparam name="TResponseDTO">The type of the Response in DTO form</typeparam>
        /// <param name="dealerId">The dealer to which this bindings apply</param>
        /// <returns>the IDTOBusinessOperation for the types requested</returns>
        public static IDTOBusinessOperation<TRequestDTO, TResponseDTO> GetDTOBusinessOperation
           <TRequestDTO, TResponseDTO>(Int32 dealerId)
            where TRequestDTO : RequestBaseDTO
            where TResponseDTO : ResponseBaseDTO
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            return kernel.Get<IDTOBusinessOperation<TRequestDTO, TResponseDTO>>();
        }

        /// <summary>
        /// Gets the IDTOBusinessOperation for all the given types as signature of input ouput
        /// </summary>
        /// <typeparam name="TRequestCore">The type of the Request in core form</typeparam>
        /// <typeparam name="TResponseCore">The type of the Response in core form</typeparam>
        /// <param name="dealerId">The dealer to which this bindings apply</param>
        /// <returns>the IDTOBusinessOperation for the types requested</returns>
        public static ICoreBusinessOperation<TRequestCore, TResponseCore> GetCoreBusinessOperation
           <TRequestCore, TResponseCore>(Int32 dealerId)
            where TRequestCore : RequestBase
            where TResponseCore : ResponseBase
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            return kernel.Get<ICoreBusinessOperation<TRequestCore, TResponseCore>>();
        }


        /// <summary>
        /// Rebinds a DTO interface of given types to the implementation 
        /// </summary>
        /// <typeparam name="TRequestDTO">The type of the DTO request of the interface</typeparam>
        /// <typeparam name="TResponseDTO">The type of the DTO response of the interface</typeparam>
        /// <typeparam name="TImplementation">The type implementing the interface</typeparam>
        /// <param name="dealerId">the dealer id to which this implementation needs to be rebind</param>
        public static void RebindDTOInterface<TRequestDTO, TResponseDTO, TImplementation>(int dealerId) 
            where TRequestDTO : RequestBaseDTO 
            where TResponseDTO : ResponseBaseDTO 
            where TImplementation : IDTOBusinessOperation<TRequestDTO, TResponseDTO>
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            kernel.Rebind<IDTOBusinessOperation<TRequestDTO, TResponseDTO>>().To<TImplementation>();
        }

        /// <summary>
        /// Rebinds a Core interface of given types to the implementation 
        /// </summary>
        /// <typeparam name="TRequestCore">The type of the Core request of the interface</typeparam>
        /// <typeparam name="TResponseCore">The type of the Core response of the interface</typeparam>
        /// <typeparam name="TImplementation">The type implementing the interface</typeparam>
        /// <param name="dealerId">the dealer id to which this implementation needs to be rebind</param>
        public static void RebindCoreInterface<TRequestCore, TResponseCore, TImplementation>(int dealerId) 
            where TRequestCore : RequestBase 
            where TResponseCore : ResponseBase 
            where TImplementation : ICoreBusinessOperation<TRequestCore, TResponseCore>
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            kernel.Rebind<ICoreBusinessOperation<TRequestCore, TResponseCore>>().To<TImplementation>();
        }

        /// <summary>
        /// Binds a IBusinessOpeartion to a given implementation, constrained/defined by 
        /// the types of the input output
        /// </summary>
        /// <typeparam name="TRequestDTO">The type of the Request in DTO form</typeparam>
        /// <typeparam name="TResponseDTO">The type of the Response in DTO form</typeparam>
        /// <typeparam name="TRequestCore">The type of the Core request of the interface</typeparam>
        /// <typeparam name="TResponseCore">The type of the Core response of the interface</typeparam>
        /// <typeparam name="TImplementation">The type implemeting the operation</typeparam>
        /// <param name="dealerId">the dealer to which this bind needs to be applied</param>
        public static void RebindBusinessInterface<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore, TImplementation>(int dealerId)
            where TRequestDTO : RequestBaseDTO
            where TResponseDTO : ResponseBaseDTO
            where TRequestCore : RequestBase
            where TResponseCore : ResponseBase
            where TImplementation : IBusinessOperation<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore>
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            kernel.Rebind<IBusinessOperation<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore>>().To<TImplementation>();
        }

        /// <summary>
        /// Rebinds a DTO interface of given types to the implementation 
        /// </summary>
        /// <typeparam name="TRequestDTO">The type of the DTO request of the interface</typeparam>
        /// <typeparam name="TResponseDTO">The type of the DTO response of the interface</typeparam>
        /// <param name="dealerId">the dealer id to which this implementation needs to be rebind</param>
        /// <param name="impl">The constant object to which the interface will be bind</param>
        public static void RebindDTOInterfaceToConstant<TRequestDTO, TResponseDTO>(int dealerId, IDTOBusinessOperation<TRequestDTO, TResponseDTO> impl)
            where TRequestDTO : RequestBaseDTO
            where TResponseDTO : ResponseBaseDTO
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            kernel.Rebind<IDTOBusinessOperation<TRequestDTO, TResponseDTO>>().ToConstant(impl);
        }

        /// <summary>
        /// Rebinds a Core interface of given types to the implementation 
        /// </summary>
        /// <typeparam name="TRequestCore">The type of the Core request of the interface</typeparam>
        /// <typeparam name="TResponseCore">The type of the Core response of the interface</typeparam>
        /// <param name="dealerId">the dealer id to which this implementation needs to be rebind</param>
        /// <param name="impl">The constant object to which the interface will be bind</param>
        public static void RebindCoreInterfaceToConstant<TRequestCore, TResponseCore>(int dealerId,  ICoreBusinessOperation<TRequestCore, TResponseCore> impl)
            where TRequestCore : RequestBase
            where TResponseCore : ResponseBase
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            kernel.Rebind<ICoreBusinessOperation<TRequestCore, TResponseCore>>().ToConstant(impl);
        }

        /// <summary>
        /// Binds a IBusinessOpeartion to a given implementation, constrained/defined by 
        /// the types of the input output
        /// </summary>
        /// <typeparam name="TRequestDTO">The type of the Request in DTO form</typeparam>
        /// <typeparam name="TResponseDTO">The type of the Response in DTO form</typeparam>
        /// <typeparam name="TRequestCore">The type of the Core request of the interface</typeparam>
        /// <typeparam name="TResponseCore">The type of the Core response of the interface</typeparam>
        /// <param name="dealerId">the dealer to which this bind needs to be applied</param>
        /// <param name="impl">The constant object to which the interface will be bind</param>
        public static void RebindBusinessInterfaceToConstant<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore>(int dealerId, IBusinessOperation<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore> impl)
            where TRequestDTO : RequestBaseDTO
            where TResponseDTO : ResponseBaseDTO
            where TRequestCore : RequestBase
            where TResponseCore : ResponseBase
        {
            IKernel kernel = GetKernelForDealer(dealerId);
            kernel.Rebind<IBusinessOperation<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore>>().ToConstant(impl);
        }
    }
}
