using System;
using System.Linq;
using System.Reflection;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.GSMSubscription.messages.CreateCustomerProductAssignment;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.GetProductByProductId;
using com.etak.core.product.message.GetProductChargeOptionByProductChargeOptionId;
using com.etak.core.product.message.GetProductOfferingByProductOfferingId;
using com.etak.core.product.microservices;
using log4net;

namespace com.etak.core.bizops.fullfilment.AssignProductToCustomer
{
    /// <summary>
    /// Assign only one product at the time to the Customer
    /// Used as auxiliary in PurchaseProductForCusotmer in order to generate multiple records of execution
    /// So.. this Order is used ONLY for log purposes. We know is not intended for that, but is the best solution available
    /// </summary>
    public class AssignProductOfferingToCustomerBizOp : AbstractSinglePhaseOrderProcessor<AssignProductOfferingToCustomerRequestDTO,AssignProductOfferingToCustomerResponseDTO
        ,AssignProductOfferingToCustomerRequestInternal,AssignProductOfferingToCustomerResponseInternal,AssignProductOfferingToCustomerOrder>
    {

        #region Ini Var

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);



        #endregion

        
        
        #region Create MicroServices
        
        private readonly IMicroService<CreateCustomerProductAssignmentRequest, CreateCustomerProductAssignmentResponse> _createassignProductForCustomerMs = MicroServiceManager.GetMicroService<CreateCustomerProductAssignmentRequest, CreateCustomerProductAssignmentResponse>();
        private readonly IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> _microServiceCheckAuthorizationMs = MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        private readonly IMicroService<GetProductChargeOptionByProductChargeOptionIdRequest, GetProductChargeOptionByProductChargeOptionIdResponse>  _getChargeOptionByChargeOptionId = MicroServiceManager.GetMicroService<GetProductChargeOptionByProductChargeOptionIdRequest, GetProductChargeOptionByProductChargeOptionIdResponse>();
        private readonly IMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse> _getProductOfferingByProductOfferingIdMs = MicroServiceManager.GetMicroService<GetProductOfferingByProductOfferingIdRequest, GetProductOfferingByProductOfferingIdResponse>();
        #endregion




