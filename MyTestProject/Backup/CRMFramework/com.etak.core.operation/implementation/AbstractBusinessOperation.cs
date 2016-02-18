using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.contract.amount;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.numberInfo;
using com.etak.core.model.operation.contract.product;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.aaa;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.operation;
using com.etak.core.repository.crm.resource;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.crm.subscription;
using log4net;
using Newtonsoft.Json;

namespace com.etak.core.operation.implementation
{
    /// <summary>
    /// Class that provides the commong functionality for any etak operation, as Authentication,
    /// Authorization, Operation log. It forces the implementation of the business logic. 
    /// </summary>
    /// <typeparam name="TDTOInput">The external type of the request in DTO style</typeparam>
    /// <typeparam name="TDTOOutput">The external type of the response in DTO style</typeparam>
    /// <typeparam name="TInternalInput">The internal type of the request using the core model</typeparam>
    /// <typeparam name="TInternalOutput">The internal type of the response using the core model</typeparam>
    public abstract class AbstractBusinessOperation<TDTOInput, TDTOOutput, TInternalInput, TInternalOutput>
        : BusinessOperation,
         IBusinessOperation<TDTOInput, TDTOOutput, TInternalInput, TInternalOutput>
        where TDTOInput : RequestBaseDTO, new()
        where TDTOOutput : ResponseBaseDTO, new()
        where TInternalInput : RequestBase, new()
        where TInternalOutput : ResponseBase, new()
    {
      


        // ReSharper disable once StaticFieldInGenericType
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

     

        /// <summary>
        /// Logical code, this is a cnst field but Nhibernate requires the setter.
        /// </summary>
        public override String OperationCode
        {
            set { }
        }

        private BusinessOperation GetSelfPeristedInstance()
        {
            IBusinessOperationRepository<BusinessOperation> repo =
                RepositoryManager.GetRepository<IBusinessOperationRepository<BusinessOperation>>();

            BusinessOperation selfPersisted = repo.GetById(Id) ?? repo.Create(this);
            return selfPersisted;
        }

        /// <summary>
        /// The time if any of the operation start at DTO time.
        /// </summary>
        private static DateTime? DTOOperationStartTime { get; set; }
        
        /// <summary>
        /// True if the operation is part of another operation
        /// </summary>
        protected Boolean IsRootOperation
        {
            get { return StackHelper.Stack.Count == 1; }
        }

        /// <summary>
        /// This method operates in ET model. Converts the DTORequest to the Request types
        /// </summary>
        /// <param name="dtoRequest">The request in ET DTO form</param>
        /// <param name="coreInput">The request in Internal form, prefilled with the common parameters</param>
        /// <returns>the operation in the et Internal form</returns>
        protected abstract void MapNotAutomappedInboundProperties(TDTOInput dtoRequest, ref TInternalInput coreInput);

        /// <summary>
        /// Coverts the response/result of the process (in internal form) to the ET DTO form.
        /// </summary>
        /// <param name="coreOutput">the result of process implementation</param>
        /// <param name="dtoOutput">the preallocated response, will all auto fields mapped</param>
        /// <returns>the response in the ET DTO form</returns>
        protected abstract void MapNotAutomappedOutboundProperties(TInternalOutput coreOutput, ref TDTOOutput dtoOutput);

        /// <summary>
        /// Method implemented by the inheriting class that actually performs the core operation
        /// </summary>
        /// <param name="request">Input parameter for the request</param>
        /// <param name="runningOperation">The trace for the ongoing operation</param>
        /// <param name="invoker">The information about the invokation of the operation</param>
        /// <returns>The response of processing the request</returns>
        protected abstract TInternalOutput ProcessBusinessLogic(TInternalInput request, BusinessOperationExecution runningOperation, RequestInvokationEnvironment invoker);

        /// <summary>
        /// Process a request in ETAK core model, to invoke internally, not in DTO form
        /// </summary>
        /// <param name="request">the request to be processed</param>
        /// <param name="invoker">the invokation environment for the request</param>
        /// <returns>The result of the processing</returns>
        public TInternalOutput Process(TInternalInput request, RequestInvokationEnvironment invoker)
        {

            Log.Debug("Populating trazeObject with request information");
            IBusinessOperationExecutionRepository<BusinessOperationExecution> bizOpRepo =
                RepositoryManager.GetRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();

            Log.Info("Processing business logic");

            BusinessOperationExecution bizOpTraze = new BusinessOperationExecution
            {
                User = request.User,
                MVNO = request.MVNO,
                Channel = request.Channel,
                StartTime = DTOOperationStartTime ?? DateTime.Now,
                OperationDefintition = GetSelfPeristedInstance(),
                //Get the details from the operation definition from the class extending this one.
                //ProcessorDiscriminator = OperationDiscriminator,
                //OperationCode = OperationCode,
            };

            //Fill the basic information from RequestBase
            FillInputTrazedObjectFromRequest(request, bizOpTraze);

            //Check if the operation has been invoked inside another BizOp if so 
            //we need to create the trace and assing the parent
            if (StackHelper.Stack.Count != 0)
            {
                Log.InfoFormat("Stack.Count: [{0}]", StackHelper.Stack.Count);
                Log.InfoFormat("Assining bizOpTraze Id to Parent: {0}", StackHelper.Stack.Peek().Id);
                bizOpTraze.ParentBusinessOperation = StackHelper.Stack.Peek();
                Log.InfoFormat("Assining bizOpTraze Id to Root: {0}", StackHelper.Stack.Last().Id);
                bizOpTraze.RootBusinessOperation = StackHelper.Stack.Last();
            }
            else
            {
                Log.Info("We don't have any bizOp on the Stack");
            }
            //Here we push curent operation so if there's any chained operation we can detect
            //that it has a parent and map the relations acordingly
            Log.Info("Push BizOpTraze...");
            StackHelper.Stack.Push(bizOpTraze);
           

            //Invoke the operation
            TInternalOutput response = ProcessBusinessLogic(request, bizOpTraze, invoker);

            //We have finshed we need to take ourselves out of the stack

            //Check if the operation has been invoked inside another BizOp if so 
            //we need to create the trace and assing the parent. But if it's the lastone
            //let's keep it there so we can adjust the real endtime after
            Log.Info("...Pop BizOpTraze");
            StackHelper.Stack.Pop();
            FillInputTrazedObjectFromResponse(bizOpTraze, response);
            bizOpTraze.EndDate = DateTime.Now;
            bizOpRepo.Create(bizOpTraze);
            return response;
        }

        private void FillInputTrazedObjectFromResponse(BusinessOperationExecution bizOpTraze, TInternalOutput response)
        {
            if (response == null || bizOpTraze == null)
                return;

            bizOpTraze.ResultType = response.ResultType;
            bizOpTraze.ErrorCode = response.ErrorCode;
            bizOpTraze.SystemMessages = response.Message;

            #region Customer based request
            var custResponse = response as ICustomerBasedResponse;
            if (custResponse != null && bizOpTraze.Customer==null)
                bizOpTraze.Customer = custResponse.Customer;
            #endregion

            #region Simcard based response
            var simCardResp = response as ISimCardBasedResponse;
            if (simCardResp != null && bizOpTraze.SimCard == null)
                bizOpTraze.SimCard = simCardResp.SimCard;
            #endregion

            #region NumberInfo based response
            var numberInfoResp = response as INumberInfoBasedResponse;
            if (numberInfoResp != null && bizOpTraze.MSISDN == null)
                bizOpTraze.MSISDN = numberInfoResp.NumberInPool;
            #endregion

            #region Amount based response
            var amountResp = response as IAmountBasedResponse;
            if (amountResp != null && bizOpTraze.Amount ==0)
            {
                bizOpTraze.Amount = amountResp.Amount;
            }
            #endregion

            #region Subscription based response
            var subscriptResp = response as ISubscriptionBasedResponse;
            if (subscriptResp != null && bizOpTraze.Subscription == null)
            {
                bizOpTraze.Subscription = subscriptResp.Subscription;
            }
            #endregion

            #region Product Offering based response
            var productOfferingResp = response as IProductOfferingBasedResponse;
            if (productOfferingResp != null && bizOpTraze.ProductOffering == null)
            {
                bizOpTraze.ProductOffering = productOfferingResp.ProductOffering;
            }
            #endregion

            #region Product based response
            var productResp = response as IProductBasedResponse;
            if (productResp != null && bizOpTraze.Product == null)
            {
                bizOpTraze.Product = productResp.Product;
            }
            #endregion
        }

        /// <summary>
        /// Implementation of process, takes care of the main infrastructure logic as
        /// ExceptionHandling, operation log Creation and transactions.
        /// </summary>
        /// <typeparam name="TDTOExternalIn">The type of the request in the customer model</typeparam>
        /// <typeparam name="TDTOExternalOut">The type of the response in the customer model</typeparam>
        /// <param name="validator">The validator</param>
        /// <param name="inboundConverter">the converter for the the DTO request in customer form to the DTO request in ET Form</param>
        /// <param name="outboundConverter">the converter for the the DTO response in ET form to the DTO response in customer</param>
        /// <param name="requestExt">the reques in Customer DTO form (TDTOExternalIn) </param>
        /// <param name="invoker">the request invockation environmnt.</param>
        /// <returns>the response in the DTO customer form</returns>
        public TDTOExternalOut ProcessFromCustomerModel<TDTOExternalIn, TDTOExternalOut>(
            IValidator<TDTOExternalIn> validator, 
            ITypeConverter<TDTOExternalIn, TDTOInput> inboundConverter, 
            ITypeConverter<TDTOOutput, TDTOExternalOut> outboundConverter,
            TDTOExternalIn requestExt, 
            RequestInvokationEnvironment invoker)
        {

            DTOOperationStartTime = DateTime.Now;
            IBusinessOperationExecutionRepository<BusinessOperationExecution> bizOpRepo = RepositoryManager.GetRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();

            TDTOInput request = null;
            TDTOOutput response;
            TInternalInput internalRequest = null;
            
            try
            {
                //Conver the request in the external form to the internal
                if (Log.IsDebugEnabled)
                    Log.Debug("Converting from customer DTO model to ET DTO model");
                request = inboundConverter.Convert(requestExt);

                //Customer model validation
                if (Log.IsDebugEnabled)
                    Log.Debug("Performing customer model request validation");
                validator.Validate(requestExt);
               
                if (Log.IsDebugEnabled)
                    Log.Debug("Checking permissions of user for dealer");

                //Check that the user can do operation on the given vmno
                AuthorizationHelper.Authorize(invoker.User, invoker.Dealer);

                using (var trx = RepositoryManager.GetConnection().BeginTransaction())
                {
                    if (Log.IsDebugEnabled)
                        Log.Debug("Converting request ET DTO model to ET Internal model (access db)");

                    //Convert from DTO model to the internal model
                    internalRequest = AutoMapInboundFields(request, invoker.User, invoker.Dealer);

                    //Get operation configuration
                    Log.Info("Processing request");
                    TInternalOutput internalResponse = Process(internalRequest, invoker);
                    
                    if (Log.IsDebugEnabled)
                        Log.Debug("Converting request in internal format to ET DTO format");
                    response = AutoMapOutboundFields(internalResponse);
                    
                    Log.Info("Commiting transaction");
                    trx.Commit();
                }
            }            
            //We need to ensure that the trace is kept, but only in case of exception. 
            catch (Exception ex)
            {
                //This trace needs to be created in case of error (Exception thrown)
                //when everything goes ok, it is stored in the process from core.
                
                //Fill with all the entities from request and filled previously
                BusinessOperationExecution emergencyTrace = new BusinessOperationExecution();
                FillInputTrazedObjectFromRequest(internalRequest, emergencyTrace);
                emergencyTrace.StartTime = DTOOperationStartTime.Value;
                emergencyTrace.OperationDefintition = GetSelfPeristedInstance();
                
                //emergencyTrace.OperationCode = OperationCode;
                //emergencyTrace.ProcessorDiscriminator = OperationDiscriminator;


                TInternalOutput coreResponse;
                //If it's an internal thrown exception, put the known error code and the handled error message
                ElephantTalkBaseException etakEx = ex as ElephantTalkBaseException;
                if (etakEx != null)
                {
                    Log.InfoFormat("Handled error while doing BusinessOperation ResultType:{0} ErrorCode:{1} Message:{2}", etakEx.ResultType, etakEx.ErrorCode, etakEx.Message);
                    coreResponse = new TInternalOutput
                    {
                        ErrorCode = etakEx.ErrorCode,
                        Message = etakEx.Message,
                        ResultType = etakEx.ResultType,
                    };
                    emergencyTrace.SystemMessages = coreResponse.Message;
                }
                //If it is a system exception, then hide the error text and return a constant Error
                else 
                {
                    Log.Error("Unhandled error in operation", ex);
                    coreResponse = new TInternalOutput
                    {
                        ErrorCode = -1,
                        Message = "Unknown Error",
                        ResultType = ResultTypes.UnknownError,
                    };
                    emergencyTrace.SystemMessages = ex.ToString();
                }

                emergencyTrace.ResultType = coreResponse.ResultType;
                emergencyTrace.ErrorCode = coreResponse.ErrorCode;

                //Clean the Stack to avoid thrown an Exception with 
                Log.Info("Clear BizOpTrace's stack to avoid NHibernate.TransientException Error");
                StackHelper.Stack.Clear();

                response = AutoMapOutboundFields(coreResponse);
                try
                {
                    if (emergencyTrace.SystemMessages.Length > 1024)
                        emergencyTrace.SystemMessages = emergencyTrace.SystemMessages.Substring(0, 1024);

                    using (var trx = RepositoryManager.GetConnection().BeginTransaction())
                    {

                        #region CFP-225 The dealer is empty when an error occurs
                        if (internalRequest != null)
                        {
                            emergencyTrace.MVNO = internalRequest.MVNO;
                        }
                        else if (request != null)
                        {
                            emergencyTrace.MVNO = invoker.Dealer;
                        } 
                        #endregion

                        emergencyTrace.EndDate = DateTime.Now;
                        bizOpRepo.Create(emergencyTrace);
                        trx.Commit();
                    }
                }
                catch (Exception innerEx)
                {
                    Log.FatalFormat("Unable to persist BusinessOperation in the DB! Operation:{0} Error:{1}", emergencyTrace, innerEx);
                }
            }
            if (Log.IsDebugEnabled)
                Log.Debug("Converting ET DTO model to customer DTO model");
            
            TDTOExternalOut resposeCustomerModel = outboundConverter.Convert(response);
            return (resposeCustomerModel);
        }

        private TInternalInput AutoMapInboundFields(TDTOInput request, LoginInfo user, DealerInfo dealer)
        {
            TInternalInput coreInput = new TInternalInput
            {
                User = user,
                MVNO = dealer,
            };

            #region Load Customer Related Information
            ICustomerBasedRequest custRequest = coreInput as ICustomerBasedRequest;
            if (custRequest != null)
            {
                CustomerInfo customer;

                ICustomerIdBasedDTORequest custIdDTORequest = request as ICustomerIdBasedDTORequest;
               
                if (custIdDTORequest != null)
                {
                    if (Log.IsDebugEnabled)
                        Log.Info("CustomerId based request, loading Customer with ID:" + custIdDTORequest.CustomerId);
                    ICustomerInfoRepository<CustomerInfo> custRepo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                    customer = custRepo.GetById(custIdDTORequest.CustomerId);
                   
                }
                else
                {
                    throw new DevelopmentException("Request core model is ICustomerBasedRequest but the DTO did not have any"+
                                        "Customer information in the request in DTO model, this is a development error");
                }
                custRequest.Customer = customer;
            }

            IMultiCustomerRequestBased multiCustomerRequestBased = coreInput as IMultiCustomerRequestBased;
            if (multiCustomerRequestBased != null)
            {
                IExternalCustomerIdBasedDTORequest custExtIdRequest = request as IExternalCustomerIdBasedDTORequest;

                IDocumentIdBasedDTORequest custDocumentRequest = request as IDocumentIdBasedDTORequest;

                if (custExtIdRequest != null)
                {
                    if (Log.IsDebugEnabled)
                        Log.Info("External customerId based request, loading Customers with external id:" + custExtIdRequest.ExternalCustomerId);
                    ICustomerInfoRepository<CustomerInfo> custRepo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                    multiCustomerRequestBased.Customers = custRepo.GetByExternalId(custExtIdRequest.ExternalCustomerId);
                }
                else if (custDocumentRequest != null)
                {
                    if (Log.IsDebugEnabled)
                        Log.InfoFormat("Document Id based request, loading Customers with document Id [{0}] and type [{1}] that corresponds with the actual dealer.", 
                            custDocumentRequest.DocumentNumber, custDocumentRequest.DocumentType);
                                                

                    IPropertyInfoRepository<model.PropertyInfo> custRepo = RepositoryManager.GetRepository<IPropertyInfoRepository<model.PropertyInfo>>();
                    IDealerInfoRepository<DealerInfo> dealerRepo = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
                    multiCustomerRequestBased.Customers = custRepo.GetByDocumentId(custDocumentRequest.DocumentType, custDocumentRequest.DocumentNumber)
                        .Select(x => x.CustomerInfo).Where(x => AuthorizationHelper.TryAuthorize(user, dealerRepo.GetById(x.DealerID.Value)));
                }
                else
                {
                    throw new DevelopmentException("Request core model is IMultiCustomerRequestBased but the DTO did not have any" +
                                        "Multi Customer information in the request in DTO model, this is a development error");
                }
            }
            #endregion

            #region Load Multi Subscription related information
            IMultiSubscriptionBasedRequest multiSubsRequest = coreInput as IMultiSubscriptionBasedRequest;
            if (multiSubsRequest != null)
            {
                IEnumerable<ResourceMBInfo> subscription;
                IResourceMBRepository<ResourceMBInfo> subscriptionRepo = RepositoryManager.GetRepository<IResourceMBRepository<ResourceMBInfo>>();

                IMsisdnBasedDTORequest msdinBasedDTORequest = request as IMsisdnBasedDTORequest;
                if (msdinBasedDTORequest != null)
                {
                    if (Log.IsDebugEnabled)
                        Log.Debug("Loading ResourceMBs by msisdn " + msdinBasedDTORequest.MSISDN);
                    subscription = subscriptionRepo.GetByMSISDNAndStatusNotInAndActiveDates(msdinBasedDTORequest.MSISDN, new Int32[] {});
                }
                else
                {
                    throw new DevelopmentException("Request core model is ISubscriptionBasedRequest but the DTO did not have any" +
                                       "msisdn information in the request in DTO model, this is a development error");
                }
                multiSubsRequest.Subscriptions = subscription;
                multiSubsRequest.MSISDN = msdinBasedDTORequest.MSISDN;
            }
            #endregion

            #region Load Subscription Last Active information

            ISubscriptionLastActiveBasedRequest subsRequest = coreInput as ISubscriptionLastActiveBasedRequest;
            if (subsRequest != null)
            {
                ResourceMBInfo subscription;
                IResourceMBRepository<ResourceMBInfo> subscriptionRepo = RepositoryManager.GetRepository<IResourceMBRepository<ResourceMBInfo>>();

                IMsisdnBasedDTORequest msdinBasedDTORequest = request as IMsisdnBasedDTORequest;
                if (msdinBasedDTORequest != null)
                {
                    #region List of non valid status to get the active subscription
                    var listOfNonValidStatus = new Int32[]
                    {
                        (int)ResourceStatus.Deactive, 
                        (int)ResourceStatus.Deleted,
                        (int)ResourceStatus.Expired,
                        (int)ResourceStatus.Locked,
                        (int)ResourceStatus.Init, 
                        (int)ResourceStatus.Installed,
                        (int)ResourceStatus.Frozen,
                        (int)ResourceStatus.Converted,
                        (int)ResourceStatus.Reserved,
                        (int)ResourceStatus.Deactived,
                        (int)ResourceStatus.Inactive,
                    }; 
                    #endregion

                    if (Log.IsDebugEnabled)
                        Log.Debug("Loading ResourceMBs by msisdn " + msdinBasedDTORequest.MSISDN);
                    subscription = subscriptionRepo.GetByMSISDNAndStatusNotInAndActiveDates(msdinBasedDTORequest.MSISDN, listOfNonValidStatus)
                        .OrderByDescending(x => x.StartDate).FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active);
                }
                else
                {
                    throw new DevelopmentException("Request core model is ISubscriptionBasedRequest but the DTO did not have any" +
                                       "msisdn information in the request in DTO model, this is a development error");
                }
                subsRequest.Subscription = subscription;
                subsRequest.MSISDN = msdinBasedDTORequest.MSISDN;
            }

            #endregion

            #region Load SimCard related information
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            // ReSharper disable SuspiciousTypeConversion.Global
            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable HeuristicUnreachableCode
            ISimCardBasedRequest simRequest = coreInput as ISimCardBasedRequest;
            if (simRequest != null)
            {
                IICCIDBasedDTORequest iccidBasedDTORequest = request as IICCIDBasedDTORequest;
                if (iccidBasedDTORequest != null)
                {
                    ISIMCardInfoRepository<SIMCardInfo> simRepo = RepositoryManager.GetRepository<ISIMCardInfoRepository<SIMCardInfo>>();
                    SIMCardInfo sim = simRepo.GetById(iccidBasedDTORequest.ICCID);
                    simRequest.SimCard = sim;
                }
                else
                {
                    throw new DevelopmentException("Request core model is ISimCardBasedRequest but the DTO did not have any" +
                                      "sim information in the request in DTO model, this is a development error");
                }
            }
           
            #endregion

            #region NumberInfoBased Request
            INumberInfoBasedRequest numInfoBasedRequest = coreInput as INumberInfoBasedRequest;
            if (numInfoBasedRequest != null)
            {
                NumberInfo msisdnResource;
                INumberInfoRepository<NumberInfo> numberInfoReop =RepositoryManager.GetRepository<INumberInfoRepository<NumberInfo>>();
                IMsisdnBasedDTORequest msdinBasedDTORequest = request as IMsisdnBasedDTORequest;
                if (msdinBasedDTORequest != null)
                {
                    if (Log.IsDebugEnabled)
                        Log.Debug("Loading ResourceMB by msisdn " + msdinBasedDTORequest.MSISDN);
                    msisdnResource = numberInfoReop.GetById(msdinBasedDTORequest.MSISDN);
                }
                else
                {
                    throw new DevelopmentException("Request core model is INumberInfoBasedRequest but the DTO did not have any" +
                                       "msisdn information in the request in DTO model, this is a development error");
                }
                numInfoBasedRequest.NumberInPool = msisdnResource;
            }
            #endregion

            #region JointCustomer Related Information
            IJointCustomerBasedRequest jointCustomer = coreInput as IJointCustomerBasedRequest;
            if (jointCustomer != null)
            {
                CustomerInfo sourceCustomer;
                CustomerInfo destinationCustomer;

                IJointCustomerIdDTOBasedRequest jointCustIdDtoRequest = request as IJointCustomerIdDTOBasedRequest;
                if (jointCustIdDtoRequest != null)
                {
                    ICustomerInfoRepository<CustomerInfo> custRepo =
                        RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();

                    if (Log.IsDebugEnabled)
                        Log.DebugFormat("Joint CustomerId based request, loading Source Customer with ID: {0} and Destination Customer with ID: {1}" 
                                        , jointCustIdDtoRequest.SourceCustomerId, jointCustIdDtoRequest.DestinationCustomerId);
                    
                    sourceCustomer = custRepo.GetById(jointCustIdDtoRequest.SourceCustomerId);
                    destinationCustomer = custRepo.GetById(jointCustIdDtoRequest.DestinationCustomerId);

                }
                else
                {
                    throw new DevelopmentException("Request core model is IJointCustomerBasedRequest but the DTO did not have any" +
                                          "customerId information in the request in DTO model, this is a development error");
                }

                jointCustomer.SourceCustomerInfo = sourceCustomer;
                jointCustomer.DestinationCustomerInfo = destinationCustomer;
            }
            #endregion

            #region Joint Subscription
            IJointSubscriptionBasedRequest jointSubscription = coreInput as IJointSubscriptionBasedRequest;
            if (jointSubscription != null)
            {
                ResourceMBInfo sourceSubscription;
                ResourceMBInfo destinationSubscription;

                IResourceMBRepository<ResourceMBInfo> subscriptionRepo = RepositoryManager.GetRepository<IResourceMBRepository<ResourceMBInfo>>();

                IJointMsisdnDTOBasedRequest jointMsisdnDtoRequest = request as IJointMsisdnDTOBasedRequest;
                if (jointMsisdnDtoRequest != null)
                {
                     if (Log.IsDebugEnabled)
                         Log.DebugFormat("Joint Subscription based request, loading Source Subscription with MSISDN: {0} and Destination Subscription with MSISDN: {1}"
                                        , jointMsisdnDtoRequest.SourceMSISDN, jointMsisdnDtoRequest.DestinationMSISDN);

                    sourceSubscription = subscriptionRepo.GetByMSISDNAndStatusNotInAndActiveDates(jointMsisdnDtoRequest.SourceMSISDN, new Int32[] {}).FirstOrDefault();
                    destinationSubscription = subscriptionRepo.GetByMSISDNAndStatusNotInAndActiveDates(jointMsisdnDtoRequest.DestinationMSISDN, new Int32[] { }).FirstOrDefault();
                }
                else
                {
                    throw new DevelopmentException("Request core model is IJointSubscriptionBasedRequest but the DTO did not have any" +
                                            "customerId information in the request in DTO model, this is a development error");
                    
                }

                jointSubscription.SourceSubscription = sourceSubscription;
                jointSubscription.SourceMSISDN = jointMsisdnDtoRequest.SourceMSISDN;
                jointSubscription.DestinationSubscription = destinationSubscription;
                jointSubscription.DestinationMSISDN = jointMsisdnDtoRequest.DestinationMSISDN;
            }
            #endregion

            #region Amount Based Request

            IAmountBasedRequest amountBasedRequest = coreInput as IAmountBasedRequest;
            if (amountBasedRequest != null)
            {
                IAmountBasedDTORequest amountDtoRequest = request as IAmountBasedDTORequest;
                if (amountDtoRequest == null)
                {
                    throw new DevelopmentException("Request core model is IAmountBasedRequest but the DTO did not have any Amount, this is a development error");
                }

                amountBasedRequest.Amount = amountDtoRequest.amount;
            }

            #endregion

            #region Account Based Request

            IAccountBasedRequest accountBasedRequest = coreInput as IAccountBasedRequest;
            if (accountBasedRequest != null)
            {
                Account account;
                IAccountRepository<Account> accountRepo = RepositoryManager.GetRepository<IAccountRepository<Account>>();

                IAccountIdBasedDTORequest accountBasedDtoRequest = request as IAccountIdBasedDTORequest;
                if (accountBasedDtoRequest != null)
                {
                    if (Log.IsDebugEnabled)
                        Log.Debug("Loading Account by AccountID" + accountBasedDtoRequest.AccountId);
                    account = accountRepo.GetById(accountBasedDtoRequest.AccountId);

                }
                else
                {
                    throw new DevelopmentException("Request core model is IAccountBasedRequest but the DTO did not have any" +
                                        "accountId information in the request in DTO model, this is a development error");
                }

                accountBasedRequest.Account = account;
            }

            #endregion

            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            // ReSharper restore SuspiciousTypeConversion.Global
            // ReSharper restore ExpressionIsAlwaysNull
            MapNotAutomappedInboundProperties(request, ref coreInput);
            return (coreInput);
        }

