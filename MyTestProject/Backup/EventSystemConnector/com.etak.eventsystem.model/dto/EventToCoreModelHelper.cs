using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Core = com.etak.core.model;

namespace com.etak.eventsystem.model.dto
{
    /// <summary>
    /// Autommaper profile for event system DTOs
    /// </summary>
    public class EventSystemProfile : Profile
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EventSystemProfile () : base()
        {           
        }

        /// <summary>
        /// Name of the profile
        /// </summary>
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        } 

        /// <summary>
        /// Implementation of profile to intialize the mapper
        /// </summary>
        protected override void Configure()
        {
            AllowNullCollections = true;
        }
    }
    
    /// <summary>
    /// Helper class to convert Core model to DTO version of the objects
    /// </summary>
    public static class EventToCoreModelHelper
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly String ProfileName = typeof(EventSystemProfile).Name;

        /// <summary>
        /// Extension method copied from: https://cangencer.wordpress.com/2011/06/08/auto-ignore-non-existing-properties-with-automapper/
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType)
                && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }

        static IMappingExpression<T1,T2> MapDTOEvent<T1, T2>() where T2 : LoadeableEntity
        {
            return AutoMapper.Mapper.CreateMap<T1, T2>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreAllSourcePropertiesWithAnInaccessibleSetter()                
                .IgnoreAllNonExisting()
                .WithProfile(ProfileName)
                .AfterMap((x, y) => { y.IsLoaded = true; });
            
        }

        /// <summary>
        /// Intializes all the mappings
        /// </summary>
        static EventToCoreModelHelper()
        {
            log.Info("Initializing event system mappers");

            MapDTOEvent<Core.DealerInfo, Dealer>();

            Mapper.AddProfile(new EventSystemProfile());

            MapDTOEvent<Core.ResourceMBInfo, MobileLineService>();
            MapDTOEvent<Core.PropertyInfo, CustomerProperty>();
            MapDTOEvent<Core.ServicesInfo, Service>();
            MapDTOEvent<Core.ProductInfo, Product>();
            MapDTOEvent<Core.CustomerInfo, Customer>()
                .ForMember(x => x.Address, opt => opt.MapFrom(y => y.FiscalAddress.Address))
                .ForMember(x => x.Area, opt => opt.MapFrom(y => y.FiscalAddress.Area))
                .ForMember(x => x.CountryID, opt => opt.MapFrom(y => y.FiscalAddress.CountryId))
                .ForMember(x => x.HouseExtention, opt => opt.MapFrom(y => y.FiscalAddress.HouseExtention))
                .ForMember(x => x.HouseNO, opt => opt.MapFrom(y => y.FiscalAddress.HouseNo))
                .ForMember(x => x.Neighborhood, opt => opt.MapFrom(y => y.FiscalAddress.Neighborhood))
                .ForMember(x => x.City, opt => opt.MapFrom(y => y.FiscalAddress.City))
                .ForMember(x => x.PoBox, opt => opt.MapFrom(y => y.FiscalAddress.PoBox))
                .ForMember(x => x.Status, opt => opt.MapFrom(y => y.StatusID))
                .ForMember(x => x.SUBURB, opt => opt.MapFrom(y => y.FiscalAddress.Suburb))
                .ForMember(x => x.Zipcode, opt => opt.MapFrom(y => y.FiscalAddress.ZipCode))
               .ForMember(x => x.ResourcesList, opt => opt.MapFrom(y => y.ResourceMBInfo))
               .ForMember(x => x.CustomerPropertyList, opt => opt.MapFrom(y => y.PropertyInfo))
               .ForMember(x => x.Services, opt => opt.MapFrom(y => y.ServicesInfo))
               .ForMember(x => x.Products, opt => opt.MapFrom(y => y.ProductsInfo));

            MapDTOEvent<Core.revenueManagement.Account, AccountDTO>()
                .ForMember(x => x.Balance, opt => opt.MapFrom(y => y.Balance.Balance))
                .ForMember(x => x.BillCycleId, opt => opt.MapFrom(y => y.BillingCycle.Id))
                .ForMember(x => x.CustomerId, opt => opt.MapFrom(y => y.CurrentAsignedCustomer.CustomerID))
                .ForMember(x => x.LastBillRunId, opt => opt.MapFrom(y => y.LastBillRun.Id));

            MapDTOEvent<Core.revenueManagement.CustomerProductAssignment, CustomerProductAssignmentDTO>()
                .ForMember(x => x.ProductChargePurchased, opt => opt.MapFrom(y => y.ProductChargePurchased.Id))
                .ForMember(x => x.PurchasedProduct, opt => opt.MapFrom(y => y.PurchasedProduct.Id))
                .ForMember(x => x.PurchasingCustomer, opt => opt.MapFrom(y => y.PurchasingCustomer.CustomerID));

            MapDTOEvent<Core.revenueManagement.Invoice, InvoiceDTO>()
                .ForMember(x => x.ChargedCustomerId, opt => opt.MapFrom(y => y.ChargedCustomer.CustomerID.Value))
                 .ForMember(x => x.ChargingAccountId, opt => opt.MapFrom(y => y.ChargingAccount.Id))
                 .ForMember(x => x.GeneratingBillRunId, opt => opt.MapFrom(y => y.GeneratingBillRun.Id));


            MapDTOEvent<Core.revenueManagement.CustomerChargeSchedule, CustomerChargeScheduleDTO>()
              .ForMember(x => x.ChargedAccountId, opt => opt.MapFrom(y => y.ChargedAccount.Id))
               .ForMember(x => x.ChargeDefinitionId, opt => opt.MapFrom(y => y.ChargeDefinition.Id))
               .ForMember(x => x.CustomerId, opt => opt.MapFrom(y => y.Customer.CustomerID))
               .ForMember(x => x.CustomerProductAssignmentId, opt => opt.MapFrom(y => y.Purchase.Id));


            MapDTOEvent<Core.revenueManagement.CustomerCharge, CustomerChargeDTO>()
             .ForMember(x => x.ChargeDefinitionId, opt => opt.MapFrom(y => y.ChargeDefinition.Id))
             .ForMember(x => x.ChargingAccountId, opt => opt.MapFrom(y => y.ChargingAccount.Id))
             .ForMember(x => x.CustomerId, opt => opt.MapFrom(y => y.Customer.CustomerID))
             .ForMember(x => x.CustomerProductAssignmentId, opt => opt.MapFrom(y => y.Product.Id))
             .ForMember(x => x.InvoiceId, opt => opt.MapFrom(y => y.Invoice.Id))
                //.ForMember(x => x.ScheduleId, opt => opt.MapFrom(y => y.Schedule.Id))
             .ForMember(x => x.ScheduleId, opt => opt.MapFrom(y => y.Schedule == null ? null : y.Schedule.Id as Nullable<Int64>))
             .ForMember(x => x.TaxId, opt => opt.MapFrom(y => y.Tax.Id));


            MapDTOEvent<Core.revenueManagement.Charge, ChargeDTO>();

        }

        /// <summary>
        /// Converts a core Invoice to a Event system DTO InvoiceDTO
        /// </summary>
        /// <param name="charge">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static ChargeDTO FromCoreCharge(Core.revenueManagement.Charge charge)
        {
            return (AutoMapper.Mapper.Map<Core.revenueManagement.Charge, ChargeDTO>(charge));
        }

        /// <summary>
        /// Converts a core Invoice to a Event system DTO InvoiceDTO
        /// </summary>
        /// <param name="invoice">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static CustomerChargeDTO FromCoreCustomerCharge(Core.revenueManagement.CustomerCharge invoice)
        {
            return (AutoMapper.Mapper.Map<Core.revenueManagement.CustomerCharge, CustomerChargeDTO>(invoice));
        }

        /// <summary>
        /// Converts a core Invoice to a Event system DTO InvoiceDTO
        /// </summary>
        /// <param name="invoice">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static InvoiceDTO FromCoreInvoice(Core.revenueManagement.Invoice invoice)
        {
            return (AutoMapper.Mapper.Map<Core.revenueManagement.Invoice, InvoiceDTO>(invoice));     
        }

        /// <summary>
        /// Converts a core CustomerChargeSchedule to a Event system DTO CustomerChargeScheduleDTO
        /// </summary>
        /// <param name="schedule">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static CustomerChargeScheduleDTO FromCoreCustomerChargeSchedule(Core.revenueManagement.CustomerChargeSchedule schedule)
        {
            return (AutoMapper.Mapper.Map<Core.revenueManagement.CustomerChargeSchedule, CustomerChargeScheduleDTO>(schedule));
        }       

        /// <summary>
        /// Converts a core CustomerProductAssignment to a Event system DTO CustomerProductAssignmentDTO
        /// </summary>
        /// <param name="assn">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static CustomerProductAssignmentDTO FromCoreCustomerProductAssignment(Core.revenueManagement.CustomerProductAssignment assn)
        {
            return (AutoMapper.Mapper.Map<Core.revenueManagement.CustomerProductAssignment, CustomerProductAssignmentDTO>(assn));
        }

        /// <summary>
        /// Converts a core Account to a Event system DTO AccountDTO
        /// </summary>
        /// <param name="account">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static AccountDTO FromCoreAccount(Core.revenueManagement.Account account)
        {
            return (AutoMapper.Mapper.Map<Core.revenueManagement.Account, AccountDTO>(account));
        }

        /// <summary>
        /// Converts a core CustomerInfo to a Event system DTO Customer
        /// </summary>
        /// <param name="custInfo">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static Customer FromCoreCustomerInfo(Core.CustomerInfo custInfo)
        {           
            return (AutoMapper.Mapper.Map<Core.CustomerInfo, Customer>(custInfo));
        }

        /// <summary>
        /// Converts a core ProductInfo to a Event system DTO Product
        /// </summary>
        /// <param name="dealer">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static Product FromCoreProductInfo(Core.ProductInfo dealer)
        {
            return (AutoMapper.Mapper.Map<Core.ProductInfo, Product>(dealer));
        }

        /// <summary>
        /// Converts a core ServicesInfo to a Event system DTO Service
        /// </summary>
        /// <param name="servInfo">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static Service FromCoreServicesInfo(Core.ServicesInfo servInfo)
        {
            return (AutoMapper.Mapper.Map<Core.ServicesInfo, Service>(servInfo));
        }

        /// <summary>
        /// Converts a core ResourceMBInfo to a Event system DTO MobileLineService
        /// </summary>
        /// <param name="rmb">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static MobileLineService FromCoreResourceMb(Core.ResourceMBInfo rmb)
        {            
            return (AutoMapper.Mapper.Map<Core.ResourceMBInfo, MobileLineService>(rmb));
        }

        /// <summary>
        /// Converts a core ResourceMBInfo to a Event system DTO MobileLineService
        /// </summary>
        /// <param name="propertyInfo">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static CustomerProperty FromCorePropertyInfo(Core.PropertyInfo propertyInfo)
        {
            return (AutoMapper.Mapper.Map<Core.PropertyInfo, CustomerProperty>(propertyInfo));
        }

        /// <summary>
        /// Converts a core DealerInfo to a Event system DTO Dealer
        /// </summary>
        /// <param name="dealer">the core object to convert</param>
        /// <returns>the coverted object in DTO form</returns>
        public static Dealer FromCoreDealerInfo(Core.DealerInfo dealer)
        {
            return (AutoMapper.Mapper.Map<Core.DealerInfo, Dealer>(dealer));
        }       
    }
}
