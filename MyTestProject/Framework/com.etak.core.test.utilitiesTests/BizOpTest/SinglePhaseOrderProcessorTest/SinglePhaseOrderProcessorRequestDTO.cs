using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.test.utilitiesTests.BizOpTest.SinglePhaseOrderProcessorTest
{
    public class SinglePhaseOrderProcessorRequestDTO : OrderRequestDTO, IMsisdnBasedDTORequest, ICustomerIdBasedDTORequest, IDocumentIdBasedDTORequest, IExternalCustomerIdBasedDTORequest, IICCIDBasedDTORequest, IJointCustomerIdDTOBasedRequest, IJointMsisdnDTOBasedRequest, IAccountIdBasedDTORequest 
    {
        public string MSISDN { get; set; }
        public int CustomerId { get; set; }
        public int DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string ExternalCustomerId { get; set; }
        public string ICCID { get; set; }
        public int SourceCustomerId { get; set; }
        public int DestinationCustomerId { get; set; }
        public string SourceMSISDN { get; set; }
        public string DestinationMSISDN { get; set; }
        public long AccountId { get; set; }
    }
}