        private TDTOOutput AutoMapOutboundFields(TInternalOutput coreOutput)
        {
            TDTOOutput dtoOutput = new TDTOOutput
            {
                errorCode = coreOutput.ErrorCode,
                messages = new[] {coreOutput.Message},
                resultType = coreOutput.ResultType,
            };

            #region Customer Based Response
            ICustomerBasedDTOResponse custBasedResp = dtoOutput as ICustomerBasedDTOResponse;
            if (custBasedResp != null)
            {
                ICustomerBasedResponse custCoreresp = coreOutput as ICustomerBasedResponse;
                if (custCoreresp != null)
                {
                    if (custCoreresp.Customer != null)
                        custBasedResp.Customer = custCoreresp.Customer.ToDto();
                }
                else
                {
                    throw new DevelopmentException("DTO Response is ICustomerBasedDTOResponse but the Core didn't have any" + 
                                                    " customerInfo assigned, this is a development error.");
                }
            }

            #endregion

            #region Number Info Response
            INumberInfoBasedDTOResponse numInfoBasedResp = dtoOutput as INumberInfoBasedDTOResponse;
            if (numInfoBasedResp != null)
            {
                INumberInfoBasedResponse numInfoCoreResp = coreOutput as INumberInfoBasedResponse;
                if (numInfoCoreResp != null) 
                {
                    if (numInfoCoreResp.NumberInPool != null)
                        numInfoBasedResp.NumberInPool = numInfoCoreResp.NumberInPool.ToDto();
                }
                else
                {
                    throw new DevelopmentException("DTO Response is INumberInfoBasedDTOResponse but the Core didn't have any" +
                                                    " numberInfo assigned, this is a development error.");
                }
            }

            #endregion

            #region SimCard Response
            ISimCardBasedDTOResponse simCardDTOBasedResp = dtoOutput as ISimCardBasedDTOResponse;
            if (simCardDTOBasedResp != null)
            {
                ISimCardBasedResponse simCardCoreResp = coreOutput as ISimCardBasedResponse;
                if (simCardCoreResp != null) 
                {
                    if (simCardCoreResp.SimCard != null)
                        simCardDTOBasedResp.SimCard =  simCardCoreResp.SimCard.ToDto();
                }
                else
                {
                    throw new DevelopmentException("DTO Response is ISimCardBasedDTOResponse but the Core didn't have any" +
                                                    " SimCardInfo assigned, this is a development error.");
                }
            }

            #endregion

            #region Subscription Based Response
            ISubscriptionBasedDTOResponse subscriptionDTOBasedResp = dtoOutput as ISubscriptionBasedDTOResponse;
            if (subscriptionDTOBasedResp != null)
            {
                ISubscriptionBasedResponse subscriptionCoreResp = coreOutput as ISubscriptionBasedResponse;
                if (subscriptionCoreResp != null)
                {
                    if (subscriptionCoreResp.Subscription != null)
                        subscriptionDTOBasedResp.Subscription = subscriptionCoreResp.Subscription.ToDto();
                }
                else
                {
                    throw new DevelopmentException("DTO Response is ISubscriptionBasedDTOResponse but the Core didn't have any" +
                                                   " ResourceMBInfo assigned, this is a development error.");
                }
            }

            #endregion

            #region Amount Based Response

            IAmountBasedDTOResponse amountDTOBasedResp = dtoOutput as IAmountBasedDTOResponse;
            if (amountDTOBasedResp != null)
            {
                IAmountBasedResponse amountResp = coreOutput as IAmountBasedResponse;
                if (amountResp != null)
                {
                    amountDTOBasedResp.Amount = amountResp.Amount;
                }
                else
                {
                    throw new DevelopmentException("DTO Response is IAmountBasedDTOResponse but the Core didn't have any" +
                        " amount assigned, this is a development error.");
                }
            }

            #endregion

            #region Product Based Response (Only used to fill the BusinessOperationExecution, not to convert to DTO)


            #endregion

            #region ProductOffering Based Response

            IProductOfferingBasedDTOResponse productDtoResp = dtoOutput as IProductOfferingBasedDTOResponse;
            if (productDtoResp != null)
            {
                IProductOfferingBasedResponse productResp = coreOutput as IProductOfferingBasedResponse;
                if (productResp != null)
                {
                    if (productResp.ProductOffering != null)
                        productDtoResp.ProductCatalog = productResp.ProductOffering.ToDto();
                }
                else
                {
                    throw new DevelopmentException("DTO Response is IProductOfferingBasedDTOResponse but the Core didn't have any" +
                                                   " ProductOffering assigned, this is a development error.");
                }
            }

            IMultiProductOfferingBasedDTOResponse multiProductDtoResp = dtoOutput as IMultiProductOfferingBasedDTOResponse;
            if (multiProductDtoResp != null)
            {
                IMultiProductOfferingBasedResponse multiProductResp = coreOutput as IMultiProductOfferingBasedResponse;
                if (multiProductResp != null)
                {
                    multiProductDtoResp.ProductCatalogDtos = multiProductResp.ProductOfferings.Select(x => x.ToDto());
                }
                else
                {
                    throw new DevelopmentException("DTO Response is IMultiProductOfferingBasedDTOResponse but the Core didn't have any" +
                                                   " ProductOffering assigned, this is a development error.");
                }
            }

            #endregion

            #region Charge Based Responses

            IChargeCatalogBasedDTOResponse chargeDtoResp = dtoOutput as IChargeCatalogBasedDTOResponse;
            if (chargeDtoResp != null)
            {
                IChargeBasedResponse chargeResp = coreOutput as IChargeBasedResponse;
                if (chargeResp != null)
                {
                    if (chargeResp.Charge != null)
                        chargeDtoResp.ChargeCatalogDto = chargeResp.Charge.ToDto();
                }
                else
                {
                    throw new DevelopmentException("DTO Response is IChargeCatalogBasedDTOResponse but the Core didn't have any" +
                                                   " Charge assigned, this is a development error.");
                }
            }

            IMultiChargeCatalogBasedDTOResponse multiChargeDtoResp = dtoOutput as IMultiChargeCatalogBasedDTOResponse;
            if (multiChargeDtoResp != null)
            {
                IMultiChargeBasedResponse multiChargeResp = coreOutput as IMultiChargeBasedResponse;
                if (multiChargeResp != null)
                {
                    multiChargeDtoResp.ChargeCatalogDtos = multiChargeResp.Charges.Select(x => x.ToDto());
                }
                else
                {
                    throw new DevelopmentException("DTO Response is IMultiChargeCatalogBasedDTOResponse but the Core didn't have any" +
                                                   " Charge assigned, this is a development error.");
                }
            }

            #endregion

            //At this point we already have mapped all that we know how to map
            //Now we need to leave control to implementation to finalize the mappings
            MapNotAutomappedOutboundProperties(coreOutput, ref dtoOutput);
            return dtoOutput;
        }

