using System.Collections.Generic;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.product.message.GetProductOfferingByProductOfferingId;

namespace com.etak.core.bizops.opssupport.QueryProductCatalogById
{
    /// <summary>
    /// Get Product Catalog from ProductId
    /// </summary>
    public class QueryProductCatalogByIdBizOp
        : AbstractBusinessOperation<QueryProductCatalogByIdRequestDTO, QueryProductCatalogByIdResponseDTO,
                                    QueryProductCatalogByIdRequestInternal, QueryProductCatalogByIdResponseInternal>
    {
       

        /// <summary>
        /// Map not automapped inbound properties, which is CustomerProduct
        /// </summary>
        /// <param name="dtoRequest">The dto request</param>
        /// <param name="coreInput">The core input</param>
        protected override void MapNotAutomappedInboundProperties(QueryProductCatalogByIdRequestDTO dtoRequest, ref QueryProductCatalogByIdRequestInternal coreInput)
        {
            #region Get Product using Microservice

            var getProductOfferingByIdReq = new GetProductOfferingByProductOfferingIdRequest
            {
                ProductOfferingId = dtoRequest.ProductCatalogId
            };
            var getProductOfferingByIdMs = MicroServiceManager.GetMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();
            var getProductOfferingByIdRes = getProductOfferingByIdMs.Process(getProductOfferingByIdReq, null);

            if (getProductOfferingByIdRes.ProductOffering == null)
            {
                throw new BusinessLogicErrorException(string.Format("Product Offering Catalog not found"), BizOpsErrors.ProductNotFound);
            }

            coreInput.ProductOffering = getProductOfferingByIdRes.ProductOffering;

            #endregion
        }

        /// <summary>
        /// Convert the core CustomerProduct into CustomerProductCatalogDTO and the core ChargingOptions of CUstomerProduct into ProductPurchaseChargingOptionDTO
        /// </summary>
        /// <param name="coreOutput">the core output</param>
        /// <param name="dtoOutput">the dto output</param>
        protected override void MapNotAutomappedOutboundProperties(QueryProductCatalogByIdResponseInternal coreOutput, ref QueryProductCatalogByIdResponseDTO dtoOutput)
        {
            if (coreOutput.ProductOffering != null)
            {
                dtoOutput.CustomerProductCatalogDto = coreOutput.ProductOffering.ToDto(); 
                //TODO Do we really need this List of ProductChargeOption when the ProductCatalogDTO already has the same list???
                dtoOutput.ProductPurchaseChargingOptionDto = new List<ProductPurchaseChargingOptionDTO>();
                foreach (var chargingOption in coreOutput.ProductOffering.ChargingOptions)
                {
                    dtoOutput.ProductPurchaseChargingOptionDto.Add(chargingOption.ToDto());
                }
            }

        }

        /// <summary>
        /// Code that will be stored in the operation log for the operation
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.QueryProductCatalogById; }
        }

        /// <summary>
        /// Unique identifier of the operation
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.QueryProductCatalogById; }
        }

        /// <summary>
        /// Simple process only returning the response that has already been in core request/QueryProductCatalogByIdRequestInternal
        /// </summary>
        /// <param name="request">The core input</param>
        /// <param name="runningOperation">The trace of the operation</param>
        /// <param name="invoker">The environment of the invokation</param>
        /// <returns>QueryProductCatalogByIdResponseInternal</returns>
        protected override QueryProductCatalogByIdResponseInternal ProcessBusinessLogic(QueryProductCatalogByIdRequestInternal request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker)
        {
            return new QueryProductCatalogByIdResponseInternal
            {
                ProductOffering = request.ProductOffering,
                ErrorCode = 0,
                Message = "Query successful",
                ResultType = ResultTypes.Ok
            };
        }
    }
}