        /// <summary>
        /// Map Inbound
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedOrderInboundProperties(AssignProductOfferingToCustomerRequestDTO request, ref AssignProductOfferingToCustomerRequestInternal coreInput)
        {

            #region Validate Customer

            if (coreInput.Customer == null)
            {
                throw new DataValidationErrorException("Customer does not exist", BizOpsErrors.CustomerIsNull);
            }

            #endregion

            #region Product
            Log.Debug("MapNotAutomappedOrderInboundProperties Product to assign");
            var getProductOfferingResponse = _getProductOfferingByProductOfferingIdMs.Process(new GetProductOfferingByProductOfferingIdRequest()
            {
                MVNO = coreInput.MVNO,
                User = coreInput.User,
                ProductOfferingId = request.ProductOfferingId
            }, null);
            if (getProductOfferingResponse.ResultType != ResultTypes.Ok || getProductOfferingResponse.ProductOffering == null)
                throw new DataValidationErrorException("Product does not exist", BizOpsErrors.ProductNotFound);

            Log.Debug("MapNotAutomappedOrderInboundProperties ProductChargeOption to assign");
            var getChargeOptionResponse =
                _getChargeOptionByChargeOptionId.Process(new GetProductChargeOptionByProductChargeOptionIdRequest()
                {
                  MVNO  = coreInput.MVNO,
                  User = coreInput.User,
                  ProductChargeOptionId = request.ProductChargeOptionId,
                },null);
            if (getChargeOptionResponse.ResultType != ResultTypes.Ok || getChargeOptionResponse.ProductChargeOption == null)
                throw new DataValidationErrorException(string.Format("ProductChargeOption {0} doesn't exist.", request.ProductChargeOptionId), BizOpsErrors.ProductChargeOptionNotFound);
            
            coreInput.ProductOffering = Tuple.Create(getProductOfferingResponse.ProductOffering, getChargeOptionResponse.ProductChargeOption);
            #endregion

            #region Check if the products and customer corresponds to the actual MVNO

            var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Customer.DealerID.HasValue ? coreInput.Customer.DealerID.Value : 0 };
            var checkAuthorizationResponse = _microServiceCheckAuthorizationMs.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);

            checkAuthorizationRequest = new CheckAuthorizationRequest()
            {
                UserInfo = coreInput.User,
                DealerId = (int) (coreInput.ProductOffering.Item1.OfferedProduct.VMO.DealerID.HasValue ? coreInput.ProductOffering.Item1.OfferedProduct.VMO.DealerID : 0),
            };
            checkAuthorizationResponse = _microServiceCheckAuthorizationMs.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);


            #endregion

            #region Fill Input

            coreInput.CreateDate = request.CreateDate;
            coreInput.StartDate = request.StartDate;
            coreInput.EndDate = request.EndDate;


            #endregion
        }


        /// <summary>
        /// Map Outbound
        /// </summary>
        /// <param name="source"></param>
        /// <param name="DTOOutput"></param>
        protected override void MapNotAutomappedOrderOutboundProperties(AssignProductOfferingToCustomerResponseInternal source, ref AssignProductOfferingToCustomerResponseDTO DTOOutput)
        {

            #region Fill Output

            DTOOutput.productPurchased = source.productPurchased.ToDto();
            DTOOutput.ProductCatalog = source.ProductOffering.ToDto();
            DTOOutput.Subscription = source.Subscription.ToDto();

            #endregion

        }

        /// <summary>
        /// ProcessReqeust: purchase indivudual product Provided
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override AssignProductOfferingToCustomerResponseInternal ProcessRequest(AssignProductOfferingToCustomerOrder order, AssignProductOfferingToCustomerRequestInternal request)
        {

            #region Var

            Tuple<ProductOffering, ProductChargeOption> pc = request.ProductOffering;
            

            #endregion

            #region Variables

            AssignProductOfferingToCustomerResponseInternal response = new AssignProductOfferingToCustomerResponseInternal();
            CreateCustomerProductAssignmentResponse createCustomerProductAssignmentResponse = new CreateCustomerProductAssignmentResponse();

            #endregion

            #region Assign Product to Customer

            Log.Debug("Assign Product to Customer");
            CreateCustomerProductAssignmentRequest assignProductForCustomerRequest = new CreateCustomerProductAssignmentRequest
                ()
            {
                CustomerProductAssignment = new CustomerProductAssignment()
                {
                    PurchasingCustomer = request.Customer,
                    PurchasedProduct = pc.Item1.OfferedProduct,
                    ProductOffering = pc.Item1,
                    ProductChargePurchased = pc.Item2,
                    CreateDate = request.CreateDate,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                }
            };

            createCustomerProductAssignmentResponse =
                _createassignProductForCustomerMs.Process(assignProductForCustomerRequest, null);
            if (createCustomerProductAssignmentResponse.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("CreateCustomerProductAssignment: Failed",
                    BizOpsErrors.CreateCustomerProductAssignmentError);

            Log.DebugFormat("Product: {0}", pc.Item1.Id);

            #endregion Assign Product to Customer

            #region Response

            response.productPurchased = createCustomerProductAssignmentResponse.CustomerProductAssignment;
            response.ProductOffering = request.ProductOffering.Item1;
            response.Product = request.ProductOffering.Item1.OfferedProduct;
            response.Subscription = request.Customer.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active);
            return response;

            #endregion

        }

        /// <summary>
        /// OperationCode
        /// </summary>
        public override string OperationCode
        {
            get { return (OperationCodes.AssignProductToCustomerOperation); }
        }

        /// <summary>
        /// OperationDiscriminator
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return (OperationDiscriminators.AssignProductToCustomerOperation); }
        }

    }
}
