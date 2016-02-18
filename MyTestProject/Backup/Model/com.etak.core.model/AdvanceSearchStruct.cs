using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    public class AdvanceSearchStruct
    {        
        private int? _CustomerIDOperation = null;        
        private string _CustomerIDValue;        

        private int? _CompanyOperation = null;        
        private string _CompanyValue;        

        private int? _ContactOperation = null;        
        private string _ContactValue;        

        private int? _AddressOperation = null;        
        private string _AddressValue;        

        private int? _HouseNOOperation = null;        
        private string _HouseNOValue;        

        private int? _ZipcodeOperation = null;        
        private string _ZipcodeValue;        

        private int? _ResourceOperation = null;        
        private string _ResourceValue;        

        private int? _InvoiceIDOperation = null;        
        private string _InvoiceIDValue;        

        private int? _ICCOperation = null;        
        private string _ICCValue;        

        private int? _PendingStatusOperation = null;        
        private string _PendingStatusValue;        

        private int? _MsisdnOperation = null;        
        private string _MsisdnValue;        

        private int? _CityOperation = null;        
        private string _CityValue;        

        private int? _EmailOperation = null;        
        private string _EmailValue;        

        private int? _BankNumberOperation = null;        
        private string _BankNumberValue;        

        private int? _IDTypeOperation = null;        
        private string _IDTypeValue;        

        private int? _IDNumberOperation = null;        
        private string _IDNumberValue;       

        private int? _customerTypeOperation = null;
        private string _customerTypeValue;

        //private int? _resourceOperation = null;          

        private int? _paymentMethodOperation = null;
        private string _paymentMethodValue;

        private int? _billingMethodOperation = null;
        private string _billingMethodValue;

        private int? _bundleOperation = null;
        private string _bundleValue;

        private int? _ResourceMBStatusOperation = null;        
        private string _ResourceMBStatusValue;
        

        #region Attribute       
        public int? CustomerIDOperation
        {
            get { return _CustomerIDOperation; }
            set { _CustomerIDOperation = value; }
        }

        public string CustomerIDValue
        {
            get { return _CustomerIDValue; }
            set { _CustomerIDValue = value; }
        }

        public int? CompanyOperation
        {
            get { return _CompanyOperation; }
            set { _CompanyOperation = value; }
        }

        public string CompanyValue
        {
            get { return _CompanyValue; }
            set { _CompanyValue = value; }
        }

        public int? ContactOperation
        {
            get { return _ContactOperation; }
            set { _ContactOperation = value; }
        }

        public string ContactValue
        {
            get { return _ContactValue; }
            set { _ContactValue = value; }
        }

        public int? AddressOperation
        {
            get { return _AddressOperation; }
            set { _AddressOperation = value; }
        }

        public string AddressValue
        {
            get { return _AddressValue; }
            set { _AddressValue = value; }
        }

        public int? HouseNOOperation
        {
            get { return _HouseNOOperation; }
            set { _HouseNOOperation = value; }
        }

        public string HouseNOValue
        {
            get { return _HouseNOValue; }
            set { _HouseNOValue = value; }
        }

        public int? ZipcodeOperation
        {
            get { return _ZipcodeOperation; }
            set { _ZipcodeOperation = value; }
        }

        public string ZipcodeValue
        {
            get { return _ZipcodeValue; }
            set { _ZipcodeValue = value; }
        }

        public int? ResourceOperation
        {
            get { return _ResourceOperation; }
            set { _ResourceOperation = value; }
        }

        public string ResourceValue
        {
            get { return _ResourceValue; }
            set { _ResourceValue = value; }
        }

        public int? InvoiceIDOperation
        {
            get { return _InvoiceIDOperation; }
            set { _InvoiceIDOperation = value; }
        }

        public string InvoiceIDValue
        {
            get { return _InvoiceIDValue; }
            set { _InvoiceIDValue = value; }
        }

        public int? ICCOperation
        {
            get { return _ICCOperation; }
            set { _ICCOperation = value; }
        }

        public string ICCValue
        {
            get { return _ICCValue; }
            set { _ICCValue = value; }
        }

        public int? PendingStatusOperation
        {
            get { return _PendingStatusOperation; }
            set { _PendingStatusOperation = value; }
        }

        public string PendingStatusValue
        {
            get { return _PendingStatusValue; }
            set { _PendingStatusValue = value; }
        }

        public int? MsisdnOperation
        {
            get { return _MsisdnOperation; }
            set { _MsisdnOperation = value; }
        }

        public string MsisdnValue
        {
            get { return _MsisdnValue; }
            set { _MsisdnValue = value; }
        }

        public int? CityOperation
        {
            get { return _CityOperation; }
            set { _CityOperation = value; }
        }

        public string CityValue
        {
            get { return _CityValue; }
            set { _CityValue = value; }
        }

        public int? EmailOperation
        {
            get { return _EmailOperation; }
            set { _EmailOperation = value; }
        }

        public string EmailValue
        {
            get { return _EmailValue; }
            set { _EmailValue = value; }
        }

        public int? BankNumberOperation
        {
            get { return _BankNumberOperation; }
            set { _BankNumberOperation = value; }
        }

        public string BankNumberValue
        {
            get { return _BankNumberValue; }
            set { _BankNumberValue = value; }
        }

        public int? IDTypeOperation
        {
            get { return _IDTypeOperation; }
            set { _IDTypeOperation = value; }
        }

        public string IDTypeValue
        {
            get { return _IDTypeValue; }
            set { _IDTypeValue = value; }
        }

        public int? IDNumberOperation
        {
            get { return _IDNumberOperation; }
            set { _IDNumberOperation = value; }
        }

        public string IDNumberValue
        {
            get { return _IDNumberValue; }
            set { _IDNumberValue = value; }
        }
        public int? CustomerTypeOperation
        {
            get { return _customerTypeOperation; }
            set { _customerTypeOperation = value; }
        }

        public string CustomerTypeValue
        {
            get { return _customerTypeValue ; }
            set { _customerTypeValue = value; }
        }

        public int? PaymentMethodOperation
        {
            get { return _paymentMethodOperation; }
            set { _paymentMethodOperation = value; }
        }

        public string PaymentMethodValue
        {
            get { return _paymentMethodValue; }
            set { _paymentMethodValue = value; }
        }


        public int? BillingMethodOperation
        {
            get { return _billingMethodOperation; }
            set { _billingMethodOperation = value; }
        }

        public string BillingMethodValue
        {
            get { return _billingMethodValue; }
            set { _billingMethodValue = value; }
        }


        public int? BundleOperation
        {
            get { return _bundleOperation; }
            set { _bundleOperation = value; }
        }

        public string BundleValue
        {
            get { return _bundleValue; }
            set { _bundleValue = value; }
        }

        public virtual int? ResourceMBStatusOperation
        {
            get { return _ResourceMBStatusOperation; }
            set { _ResourceMBStatusOperation = value; }
        }

        public virtual string ResourceMBStatusValue
        {
            get { return _ResourceMBStatusValue; }
            set { _ResourceMBStatusValue = value; }
        }

        public string PremiumFreePhone
        {
            get;
            set;
        }

        public int? PremiumFreePhoneOperation
        {
            get { return _billingMethodOperation; }
            set { _billingMethodOperation = value; }
        }

        public string Routing
        {
            get;
            set;
        }

        public int? RoutingOperation
        {
            get;
            set;
        }

        public string TariffCode
        {
            get;
            set;
        }

        public int? TariffCodeOperation
        {
            get;
            set;
        }
        #endregion
    }
}