        /// <summary>
        /// In case the business Operation have been called directly with the Process method,
        /// we need to fill the BusinessOperationExecution Object with all the input
        /// parameters
        /// </summary>
        /// <param name="request">The request used to call the bizop</param>
        /// <param name="trazeObject">The trace to be filled with the data from the request</param>
        /// <returns>Returns a filled Object with all the information to be logged</returns>
        private void FillInputTrazedObjectFromRequest(TInternalInput request, BusinessOperationExecution trazeObject)
        {
            if (request == null || trazeObject == null)
                return;

            #region Customer based request
            var custRequest = request as ICustomerBasedRequest;
            if (custRequest != null)
                trazeObject.Customer = custRequest.Customer;
            #endregion
            
            #region Simcard based request
            var simCardReq = request as ISimCardBasedRequest;
            if (simCardReq != null)
                trazeObject.SimCard = simCardReq.SimCard;
            #endregion

            #region NumberInfo based request
            var numberInfoReq = request as INumberInfoBasedRequest;
            if (numberInfoReq != null)
                trazeObject.MSISDN = numberInfoReq.NumberInPool;
            #endregion

            #region Joint Customer based request
            var jointCustomerReq = request as IJointCustomerBasedRequest;
            if (jointCustomerReq != null)
            {
                trazeObject.Customer = jointCustomerReq.SourceCustomerInfo;
                trazeObject.CustomerDestination = jointCustomerReq.DestinationCustomerInfo;
            }
            #endregion

            #region Joint Subscription based request
            var jointSubscriptionReq = request as IJointSubscriptionBasedRequest;
            if (jointSubscriptionReq != null)
            {
                trazeObject.Subscription = jointSubscriptionReq.SourceSubscription;
                trazeObject.SubscriptionDestination = jointSubscriptionReq.DestinationSubscription;
            }
            #endregion

            #region Amount based request
            var amountReq = request as IAmountBasedRequest;
            if (amountReq != null)
            {
                trazeObject.Amount = amountReq.Amount;
            }
            #endregion

            #region Account based request

            var accountReq = request as IAccountBasedRequest;
            if (accountReq != null)
            {
                trazeObject.Account = accountReq.Account;
            }

            #endregion

            #region Subscription Last Active information request

            var subsReq = request as ISubscriptionLastActiveBasedRequest;
            if (subsReq != null)
            {
                trazeObject.Subscription = subsReq.Subscription;
            }

            #endregion

        }

