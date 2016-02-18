using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.etak.core.customer.message.CreateAddresses;
using com.etak.core.customer.message.UpdateCustomerInfo;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.dtoConverters.customer;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using log4net;

namespace com.etak.core.bizops.fullfilment.UpdateCustomerData
{
    /// <summary>
    /// Update CustomerInfo base on given information
    /// </summary>
    public class UpdateCustomerDataBizOp : AbstractSinglePhaseOrderProcessor<UpdateCustomerDataDTORequest, UpdateCustomerDataDTOResponse, UpdateCustomerDataInternalRequest, UpdateCustomerDataInternalResponse, UpdateCustomerOrder>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Map Not Automap Inbound properties UpdateCustomerDataDTORequest to UpdateCustomerDataInternalRequest
        /// </summary>
        /// <param name="request">UpdateCustomerDataDTORequest</param>
        /// <param name="coreInput">UpdateCustomerDataInternalRequest</param>
        protected override void MapNotAutomappedOrderInboundProperties(UpdateCustomerDataDTORequest request, ref UpdateCustomerDataInternalRequest coreInput)
        {

            if (coreInput.Customer == null)
            {
                throw new DataValidationErrorException("The requested customer doesn't exist", BizOpsErrors.CustomerInfoNotFound);
            }
            if (coreInput.Customer.StatusID == (int)CustomerStatus.Deleted)
            {
                throw new DataValidationErrorException("Can not update customer that already deleted", BizOpsErrors.CustomerAlreadyDeleted);
            }

        #region checkAuthorization
                var microServiceCheckAuthorization =
                    MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
                var checkAuthorizationRequest = new CheckAuthorizationRequest { UserInfo = coreInput.User, DealerId = coreInput.Customer.DealerID != null ? coreInput.Customer.DealerID.Value : 0 };
                var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
                if (!checkAuthorizationResponse.IsAuthorized)
                    throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
        #endregion

            ValidateRequest(request);
            coreInput.NewCustomerInfo = request.Customer;

            // Changes for Ticket: CBO-240
            #region Do not allow to modify the Document Type and Number for the customer

            CustomerDocumentTypeAndNumberValidation(coreInput);

            #endregion
        }
        /// <summary>
        /// Map Not Automap Inbound properties UpdateCustomerDataInternalResponse to UpdateCustomerDataDTOResponse
        /// </summary>
        /// <param name="source">UpdateCustomerDataInternalResponse</param>
        /// <param name="coreOutput">UpdateCustomerDataDTOResponse</param>
        protected override void MapNotAutomappedOrderOutboundProperties(UpdateCustomerDataInternalResponse source, ref UpdateCustomerDataDTOResponse coreOutput)
        {
            if (source.UpdatedCustomer != null)
            {
                coreOutput.Customer = source.UpdatedCustomer.ToDto();
            }

            coreOutput.resultType = source.ResultType;
        }

