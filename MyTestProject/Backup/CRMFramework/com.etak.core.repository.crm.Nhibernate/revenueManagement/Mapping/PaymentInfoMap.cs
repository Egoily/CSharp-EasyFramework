using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// Class to map to Nhibernate class AccountData
    /// </summary>
    public class PaymentInfoMap : ClassMap<PaymentInfo>
    {
        //[INVOICE_PAYMENTID] [bigint] NOT NULL IDENTITY(1, 1),
        //[INVOICEID] [bigint] NOT NULL,
        //[ORDERID] [bigint] NULL,
        //[STATUSID] [int] NOT NULL,
        //[PAYMENT_METHODID] [int] NOT NULL,
        //[AMOUNT] [decimal] (19, 8) NULL,
        //[DISCOUNT] [decimal] (19, 8) NULL,
        //[ISO4217_CURRENCY_CODE] [char] (3) COLLATE Latin1_General_CI_AS NULL,
        //[EXTERNAL_PAYMENTID] [varchar] (255) COLLATE Latin1_General_CI_AS NULL,
        //[PAYMENT_INFO] [varchar] (1024) COLLATE Latin1_General_CI_AS NULL

        /// <summary>
        /// The constructor called by fluent to map the class. 
        /// </summary>
        public PaymentInfoMap()
        {
            Schema("dbo");
            Table("CRM_INVOICE_PAYMENTS");
            DynamicUpdate();
            Id(x => x.Id).GeneratedBy.Identity().Column("INVOICE_PAYMENTID");

            References(x => x.Invoice, "INVOICEID");
            References(x => x.Order, "ORDERID");

            Map(x => x.Status, "STATUSID");
            Map(x => x.PaymentMethod, "PAYMENT_METHODID");
            Map(x => x.Amount, "AMOUNT");
            Map(x => x.Discount, "DISCOUNT");
            Map(x => x.Currency, "ISO4217_CURRENCY_CODE").CustomType<ISO4217CurrencyCodes>();
            Map(x => x.ExternalPayment, "EXTERNAL_PAYMENTID");
            Map(x => x.PaymentInfoText, "PAYMENT_INFO");
            Map(x => x.TaxInfo, "TAX_INFO");
        }

    }
}
