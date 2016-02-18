using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.eventsystem.model.dto;
using Core = com.etak.core.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.eventsystem.test
{
    static class DefaultObjectCreator
    {
        public static readonly DateTime dateValue = DateTime.Now;
        public const Int32 intValue = 314159;
        public const Int64 longValue = 314159;
        public const Int16 shortValue = 31415;
        public const String stringValue = "3.14159";
        public const Boolean booleanValue = !default(Boolean);
        public const Decimal decimalValue = 3.14159M;
        public const Double doubleValue = 3.14159D;
        public static readonly Byte[] byteArrayValue = null; //new Byte[] { 0x00, 0x01, 0x02 };

        public static void CheckIsAssignedValue<T>(T sourceValue)
        {
            Object val = null;
            if (typeof(T) == typeof(String))
                val = stringValue;
            else if (typeof(T) == typeof(Int16))
                val = shortValue;
            else if (typeof(T) == typeof(Int32) || typeof(T) == typeof(Nullable<Int32>))
                val = intValue;
            else if (typeof(T) == typeof(Int64) || typeof(T) == typeof(Nullable<Int64>))
                val = longValue;
            else if (typeof(T) == typeof(Decimal) || typeof(T) == typeof(Nullable<Decimal>))
                val = decimalValue;
            else if (typeof(T) == typeof(Double) || typeof(T) == typeof(Nullable<Double>))
                val = doubleValue;
            else if (typeof(T) == typeof(Nullable<DateTime>) || typeof(T) == typeof(DateTime))
                val = dateValue;
            else if (typeof(T) == typeof(Boolean))
                val = booleanValue;
            else if (typeof(T) == typeof(Byte[]))
                val = byteArrayValue;
            else
                throw new Exception(String.Format("The Type: {0} is not handled by testss", typeof(T)));

            Assert.AreEqual(sourceValue, val);
        }


        public static Core.revenueManagement.CustomerProductAssignment CreateCustomerProductAssignment()
        {
            return new Core.revenueManagement.CustomerProductAssignment()
            {
                CreateDate = dateValue,
                EndDate = dateValue,
                Id = longValue,
                ProductChargePurchased = CreateProductPurchaseOption(),
                PurchasedProduct = CreateProduct(),
                PurchasingCustomer = CreateCustomerInfo(),
                StartDate = dateValue,

            };

        }

        public static Core.revenueManagement.Product CreateProduct()
        {
            return new Core.revenueManagement.Product()
            {
                AssociatedBundle = null,
                AssociatedPrmotionPlan = null,
                AssociatedPrmotionGroup = null,
                ChargingOptions = null,
                ChildProducts = null,
                Description = CreateMultiLingualDescription(),
                Id = intValue,
                ProductRelationDependencies = null,
                Names = CreateMultiLingualDescription(),
                ParentProducts = null,
                Type = CreateProductType(),
            };
        }

        public static Core.revenueManagement.ProductType CreateProductType()
        {
            return new Core.revenueManagement.ProductType()
            {
                Description = stringValue,
                Id = intValue,
            };
        }

        public static Core.revenueManagement.ProductChargeOption CreateProductPurchaseOption()
        {
            return new Core.revenueManagement.ProductChargeOption()
            {
                Charges = null,
                CreateDate = dateValue,
                Description = CreateMultiLingualDescription(),
                EndDate = dateValue,
                Id = intValue,
                IsDefaultOption = Core.revenueManagement.DefaultOptions.Y,
                Name = CreateMultiLingualDescription(),
                ProductOfOption = null,
                StartDate = dateValue,
                Status = Core.revenueManagement.ProductPurchaseStatus.Default

            };
        }


        public static Core.revenueManagement.Account CreateAccount()
        {
            return new Core.revenueManagement.Account
            {
                Balance = CreateBalanceForaccount(),
                BillingCycle = CreateBillingCycle(),
                CurrentAsignedCustomer = CreateCustomerInfo(),
                Description = CreateMultiLingualDescription(),
                Id = longValue,
                LastBillRun = CreateBillRun(),
                Name = CreateMultiLingualDescription(),
                Type = Core.revenueManagement.AccountType.Data,
            };
        }

        public static Core.revenueManagement.BillRun CreateBillRun()
        {
            return new Core.revenueManagement.BillRun()
            {
                BillingCycle = CreateBillingCycle(),
                CutOffDate = dateValue,
                DueDate = dateValue,
                EndDate = dateValue,
                FirstUsageDetailId = longValue,
                Id = intValue,
                LastUsageDetailId = longValue,
                RunDate = dateValue,
                StarteDate = dateValue,
                
            };
        }

        public static Core.revenueManagement.BillCycle CreateBillingCycle()
        {
            return new Core.revenueManagement.BillCycle
            {
                CutOffDay = shortValue,
                DaysUntilLate = intValue,
                Description = CreateMultiLingualDescription(),
                Id = intValue,
                PeriodQuantity = intValue,
                PeriodUnit = Core.TimeUnits.Minute,
                RunDay = shortValue,
                VMNO = CreateDealerInfo()
            };
        }

        public static Core.MultiLingualDescription CreateMultiLingualDescription()
        {
            return new Core.MultiLingualDescription
            {
                DefaultMessage = stringValue,
                Id = intValue,
                Texts = new List<Core.LanguageSpecificText> {  
                    CreateLanguageSpecificText(),  CreateLanguageSpecificText()
                }
            };
        }

        public static Core.LanguageSpecificText CreateLanguageSpecificText()
        {
            return new Core.LanguageSpecificText()
            {
                //Description = stringValue,
                Language = Core.ISO639LanguageCodes.abk,
                Text = stringValue,
            };
        }

        public static Core.revenueManagement.BalanceForAccount CreateBalanceForaccount()
        {
            return new Core.revenueManagement.BalanceForAccount
            {
                Balance = decimalValue,
                Id = longValue
            };
        }


        public static Core.ProductInfo CreateProductInfo()
        {
            return new Core.ProductInfo()
            {
                CreateDate = dateValue,
                CreditLimit = decimalValue,
                CustomerInfo = null,
                EndDate = dateValue,
                ExactcreditLimit = decimalValue,
                PackageDefinition = null,
                ProductID = intValue,
                ServiceInfo = null,
                ServiceTypeID = intValue,
                SpecialCreditLimit = decimalValue,
                StartDate = dateValue,
                UserID = intValue,
            };
        }

        public static Core.ServicesInfo CreateServicesInfo()
        {
            return new Core.ServicesInfo()
            {
                BilledBalance = decimalValue,
                BundleDefinition = null,
                CreateDate = dateValue,
                CreditLimit = decimalValue,
                CREDITLIMITBASEBUNDLEID = intValue,
                CustomerInfo = null,
                DeleteFlag = booleanValue,
                DepositAmount = doubleValue,
                EndDate = dateValue,
                InvoiceTemplateID = intValue,
                PointToBaseBundle = booleanValue,
                ProductInfo = null,
                ServiceID = intValue,
                StartDate = dateValue,
                Status = intValue,
                UnBilledBalance = decimalValue,
                UpdateStamp = byteArrayValue,
                UserID = intValue,
            };
        }

        public static Core.DealerInfo CreateDealerInfo()
        {
            return new Core.DealerInfo()
            {
                Address = stringValue,
                AgentID = intValue,
                CHOC = stringValue,
                City = stringValue,
                Company = stringValue,
                Contact = stringValue,
                CountryID = intValue,
                CreateDate = dateValue,
                CreateUser = intValue,
                CurrentCrmDefaultProvisionInfoList = null,
                DealerATMTopUpConfigInfo = null,
                DealerBankList = null,
                DealerID = intValue,
                DealerLoyaltyList = null,
                DealerNode = stringValue,
                DealerOBOPRSList = null,
                DealerPropertiesList = null,
                DealerRatePlanList = null,
                DealerTypeID = intValue,
                Email = stringValue,
                FiscalUnitID = intValue,
                GenderID = intValue,
                Hide = intValue,
                HouseNO = stringValue,
                MVNOConfigActionList = null,
                MvnoDataRoamingLimitList = null,
                MVNOPropertiesInfo = null,
                MvnotypeID = intValue,
                ParentID = intValue,
                ResellerID = intValue,
                RoamingSettingList = null,
                State = intValue,
                SubagentID = intValue,
                Telefax = stringValue,
                Telephone = stringValue,
                TitleID = intValue,
                UpdateDate = dateValue,
                UpdateUser = intValue,
                UserID = intValue,
                VAT = stringValue,
                Zipcode = stringValue
            };
        }

        public static Core.ResourceMBInfo CreateResourceMB()
        {
            return new Core.ResourceMBInfo
            {
                ActiveDeadlineDate = dateValue,
                BearerServiceList = stringValue,
                Calculation = intValue,
                CBPassword = stringValue,
                CBSubsoption = intValue,
                CBWrongAttempts = intValue,
                ChangeStatusDate = dateValue,
                CreateDate = dateValue,
                CustomerInfo = null,
                DeleteFlag = booleanValue,
                EndDate = dateValue,
                FirstUsed = dateValue,
                FrozenDate = dateValue,
                FTNRule = stringValue,
                ICC = stringValue,
                IMSI = stringValue,
                LastConsumeDate = dateValue,
                LastUsed = dateValue,
                MainNumberStatus = intValue,
                MainNumberVoiceMailStatus = intValue,
                MobileType = stringValue,
                MsIsdnAlertInd = stringValue,
                NAM = intValue,
                OCPPlmnTemplateId = intValue,
                ODBMask = stringValue,
                OperatorInfo = null,
                PINInvalidTimes = intValue,
                PINInvalidTimesTotal = intValue,
                PortedNO = stringValue,
                ProvisionId = intValue,
                PUK = stringValue,
                Remarks = stringValue,
                Resource = stringValue,
                ResourceDIDInfo = null,
                ResourceID = intValue,
                StartDate = dateValue,
                StatusID = intValue,
                TeleServiceList = stringValue,
                TempNO = stringValue,
                UserID = intValue,
                UssdAllowed = booleanValue,
                WelcomeSMS = booleanValue
            };
        }

        public static Core.PropertyInfo CreatePropertyInfo()
        {
            return new Core.PropertyInfo()
            {
                AcceptNews = booleanValue,
                ActionCode = stringValue,
                AutoTopupAmount = decimalValue,
                AutoTopupStatus = intValue,
                BillingEntity = intValue,
                BillingMethodID = intValue,
                BillingScenarioID = intValue,
                Birthday = dateValue,
                ContractNo = intValue,
                ContractPeriod = intValue,
                CountryCode = intValue,
                CPPCOUNTER = intValue,
                CPPCOUNTER_STARTDATE = dateValue,
                CPSCode = stringValue,
                CreateDate = dateValue,
                CreditScore = intValue,
                CreditTransferType = intValue,
                CurrentDepositAmount = decimalValue,
                CurrentDepositCredit = decimalValue,
                CustomerID = intValue,
                CustomerInfo = null,
                CustomerTypeID = intValue,
                DateUpdated = dateValue,
                DepositDate = dateValue,
                DepositStatus = intValue,
                DMCEndUserId = stringValue,
                Email = stringValue,
                ExternalId = stringValue,
                FF = booleanValue,
                IDExpiryDate = dateValue,
                IDNumber = stringValue,
                IDType = intValue,
                InvoiceDetails = booleanValue,
                InvoiceDueDate = intValue,
                LanguageID = intValue,
                LastLoyaltyDate = dateValue,
                LoginType = intValue,
                LoyaltyPoint = intValue,
                MailType = stringValue,
                NEEDLCSENDWELCOMESMS = intValue,
                NEXT_PACKAGE_DATE = dateValue,
                NEXT_PACKAGEID = intValue,
                OriginalDepositAmount = decimalValue,
                ParentBilling = booleanValue,
                PasswordDES = stringValue,
                PasswordMD5 = stringValue,
                PaymentMethodID = intValue,
                PendingStatus = intValue,
                PropertyID = intValue,
                SubscriberType = stringValue,
                TaxPlanID = intValue,
                TopupChangePackageDate = dateValue,
                TrafficTypeID = intValue,
                UserID = intValue,
                UserName = stringValue,
                VATNO = stringValue,
                WithDrawPeriod = intValue
            };
        }

        public static Core.CustomerInfo CreateCustomerInfo()
        {
            Core.ResourceMBInfo rmb = CreateResourceMB();
            Core.PropertyInfo prop = CreatePropertyInfo();
            Core.ServicesInfo serv = CreateServicesInfo();
            Core.ProductInfo prod = CreateProductInfo();
            Core.AddressInfo add = new Core.AddressInfo()
            {
                Address = stringValue,
                Area = stringValue,
                Block = stringValue,
                BuildingDoor = stringValue,
                City = stringValue,
                Comments = stringValue,
                CountryId = intValue,
                CreateDate = dateValue,
                CreateUser = intValue,
                Door = stringValue,
                HouseExtention = stringValue,
                HouseNo = stringValue,
                Id = intValue,
                Neighborhood = stringValue,
                PoBox = intValue,
                Portal = stringValue,
                Province = intValue,
                Stair = stringValue,
                State = stringValue,
                Status = intValue,
                Suburb = stringValue,
                ZipCode = stringValue,
            };

            return new Core.CustomerInfo()
            {
                ResourceMBInfo = new List<Core.ResourceMBInfo>() { rmb },
                PropertyInfo = new List<Core.PropertyInfo>() { prop },
                ActivedDate = dateValue,
                Addresses = new List<Core.CustomerAddress>() { new Core.CustomerAddress() { Address = add, UsageType = Core.AddressUsages.FiscalAddress } },
                BankInfo = null,
                BillingDate = intValue,
                Category = stringValue,
                Choc = stringValue,
                Company = stringValue,
                Contact = stringValue,
                CreateDate = dateValue,
                CreateUserName = stringValue,
                CustomerCreditCards = null,
                CustomerID = intValue,
                DateOfBirth = dateValue,
                DealerID = intValue,
                Email = stringValue,
                FirstName = stringValue,
                //FiscalAddress = stringValue,
                GenderID = intValue,
                Initials = stringValue,
                LastName = stringValue,
                LastName2 = stringValue,
                MappingInfo = null,
                MiddleName = stringValue,
                Mobile = stringValue,
                MVNOCustomerPropertyInfo = null,
                ParentControl = intValue,
                ParentID = intValue,
                ProductsInfo = new List<Core.ProductInfo> { prod },
                PromotionGroups = null,
                Promotions = null,
                ProvisionResourceMBInfo = new List<Core.CrmCustomersResourceMbInfo>() { new Core.CrmCustomersResourceMbInfo() },
                RegistrationType = intValue,
                RemarksInfo = new List<Core.RemarksInfo> { new Core.RemarksInfo() },
                RevenueProductsInfo = new List<Core.revenueManagement.CustomerProductAssignment> { new Core.revenueManagement.CustomerProductAssignment() },
                SalesSellerID = stringValue,
                SalesShopID = stringValue,
                ServicesInfo = new List<Core.ServicesInfo>() { serv },
                StatusID = intValue,
                Telefax = stringValue,
                Telephone = stringValue,
                TitleID = intValue,
                UpdateStamp = byteArrayValue,
                UserID = intValue,
                VAT = stringValue,
            };
        }

        public static Core.revenueManagement.CustomerCharge CreateCustomerCharge()
        {
            return new Core.revenueManagement.CustomerCharge()
            {
                Amount = decimalValue,
                ChargeDefinition = CreateChargeDefinition(),
                ChargingAccount = CreateAccount(),
                ChargingDate = dateValue,
                Comments = stringValue ,
                Currency = Core.ISO4217CurrencyCodes.AZN,
                Customer = CreateCustomerInfo(),
                CycleNumber = longValue,
                ExternalReferenceCode = stringValue,
                Id = longValue,
                InformationalAmount = decimalValue,
                Invoice = CreateInvoice(),
                PeriodNumber = longValue,
                Product = CreateCustomerProductAssignment(),
                Schedule = CreateCustomerChargeSchedule(),
                Tax = CreateTaxDefinition(),
                TaxAmount = decimalValue,
            };
        }

        public static Core.revenueManagement.TaxDefinition CreateTaxDefinition()
        {
            return new Core.revenueManagement.TaxDefinition
            {
                Description = CreateMultiLingualDescription(),
                Id = intValue ,
                Rates = null,
                TaxCategory = intValue,
                ZipRanges = null
            };
        }

        public static Core.revenueManagement.CustomerChargeSchedule CreateCustomerChargeSchedule()
        {
            return new Core.revenueManagement.CustomerChargeSchedule()
            {
                ChargedAccount = DefaultObjectCreator.CreateAccount(),
                ChargeDefinition = CreateChargeDefinition(),
                Charges = null,
                CreateDate = dateValue,
                CurrentCyclenumber = intValue,
                Customer = DefaultObjectCreator.CreateCustomerInfo(),
                Id = intValue,
                NextChargeDate = dateValue,
                NextPeriodNumber = intValue,
                PriceEffectiveDate = dateValue,
                Purchase = DefaultObjectCreator.CreateCustomerProductAssignment(),
                Status = Core.revenueManagement.ScheduleChargeStatus.InProcess,
                UpdateDate = dateValue,
            };
        }

        public static Core.revenueManagement.Charge CreateChargeDefinition()
        {
            return new Core.revenueManagement.ChargeNonRecurring
            {
                AmountComputer = null,
                Category = intValue,
                CreateTime = dateValue,
                Description = CreateMultiLingualDescription(),
                GeneralLedgerAccount = stringValue,
                Id = intValue,
                InformationalOnly = Core.revenueManagement.InformationalTypes.Y,
                Name = CreateMultiLingualDescription(),
                Prices = null,
                ProrateQty = decimalValue,
                ProrateUnit = com.etak.core.model.TimeUnits.Hour
            };
        }

        public static Core.revenueManagement.Invoice CreateInvoice()
        {
            return new Core.revenueManagement.Invoice()
            {
                ChargedCustomer = CreateCustomerInfo(),
                Charges = null,
                ChargingAccount = CreateAccount(),
                EndDate = dateValue,
                GeneratingBillRun = CreateBillRun(),
                Id = longValue,
                InvoiceFileName = stringValue ,
                LegalInvoiceNumber = stringValue,
                StartDate = dateValue,
                Status = Core.revenueManagement.InvoiceStatus.Drafted
            };
        }
    }
}
