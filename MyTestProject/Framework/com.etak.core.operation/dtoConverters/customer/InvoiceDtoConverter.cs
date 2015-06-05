using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.dtoConverters.customer
{
    /// <summary>
    /// Converter to convert from Invoice to InvoiceDTO. That converter will need a chargeId corresponding
    /// to the aggregate Total Invoice Charge Id
    /// </summary>
    public class InvoiceDtoConverter : contract.ITypeConverter<Invoice, InvoiceDTO>
    {
        /// <summary>
        /// Static constructor
        /// </summary>
        static InvoiceDtoConverter()
        {
            Mapper.CreateMap<Invoice, InvoiceDTO>()
                .ForMember(dst => dst.InvoiceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.BillingCycle,
                    opt =>
                        opt.MapFrom(src => src.GeneratingBillRun != null && src.GeneratingBillRun.BillingCycle != null
                            ? src.GeneratingBillRun.BillingCycle.Id
                            : 0));
        }

        /// <summary>
        /// Convert a Invoice object to a InvoiceDTO
        /// </summary>
        /// <param name="source">The Invoice to be converted</param>
        /// <returns></returns>
        public InvoiceDTO Convert(Invoice source)
        {
            return Mapper.Map<InvoiceDTO>(source);
        }

        /// <summary>
        /// Main function to be called passing the Invoice and the chargeId needed to calculate the 
        /// total amount of the invoice
        /// </summary>
        /// <param name="source">Invoice to be converted</param>
        /// <param name="aggregateTotalInvoiceChargeId">Charge Id corresponding to the aggregate Total Invoice Charge Id</param>
        /// <returns></returns>
        public InvoiceDTO Convert(Invoice source, int aggregateTotalInvoiceChargeId)
        {
            var invoiceDto = Convert(source);
            List<CustomerCharge> customerChargeList = source.Charges.Where(x => x.ChargeDefinition.Id == aggregateTotalInvoiceChargeId).ToList();
            if (customerChargeList.Any())
            {
                CustomerCharge customerCharge = customerChargeList.First();
                invoiceDto.Amount = customerCharge.Amount + ((customerCharge.TaxAmount != null) ? customerCharge.TaxAmount.Value : 0);
            }

            return invoiceDto;
        }
    }
}
