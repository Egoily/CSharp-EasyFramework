using com.etak.core.model.operation.contract.amount;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.numberInfo;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.operations.messages
{
    public class FakeBizOpRequest : RequestBase, IAmountBasedRequest, ICustomerBasedRequest, IJointSubscriptionBasedRequest, IAccountBasedRequest, INumberInfoBasedRequest, ISimCardBasedRequest, ISubscriptionLastActiveBasedRequest
    {
        public decimal Amount { get; set; }

        public model.CustomerInfo Customer { get; set; }

        public string DestinationMSISDN { get; set; }

        public model.ResourceMBInfo DestinationSubscription { get; set; }


        public string SourceMSISDN { get; set; }

        public model.ResourceMBInfo SourceSubscription { get; set; }

        public model.revenueManagement.Account Account { get; set; }

        public model.NumberInfo NumberInPool { get; set; }

        public model.SIMCardInfo SimCard { get; set; }

        public string MSISDN { get; set; }

        public model.ResourceMBInfo Subscription
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
    }

    public class FakeBizOpResponse : ResponseBase, IAmountBasedResponse, ICustomerBasedResponse, IJointCustomerBasedRequest, INumberInfoBasedResponse
    {
        public decimal Amount { get; set; }



        public model.CustomerInfo Customer { get; set; }

        public model.CustomerInfo DestinationCustomerInfo { get; set; }

        public model.CustomerInfo SourceCustomerInfo { get; set; }

        public model.NumberInfo NumberInPool { get; set; }
    }


    public class FakeBizOpRequestDTO : RequestBaseDTO, IAmountBasedDTORequest, ICustomerIdBasedDTORequest, IExternalCustomerIdBasedDTORequest, IDocumentIdBasedDTORequest, IJointCustomerIdDTOBasedRequest, IJointMsisdnDTOBasedRequest, IAccountIdBasedDTORequest, IMsisdnBasedDTORequest, IICCIDBasedDTORequest
    {
        public decimal amount { get; set; }

        public int CustomerId { get; set; }

        public string ExternalCustomerId { get; set; }

        public string DocumentNumber { get; set; }

        public int DocumentType { get; set; }

        public int DestinationCustomerId { get; set; }

        public int SourceCustomerId { get; set; }


        public string DestinationMSISDN { get; set; }

        public string SourceMSISDN { get; set; }

        public long AccountId { get; set; }

        public string MSISDN { get; set; }

        public string ICCID { get; set; }
    }

    public class FakeBizOpResponseDTO : ResponseBaseDTO, IAmountBasedDTOResponse, ICustomerBasedDTOResponse, INumberInfoBasedDTOResponse
    {
        public decimal Amount { get; set; }

        public model.dto.CustomerDTO Customer { get; set; }

        public model.resource.MSISDNResourceDTO NumberInPool { get; set; }
    }

}