        /// <summary>
        /// Business Logic of UpdateCustomerDataOperation
        /// </summary>
        /// <param name="order">UpdateCustomerOrder</param>
        /// <param name="request">UpdateCustomerDataInternalRequest</param>
        /// <returns>UpdateCustomerDataInternalResponse as Internal output</returns>
        public override UpdateCustomerDataInternalResponse ProcessRequest(UpdateCustomerOrder order, UpdateCustomerDataInternalRequest request)
        {
            var newDto = request.NewCustomerInfo;
            #region customerDto

            request.Customer.TitleID = (int?)newDto.CustomerData.Title;
            request.Customer.FirstName = newDto.CustomerData.FirstName;
            request.Customer.MiddleName = newDto.CustomerData.MiddleName;
            request.Customer.LastName = newDto.CustomerData.LastName;
            request.Customer.LastName2 = newDto.CustomerData.LastName2;
            request.Customer.GenderID = (int?)newDto.CustomerData.Gender;
            request.Customer.Mobile = newDto.CustomerData.Mobile;
            request.Customer.Email = newDto.CustomerData.Email;
            request.Customer.Initials = newDto.CustomerData.Initials;
            request.Customer.DateOfBirth = newDto.CustomerData.BirthDay;
            request.Customer.Company = newDto.CustomerData.Company;
            request.Customer.Telephone = newDto.CustomerData.Telephone;
            request.Customer.Telefax = newDto.CustomerData.Telefax;
            #endregion customerDto

            #region propertyInfo

            var propertyInfo = request.Customer.PropertyInfo[0];
            propertyInfo.Email = newDto.CustomerData.Email;
            propertyInfo.IDNumber = newDto.CustomerData.DocumentNumber;
            propertyInfo.ExternalId = newDto.ExternalCustomerId;
            if (DtoDictionaries.DocEnumToIntMapper.ContainsKey(newDto.CustomerData.DocumentType))
                propertyInfo.IDType = DtoDictionaries.DocEnumToIntMapper[newDto.CustomerData.DocumentType];
            else
                propertyInfo.IDType = 0;

            request.Customer.PropertyInfo[0] = propertyInfo;
            #endregion propertyInfo

            #region MVNOCustomerProperties
            MVNOCustomerPropertyInfo mvnoProperty = null;
            if (!request.Customer.MVNOCustomerPropertyInfo.Any())
            {
                mvnoProperty = new MVNOCustomerPropertyInfo();
                mvnoProperty.CustomerInfo = request.Customer;

                request.Customer.MVNOCustomerPropertyInfo.Add(mvnoProperty);
            }
            else
            {
                mvnoProperty = request.Customer.MVNOCustomerPropertyInfo.First();
            }
            mvnoProperty.Nationality = newDto.CustomerData.Nationality;

            #endregion MVNOCustomerProperties
            
            #region bankDto

            //BankInfo newBankInfo = new BankDtoConverter().Convert(newDto.CustomerData.BankInformation);
            BankInfo newBankInfo = newDto.CustomerData.BankInformation.ToCore();
            newBankInfo.CreateDate = newDto.CustomerData.BankInformation.CreateDate;

            if (newBankInfo != null)
            {
                if (!newBankInfo.EndDate.HasValue)
                {
                    BankInfo oldBankInfo = request.Customer.BankInfo.FirstOrDefault(x => x.EndDate == null);
                    if (oldBankInfo != null)
                        oldBankInfo.EndDate = DateTime.Now;
                }

                BankInfo actualBankInfo = request.Customer.BankInfo.FirstOrDefault(x => CompareInformation(x, newBankInfo));
                if (actualBankInfo != null)
                {
                    actualBankInfo.EndDate = newBankInfo.EndDate;

                    if (newBankInfo.StartDate != null && newBankInfo.StartDate != actualBankInfo.StartDate)
                        actualBankInfo.StartDate = newBankInfo.StartDate;
                }
                else
                {
                    newBankInfo.CreateDate = DateTime.Now;
                    newBankInfo.UserID = request.User.UserID;
                    newBankInfo.CustomerInfo = request.Customer;
                    newBankInfo.StartDate = DateTime.Now;
                    request.Customer.BankInfo.Add(newBankInfo);
                }

            }


            #endregion bankDto

            #region Address
            // need to convert Dto -> core obj, to call MS
            var newCustomerInfo = newDto.ToCore();

            foreach (var usageType in Enum.GetValues(typeof(AddressUsages)))
            {
                var oldAddress = request.Customer.Addresses.FirstOrDefault(x => x.UsageType == (AddressUsages)usageType);
                var newAddress = newCustomerInfo.Addresses.FirstOrDefault(x => x.UsageType == (AddressUsages)usageType);

                if (((newAddress != null) && (oldAddress != null)) && !IsSameAddress(oldAddress.Address, newAddress.Address))
                {
                    newAddress.Address.CreateDate = DateTime.Now;
                    List<CustomerAddress> tmp = new List<CustomerAddress>() { newAddress };
                    //call microservice to create new address
                    IMicroService<CreateAddressesRequest, CreateAddressesResponse> createAddressMS =
                        MicroServiceManager.GetMicroService<CreateAddressesRequest, CreateAddressesResponse>();
                    var createAddressMSRequest = new CreateAddressesRequest()
                    {
                        CustomerAddresses = tmp
                    };

                    Log.InfoFormat("Calling microservice : createAddressMS, create address base on given information : ({0}). ", createAddressMSRequest.CustomerAddresses.ToString());

                    var createAddressMSResponse = createAddressMS.Process(createAddressMSRequest, null);

                    if (createAddressMSResponse != null)
                    {
                        request.Customer.Addresses.Remove(oldAddress);
                        request.Customer.Addresses.Add(newAddress);
                    }
                }
            }

            #endregion Address

            //call microservice to update customer
            IMicroService<UpdateCustomerInfoRequest, UpdateCustomerInfoResponse> UpdateCustomerMS =
                MicroServiceManager.GetMicroService<UpdateCustomerInfoRequest, UpdateCustomerInfoResponse>();

            var updateCustomerMsRequest = new UpdateCustomerInfoRequest()
            {
                CustomerInfo = request.Customer
            };

            Log.InfoFormat("Calling microservice : UpdateCustomerMS, update CustomerInfo base on given information : ({0}). ", updateCustomerMsRequest.CustomerInfo.ToString());

            var updateCustomerMsResponse = UpdateCustomerMS.Process(updateCustomerMsRequest, null);

            return new UpdateCustomerDataInternalResponse()
            {
                UpdatedCustomer = updateCustomerMsResponse.CustomerInfo,
                Subscription = updateCustomerMsResponse.CustomerInfo == null ? null : updateCustomerMsResponse.CustomerInfo.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active),
                ResultType = ResultTypes.Ok
            };
        }

        /// <summary>
        /// OperationCode for UpdateCustomer
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.UpdateCustomerOperation; }
        }

        /// <summary>
        /// OperationDiscriminator for UpdateCustomer
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.UpdateCustomerOperation; }
        }

        /// <summary>
        /// Validate UpdateCustomerData DTORequest 
        /// </summary>
        /// <param name="request"></param>
        private void ValidateRequest(UpdateCustomerDataDTORequest request)
        {
            if (request.Customer.Equals(null))
                throw new BusinessLogicErrorException("Customer cannot be null ", BizOpsErrors.NotValidCustomer);

            if (string.IsNullOrEmpty(request.Customer.ExternalCustomerId))
                throw new DataValidationErrorException("The ExternalCustomerId is Mandatory!", BizOpsErrors.CustomerDoesNotHaveExternalCustomerId);

            if (string.IsNullOrEmpty(request.Customer.CustomerData.FirstName))
                throw new DataValidationErrorException("The FirstName is Mandatory!", BizOpsErrors.CustomerDoesNotHaveFirstName);

            if (string.IsNullOrEmpty(request.Customer.CustomerData.LastName))
                throw new DataValidationErrorException("The LastName is Mandatory!", BizOpsErrors.CustomerDoesNotHaveLastName);

            if (string.IsNullOrEmpty(request.Customer.CustomerData.LastName2))
                throw new DataValidationErrorException("The LastName2 is Mandatory!", BizOpsErrors.CustomerDoesNotHaveLastName2);

            if (request.Customer.CustomerData.FiscalAddress == null)
                throw new DataValidationErrorException("The FiscalAddress is Mandatory!", BizOpsErrors.CustomerDoesNotHaveFiscalAddress);
            else
                CheckMandatoryFieldsForAddress(request.Customer.CustomerData.FiscalAddress, "FiscalAddress");

            if (request.Customer.CustomerData.CustomerAddress == null)
                throw new DataValidationErrorException("The CustomerAddress is Mandatory!", BizOpsErrors.CustomerDoesNotHaveCustomerAddress);
            else
                CheckMandatoryFieldsForAddress(request.Customer.CustomerData.CustomerAddress, "CustomerAddress");

            if (request.Customer.CustomerData.DeliveryAddress == null)
                throw new DataValidationErrorException("The DeliveryAddress is Mandatory!", BizOpsErrors.CustomerDoesNotHaveDeliveryAddress);
            else
                CheckMandatoryFieldsForAddress(request.Customer.CustomerData.DeliveryAddress, "DeliveryAddress");
        }

        /// <summary>
        /// Check Mandatory Fields For Address
        /// </summary>
        /// <param name="address">AddressDTO</param>
        /// <param name="strAddress">string</param>
        private void CheckMandatoryFieldsForAddress(AddressDTO address, string strAddress)
        {
            if (address != null)
            {
                if (string.IsNullOrEmpty(address.Address))
                    throw new DataValidationErrorException("The " + strAddress + ".Address is Mandatory!", BizOpsErrors.CustomerDoesNotHaveCustomerAddress);
                if (string.IsNullOrEmpty(address.City))
                    throw new DataValidationErrorException("The " + strAddress + ".City is mandatory!", BizOpsErrors.CustomerDoesNotHaveCity);
                if (address.CountryId <= 0)
                    throw new DataValidationErrorException("The " + strAddress + ".CountryId is mandatory!", BizOpsErrors.CustomerDoesNotHaveCountryId);
                if (string.IsNullOrEmpty(address.HouseNo))
                    throw new DataValidationErrorException("The " + strAddress + ".HouseNo is mandatory!", BizOpsErrors.CustomerDoesNotHaveHouseNo);
                if (string.IsNullOrEmpty(address.State))
                    throw new DataValidationErrorException("The " + strAddress + ".State is mandatory!", BizOpsErrors.CustomerDoesNotHaveState);
                if (string.IsNullOrEmpty(address.ZipCode))
                    throw new DataValidationErrorException("The " + strAddress + ".ZipCode is mandatory!", BizOpsErrors.CustomerNoZipcode);
            }
        }

        /// <summary>
        /// check whether those two Address infos has same values 
        /// </summary>
        /// <param name="origin">AddressInfo</param>
        /// <param name="compareObj">AddressInfo</param>
        /// <returns>boolean</returns>
        private bool IsSameAddress(AddressInfo origin, AddressInfo compareObj)
        {
            AddressDTO originDto = origin.ToDto();
            AddressDTO compareObjDto = compareObj.ToDto();

            return originDto.Equals(compareObjDto);
        }

        /// <summary>
        /// Compare the important information in two Bank Info entities
        /// </summary>
        /// <param name="oldInfo">BankInformationDTO</param>
        /// <param name="newInfo">BankInformationDTO</param>
        /// <returns>true if both are equals, false otherwise</returns>
        public static bool CompareInformation(BankInfo oldInfo, BankInfo newInfo)
        {
            return ((oldInfo.ABI == newInfo.ABI)
                && oldInfo.AccountCode == newInfo.AccountCode
                && oldInfo.BankCode == newInfo.BankCode
                && oldInfo.BankName == newInfo.BankName
                && oldInfo.BankNumber == newInfo.BankNumber
                && oldInfo.CAB == newInfo.CAB
                && oldInfo.CVC == newInfo.CVC
                && oldInfo.City == newInfo.City
                && oldInfo.CountryID == newInfo.CountryID
                && oldInfo.IBAN == newInfo.IBAN
                && oldInfo.Owner == newInfo.Owner
                && oldInfo.Swift == newInfo.Swift);
        }

        /// <summary>
        /// Document Type and Number changes validation for the customer
        /// </summary>
        /// <param name="request">UpdateCustomerData Internal Request</param>
        private void CustomerDocumentTypeAndNumberValidation(UpdateCustomerDataInternalRequest request)
        {
            var customerPropertyInfo = request.Customer.PropertyInfo[0];

            if (DtoDictionaries.DocEnumToIntMapper.ContainsKey(request.NewCustomerInfo.CustomerData.DocumentType))
            {
                if (customerPropertyInfo.IDType != DtoDictionaries.DocEnumToIntMapper[request.NewCustomerInfo.CustomerData.DocumentType])
                    throw new DataValidationErrorException("Do not allow to modify Document Type for the customer.", BizOpsErrors.CustomerDocumentTypeHasChanged);
            }

            if (customerPropertyInfo.IDNumber != request.NewCustomerInfo.CustomerData.DocumentNumber)
                throw new DataValidationErrorException("Do not allow to modify Document Number for the customer.", BizOpsErrors.CustomerDocumentNumberHasChanged);
        }
    }
}
