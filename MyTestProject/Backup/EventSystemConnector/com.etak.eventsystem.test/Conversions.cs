using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.etak.eventsystem.model.dto;
using Core = com.etak.core.model;
using System.Collections.Generic;

namespace com.etak.eventsystem.test
{
    [TestClass]
    public class Conversions
    {
        [TestMethod]
        public void FromCoreChargeToChargeDTO()
        {
            Core.revenueManagement.Charge source = DefaultObjectCreator.CreateChargeDefinition();
            ChargeDTO dest = EventToCoreModelHelper.FromCoreCharge(source);

            DefaultObjectCreator.CheckIsAssignedValue(dest.Category);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreateTime);
            DefaultObjectCreator.CheckIsAssignedValue(dest.GeneralLedgerAccount);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Id);
            Assert.AreEqual(dest.InformationalOnly, InformationalTypes.Y);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ProrateQty);
            Assert.AreEqual(dest.ProrateUnit, com.etak.core.model.TimeUnits.Hour);
          

        }

        [TestMethod]
        public void FromResourceMbToMobileLineService()
        {
            Core.ResourceMBInfo source = DefaultObjectCreator.CreateResourceMB();
            MobileLineService dest = EventToCoreModelHelper.FromCoreResourceMb(source);

            DefaultObjectCreator.CheckIsAssignedValue(dest.ActiveDeadlineDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.BearerServiceList);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Calculation);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CBPassword);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CBSubsoption);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CBWrongAttempts);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ChangeStatusDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreateDate);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.CustomerInfo , null);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DeleteFlag);
            DefaultObjectCreator.CheckIsAssignedValue(dest.EndDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.FirstUsed);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.FrozenDate );
            DefaultObjectCreator.CheckIsAssignedValue(dest.FTNRule);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ICC);
            DefaultObjectCreator.CheckIsAssignedValue(dest.IMSI);
            DefaultObjectCreator.CheckIsAssignedValue(dest.LastConsumeDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.LastUsed);
            DefaultObjectCreator.CheckIsAssignedValue(dest.MainNumberStatus);
            DefaultObjectCreator.CheckIsAssignedValue(dest.MainNumberVoiceMailStatus);
            DefaultObjectCreator.CheckIsAssignedValue(dest.MobileType);
            DefaultObjectCreator.CheckIsAssignedValue(dest.MsIsdnAlertInd);
            DefaultObjectCreator.CheckIsAssignedValue(dest.NAM);
            DefaultObjectCreator.CheckIsAssignedValue(dest.OCPPlmnTemplateId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ODBMask);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.OperatorInfo , null);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PINInvalidTimes);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PINInvalidTimesTotal);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PortedNO);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ProvisionId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PUK);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Remarks);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Resource);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.ResourceDIDInfo , null);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ResourceID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.StartDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.StatusID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.TeleServiceList);
            DefaultObjectCreator.CheckIsAssignedValue(dest.TempNO);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UserID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UssdAllowed);
            DefaultObjectCreator.CheckIsAssignedValue(dest.WelcomeSMS);
        }

        [TestMethod]
        public void FromPropertyInfoToCustomerProperty()
        {
            Core.PropertyInfo source = DefaultObjectCreator.CreatePropertyInfo();

            CustomerProperty dest = EventToCoreModelHelper.FromCorePropertyInfo(source);

            DefaultObjectCreator.CheckIsAssignedValue(dest.AcceptNews);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ActionCode);
            DefaultObjectCreator.CheckIsAssignedValue(dest.AutoTopupAmount);
            DefaultObjectCreator.CheckIsAssignedValue(dest.AutoTopupStatus);
            DefaultObjectCreator.CheckIsAssignedValue(dest.BillingEntity);
            DefaultObjectCreator.CheckIsAssignedValue(dest.BillingMethodID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.BillingScenarioID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Birthday);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ContractNo);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ContractPeriod);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CountryCode);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.CPPCOUNTER);
            // DefaultObjectCreator.CheckIsAssignedValue(dest.CPPCOUNTER_STARTDATE);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CPSCode);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreateDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreditScore);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreditTransferType);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CurrentDepositAmount);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CurrentDepositCredit);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CustomerID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CustomerTypeID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DateUpdated);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DepositDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DepositStatus);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DMCEndUserId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Email);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ExternalId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.FF);
            DefaultObjectCreator.CheckIsAssignedValue(dest.IDExpiryDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.IDNumber);
            DefaultObjectCreator.CheckIsAssignedValue(dest.IDType);
            DefaultObjectCreator.CheckIsAssignedValue(dest.InvoiceDetails);
            DefaultObjectCreator.CheckIsAssignedValue(dest.InvoiceDueDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.LanguageID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.LastLoyaltyDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.LoginType);
            DefaultObjectCreator.CheckIsAssignedValue(dest.LoyaltyPoint);
            DefaultObjectCreator.CheckIsAssignedValue(dest.MailType);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.NEEDLCSENDWELCOMESMS);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.NEXT_PACKAGE_DATE);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.NEXT_PACKAGEID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.OriginalDepositAmount);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ParentBilling);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PasswordDES);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PasswordMD5);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PaymentMethodID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PendingStatus);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PropertyID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.SubscriberType);
            DefaultObjectCreator.CheckIsAssignedValue(dest.TaxPlanID);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.TopupChangePackageDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.TrafficTypeID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UserID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UserName);
            DefaultObjectCreator.CheckIsAssignedValue(dest.VATNO);
            DefaultObjectCreator.CheckIsAssignedValue(dest.WithDrawPeriod);

        }

        [TestMethod]
        public void FromCoreCustomerChargeToCoreCustomerChargeDTO()
        {
            Core.revenueManagement.CustomerCharge source = DefaultObjectCreator.CreateCustomerCharge();
            CustomerChargeDTO dest = EventToCoreModelHelper.FromCoreCustomerCharge(source);

            DefaultObjectCreator.CheckIsAssignedValue(dest.Id);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Amount);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ChargeDefinitionId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ChargingAccountId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ChargingDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Comments);
            Assert.AreEqual(dest.Currency, Core.ISO4217CurrencyCodes.AZN);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CustomerId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CustomerProductAssignmentId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CycleNumber);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ExternalReferenceCode);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Id);
            DefaultObjectCreator.CheckIsAssignedValue(dest.InformationalAmount);
            DefaultObjectCreator.CheckIsAssignedValue(dest.InvoiceId);

        }

        [TestMethod]
        public void FromCustomerInfoToCustomer()
        {
            Core.CustomerInfo source = DefaultObjectCreator.CreateCustomerInfo();
            Customer dest = EventToCoreModelHelper.FromCoreCustomerInfo(source);
            Assert.IsTrue(dest.Products.Count > 0);
            Assert.IsTrue(dest.ResourcesList.Count > 0);
            Assert.IsTrue(dest.Services.Count > 0);
            Assert.IsTrue(dest.CustomerPropertyList.Count > 0);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ActivedDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.BillingDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Choc);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Company);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Contact);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreateDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreateUserName);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CustomerID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DateOfBirth);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DealerID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Email);
            DefaultObjectCreator.CheckIsAssignedValue(dest.FirstName);
            DefaultObjectCreator.CheckIsAssignedValue(dest.GenderID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Initials);
            DefaultObjectCreator.CheckIsAssignedValue(dest.LastName);
            DefaultObjectCreator.CheckIsAssignedValue(dest.LastName2);
            DefaultObjectCreator.CheckIsAssignedValue(dest.MiddleName);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Mobile);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ParentControl);
            DefaultObjectCreator.CheckIsAssignedValue(dest.SalesSellerID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.SalesShopID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Telefax);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Telephone);
            DefaultObjectCreator.CheckIsAssignedValue(dest.TitleID);
            Assert.AreEqual(dest.UpdateStamp, null);
            DefaultObjectCreator.CheckIsAssignedValue(dest.VAT);
        }

        [TestMethod]
        public void FromDealerInfoToDealer()
        {
            Core.DealerInfo source = DefaultObjectCreator.CreateDealerInfo();
            Dealer dest = EventToCoreModelHelper.FromCoreDealerInfo(source);

            DefaultObjectCreator.CheckIsAssignedValue(dest.Address);
            DefaultObjectCreator.CheckIsAssignedValue(dest.AgentID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CHOC);
            DefaultObjectCreator.CheckIsAssignedValue(dest.City);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Company);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Contact);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CountryID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreateDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreateUser);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.CurrentCrmDefaultProvisionInfoList, null);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.DealerATMTopUpConfigInfo, null);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.DealerBankList, null);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DealerID);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.DealerLoyaltyList, null);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DealerNode);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.DealerOBOPRSList, null);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.DealerPropertiesList, null);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.DealerRatePlanList, null);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DealerTypeID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Email);
            DefaultObjectCreator.CheckIsAssignedValue(dest.FiscalUnitID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.GenderID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Hide);
            DefaultObjectCreator.CheckIsAssignedValue(dest.HouseNO);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.MVNOConfigActionList, null);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.MvnoDataRoamingLimitList, null);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.MVNOPropertiesInfo, null);
            DefaultObjectCreator.CheckIsAssignedValue(dest.MvnotypeID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ParentID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ResellerID);
            //DefaultObjectCreator.CheckIsAssignedValue(dest.RoamingSettingList, null);
            DefaultObjectCreator.CheckIsAssignedValue(dest.State);
            DefaultObjectCreator.CheckIsAssignedValue(dest.SubagentID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Telefax);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Telephone);
            DefaultObjectCreator.CheckIsAssignedValue(dest.TitleID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UpdateDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UpdateUser);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UserID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.VAT);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Zipcode);
        }

        [TestMethod]
        public void FromServicesInfoToService()
        {
            Core.ServicesInfo source = DefaultObjectCreator.CreateServicesInfo();
            Service dest = EventToCoreModelHelper.FromCoreServicesInfo(source);
            DefaultObjectCreator.CheckIsAssignedValue(dest.BilledBalance);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreateDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreditLimit);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CREDITLIMITBASEBUNDLEID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DeleteFlag);
            DefaultObjectCreator.CheckIsAssignedValue(dest.DepositAmount);
            DefaultObjectCreator.CheckIsAssignedValue(dest.EndDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.InvoiceTemplateID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PointToBaseBundle);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ServiceID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.StartDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Status);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UnBilledBalance);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UpdateStamp);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UserID);
        }

        [TestMethod]
        public void FromProductInfoToProduct()
        {
            Core.ProductInfo source = DefaultObjectCreator.CreateProductInfo();
            Product dest = EventToCoreModelHelper.FromCoreProductInfo(source);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreateDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreditLimit);
            DefaultObjectCreator.CheckIsAssignedValue(dest.EndDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ExactcreditLimit);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ProductID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ServiceTypeID);
            DefaultObjectCreator.CheckIsAssignedValue(dest.SpecialCreditLimit);
            DefaultObjectCreator.CheckIsAssignedValue(dest.StartDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UserID);
        }

        [TestMethod]
        public void FromAccountToAccountDTO()
        {
            Core.revenueManagement.Account source = DefaultObjectCreator.CreateAccount();
            AccountDTO dest = EventToCoreModelHelper.FromCoreAccount(source);

            DefaultObjectCreator.CheckIsAssignedValue(dest.Balance);
            DefaultObjectCreator.CheckIsAssignedValue(dest.BillCycleId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CustomerId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Id);
            DefaultObjectCreator.CheckIsAssignedValue(dest.LastBillRunId);
        }

        [TestMethod]
        public void FromInvoiceToInvoiceDTO()
        {
            Core.revenueManagement.Invoice source = DefaultObjectCreator.CreateInvoice();
            InvoiceDTO dest = EventToCoreModelHelper.FromCoreInvoice(source);

            DefaultObjectCreator.CheckIsAssignedValue(dest.ChargedCustomerId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ChargingAccountId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.EndDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.GeneratingBillRunId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Id);
            DefaultObjectCreator.CheckIsAssignedValue(dest.InvoiceFileName);
            DefaultObjectCreator.CheckIsAssignedValue(dest.LegalInvoiceNumber);
            DefaultObjectCreator.CheckIsAssignedValue(dest.StartDate);
            Assert.AreEqual(dest.Status, InvoiceStatuses.Drafted);
        }

        [TestMethod]
        public void FromCoreCustomerChargeScheduleToCoreCustomerChargeScheduleDTO()
        {
            Core.revenueManagement.CustomerChargeSchedule source = DefaultObjectCreator.CreateCustomerChargeSchedule();
            CustomerChargeScheduleDTO dest = EventToCoreModelHelper.FromCoreCustomerChargeSchedule(source);

            DefaultObjectCreator.CheckIsAssignedValue(dest.ChargedAccountId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ChargeDefinitionId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreateDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CurrentCyclenumber);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CustomerId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CustomerProductAssignmentId);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Id);
            DefaultObjectCreator.CheckIsAssignedValue(dest.NextChargeDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.NextPeriodNumber);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PriceEffectiveDate);
            Assert.AreEqual(dest.Status, ScheduleChargeStatuses.Default);
            DefaultObjectCreator.CheckIsAssignedValue(dest.UpdateDate);
        }

       
        [TestMethod]
        public void FromCustomerProductAssignmentToCustomerProductAssignmentDTO()
        {
            Core.revenueManagement.CustomerProductAssignment source = DefaultObjectCreator.CreateCustomerProductAssignment();
            CustomerProductAssignmentDTO dest = EventToCoreModelHelper.FromCoreCustomerProductAssignment(source);
            DefaultObjectCreator.CheckIsAssignedValue(dest.CreateDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.EndDate);
            DefaultObjectCreator.CheckIsAssignedValue(dest.Id);
            DefaultObjectCreator.CheckIsAssignedValue(dest.ProductChargePurchased);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PurchasedProduct);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PurchasedProduct);
            DefaultObjectCreator.CheckIsAssignedValue(dest.PurchasedProduct);
        }

    }
}