        /// <summary>
        /// Gets the configuration from DB using the vmno and OperatorDiscriminator as discriminators
        /// </summary>
        /// <typeparam name="TConfig">The type of configuration to be recovered from the db</typeparam>
        /// <param name="vmno">the owner of the configuration, and will be used for filter</param>
        /// <returns>The configuration found</returns>
        protected TConfig GetOperationConfigForDealer<TConfig>(DealerInfo vmno) where TConfig : BasicOperationConfiguration
        {
            IOperationConfigurationRepository<OperationConfiguration> conifRepo =RepositoryManager.GetRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            IList<OperationConfiguration> configs = conifRepo.GetByDiscriminatorAndDealerId(vmno, GetSelfPeristedInstance()).ToList();
            if (!configs.Any())
            {
                String errMsg = String.Format("There is not any OperationConfiguration for Dealer:{0} and Operation:{1}", vmno, OperationDiscriminator);
                throw new InternalErrorException(errMsg, OperationErrorCodes.MissingConfiguration);
            }
            IEnumerable<OperationConfiguration> activeConfigs = configs.Where(x => x.StarTime < DateTime.Now && (!x.EndDate.HasValue || x.EndDate > DateTime.Now));
            IEnumerable<OperationConfiguration> operationConfigurations = activeConfigs as IList<OperationConfiguration> ?? activeConfigs.ToList();
            if (!operationConfigurations.Any())
            {
                String errMsg = String.Format("There is not any ACTIVE OperationConfiguration for Dealer:{0} and Operation:{1}", vmno, OperationDiscriminator);
                throw new InternalErrorException(errMsg, OperationErrorCodes.MissingActiveConfiguration);
            }

            if (operationConfigurations.Count() > 1)
            {
                String errMsg = String.Format("There is more than 1 ACTIVE OperationConfiguration for Dealer:{0} and Operation:{1}", vmno, OperationDiscriminator);
                throw new InternalErrorException(errMsg, OperationErrorCodes.MultipleActiveConfiguration);
            }

            OperationConfiguration config = operationConfigurations.First();

            if (String.IsNullOrWhiteSpace(config.JSonConfig))
            {
                String errMsg = String.Format("There OperationConfiguration for Dealer:{0} and Operation:{1} had not configuration field with data", vmno, OperationDiscriminator);
                throw new InternalErrorException(errMsg, OperationErrorCodes.NoConfigurationTextInOperationConfiguration);
            }

            try
            {
                TConfig conf = JsonConvert.DeserializeObject<TConfig>(config.JSonConfig);
                return conf;
            }
            catch (Exception)
            {
                String errMsg = String.Format("There OperationConfiguration for Dealer:{0} and Operation:{1} could not be serialized to {2}", vmno, OperationDiscriminator, typeof(TConfig).FullName);
                throw new InternalErrorException(errMsg, OperationErrorCodes.ErrorDesrializingOperationConfiguration);
            }

        }


    }
}
