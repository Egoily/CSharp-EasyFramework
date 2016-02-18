using System;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;


namespace com.etak.core.operation.manager
{
    /// <summary>
    /// Builds the map between the types of the Business OPeration and the implementation
    /// </summary>
    /// <typeparam name="TRequestDTO"></typeparam>
    /// <typeparam name="TResponseDTO"></typeparam>
    /// <typeparam name="TRequestCore"></typeparam>
    /// <typeparam name="TResponseCore"></typeparam>
    public class BusinessOperationBuildPart<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore>
        where TRequestDTO : RequestBaseDTO
        where TResponseDTO : ResponseBaseDTO
        where TRequestCore : RequestBase
        where TResponseCore : ResponseBase
    {
        private Int32 dealerId = 0;

        /// <summary>
        /// Starts a new BusinessOperationBuildPart for a given dealer
        /// </summary>
        /// <param name="dealerId"></param>
        public BusinessOperationBuildPart(int dealerId)
        {
            this.dealerId = dealerId;
        }

        /// <summary>
        /// Binds the three interafces DTO, Core and Combined to the given TImplementation
        /// </summary>
        /// <typeparam name="TImplementation">the type of the class that implements all interfaces</typeparam>
        public void To<TImplementation>() where TImplementation :
            IBusinessOperation<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore>
        {
            BusinessOperationManager.BindDTOInterface<TRequestDTO, TResponseDTO, TImplementation>(dealerId);
            BusinessOperationManager.BindCoreInterface<TRequestCore, TResponseCore, TImplementation>(dealerId);
            BusinessOperationManager.BindBusinessInterface<TRequestDTO, TResponseDTO, TRequestCore, TResponseCore, TImplementation>(dealerId);
        }
    }
}
