using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using com.etak.core.model;
using com.etak.core.model.dto;

namespace com.etak.core.operation.dtoConverters.customer
{
    /// <summary>
    /// Converter from CustomerInfo to CustomerDto and viceversa
    /// </summary>
    public class CustomerDtoConverter : contract.ITypeConverter<CustomerInfo, CustomerDTO>, contract.ITypeConverter<CustomerDTO, CustomerInfo>
    {
        /// <summary>
        /// Constructor to set up all the mappings
        /// </summary>
        static CustomerDtoConverter()
        {
            #region CustomerDTO -> CustomerInfo Mappings


            Mapper.CreateMap<CustomerDTO, PropertyInfo>()
                .ForMember(x => x.CreateDate, opt => opt.UseValue(DateTime.Now))
                .ForMember(x => x.Email, opt => opt.MapFrom(y => y.CustomerData.Email))
                .ForMember(x => x.IDType, opt => opt.MapFrom(y => (y.CustomerData.DocumentType != null && DtoDictionaries.DocEnumToIntMapper.ContainsKey(y.CustomerData.DocumentType)) ?
                                                                  DtoDictionaries.DocEnumToIntMapper[y.CustomerData.DocumentType] : 0))
                .ForMember(x => x.IDNumber, opt => opt.MapFrom(y => y.CustomerData.DocumentNumber))
                .ForMember(x => x.ExternalId, opt => opt.MapFrom(y => y.ExternalCustomerId));

            Mapper.CreateMap<CustomerDTO, MVNOCustomerPropertyInfo>()
                .ForMember(x => x.Nationality, opt => opt.MapFrom(y => y.CustomerData.Nationality));

            Mapper.CreateMap<CustomerDTO, CustomerInfo>()
                .ForMember(x => x.ParentID, opt => opt.MapFrom(y => y.ParentCustomerId))
                .ForMember(x => x.CreateDate, opt => opt.UseValue(DateTime.Now))
                .ForMember(x => x.TitleID, opt => opt.MapFrom(y => (int?)y.CustomerData.Title))
                .ForMember(x => x.GenderID, opt => opt.MapFrom(y => (int?)y.CustomerData.Gender))
                .ForMember(x => x.Initials, opt => opt.MapFrom(y => y.CustomerData.Initials))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(y => y.CustomerData.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(y => y.CustomerData.LastName))
                .ForMember(x => x.LastName2, opt => opt.MapFrom(y => y.CustomerData.LastName2))
                .ForMember(x => x.MiddleName, opt => opt.MapFrom(y => y.CustomerData.MiddleName))
                .ForMember(x => x.Company, opt => opt.MapFrom(y => y.CustomerData.Company))
                .ForMember(x => x.Telephone, opt => opt.MapFrom(y => y.CustomerData.Telephone))
                .ForMember(x => x.Telefax, opt => opt.MapFrom(y => y.CustomerData.Telefax))
                .ForMember(x => x.Mobile, opt => opt.MapFrom(y => y.CustomerData.Mobile))
                .ForMember(x => x.Email, opt => opt.MapFrom(y => y.CustomerData.Email))
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(y => y.CustomerData.BirthDay))
                .ForMember(x => x.BankInfo, opt => opt.MapFrom(y => new List<BankInfo>() { Mapper.Map<BankInfo>(y.CustomerData.BankInformation) }))
                .ForMember(x => x.PropertyInfo, opt => opt.MapFrom(y => new List<PropertyInfo>() { Mapper.Map<PropertyInfo>(y) }))
                .ForMember(x => x.MVNOCustomerPropertyInfo, opt => opt.MapFrom(y => new List<MVNOCustomerPropertyInfo>() { Mapper.Map<MVNOCustomerPropertyInfo>(y) }));

            #endregion

            #region CustomerInfo -> CustomerDTO Mappings
            Mapper.CreateMap<CustomerInfo, CustomerDataDTO>()
                .ForMember(x => x.Gender, opt => opt.MapFrom(y => y.GenderID.HasValue ? (Genders)y.GenderID.Value : Genders.Male))
                .ForMember(x => x.Title, opt => opt.MapFrom(y => y.TitleID.HasValue ? y.TitleID.Value == 1 ? PersonTitles.Mr : PersonTitles.Miss : PersonTitles.Mr))
                .ForMember(x => x.BirthDay, opt => opt.MapFrom(y => y.DateOfBirth))
                .ForMember(x => x.BankInformation, opt => opt.MapFrom(y => y.BankInfo != null ? y.BankInfo.FirstOrDefault(x => !x.EndDate.HasValue || x.EndDate > DateTime.Now) : null))
                .ForMember(x => x.CustomerAddress, opt => opt.MapFrom(y => y.Addresses.FirstOrDefault(add => add.UsageType == AddressUsages.PersonalAddress).Address))
                .ForMember(x => x.DeliveryAddress, opt => opt.MapFrom(y => y.Addresses.FirstOrDefault(add => add.UsageType == AddressUsages.DeliveryAddress).Address))
                .ForMember(x => x.FiscalAddress, opt => opt.MapFrom(y => y.Addresses.FirstOrDefault(add => add.UsageType == AddressUsages.FiscalAddress).Address))
                .ForMember(x => x.DocumentType, opt => opt.MapFrom(y => (y.PropertyInfo == null ||
                                                                         y.PropertyInfo.Count == 0 ||
                                                                         y.PropertyInfo.First().IDType == null) ?
                                                                         0 : y.PropertyInfo.First().IDType))
                .ForMember(x => x.DocumentNumber, opt => opt.MapFrom(y => (y.PropertyInfo == null ||
                                                                         y.PropertyInfo.Count == 0 ||
                                                                         y.PropertyInfo.First().IDNumber == null) ?
                                                                         String.Empty : y.PropertyInfo.First().IDNumber))
                .ForMember(x => x.PendingStatus, opt => opt.MapFrom(y => y.PropertyInfo == null ||
                                                                         y.PropertyInfo.Count == 0 ?
                                                                         null : (CustomerStatus?)y.PropertyInfo[0].PendingStatus))
                .ForMember(x => x.Nationality, opt => opt.MapFrom(y => y.MVNOCustomerPropertyInfo == null ||
                                                                       y.MVNOCustomerPropertyInfo.Count == 0 ?
                                                                       null : y.MVNOCustomerPropertyInfo.First().Nationality));
            Mapper.CreateMap<CustomerInfo, CustomerDTO>()
                .ForMember(x => x.ParentCustomerId, opt => opt.MapFrom(y => y.ParentID))
                .ForMember(x => x.ExternalCustomerId, opt => opt.MapFrom(y => (y.PropertyInfo != null) ? y.PropertyInfo.First().ExternalId : String.Empty))
                .ForMember(x => x.CustomerData, opt => opt.MapFrom(y => y));
            #endregion
        }
        
        /// <summary>
        /// Method to convert a CustomerInfo Entity into CustomerDTO
        /// </summary>
        /// <param name="customer">the customer in internal model to be converted</param>
        /// <returns>CustomerDto object</returns>
        public CustomerDTO Convert(CustomerInfo customer)
        {
            return Mapper.Map<CustomerDTO>(customer);
        }


        /// <summary>
        /// Convert from CustomerDTO to CustomerInfo
        /// </summary>
        /// <param name="source">Object to be converted</param>
        /// <returns>CustomerInfo object</returns>
        public CustomerInfo Convert(CustomerDTO source)
        {
            //CustomerInfo dest = CoreConversorHelper.CustomerDtoToCore(source);
            CustomerInfo dest = Mapper.Map<CustomerInfo>(source);

            List<CustomerAddress> addList = CustomerDtoToCustomerAddress(source).ToList();
            foreach (var add in addList)
            {
                add.Customer = dest;
            }
            dest.Addresses = addList;

            dest.PropertyInfo.First().CustomerInfo = dest;
            dest.MVNOCustomerPropertyInfo.First().CustomerInfo = dest;
            dest.BankInfo.First().CustomerInfo = dest;

            return dest;
        }

        /// <summary>
        /// Extracts a list of CustomerAddress from a CustomerDTO object
        /// </summary>
        /// <param name="customerDto">CustomerDTO object with all the information</param>
        /// <returns>A list of CustomerAddress with CustomerAddress, FiscalAddress and DeliveryAddess wether corresponds</returns>
        public static IEnumerable<CustomerAddress> CustomerDtoToCustomerAddress(CustomerDTO customerDto)
        {
            List<CustomerAddress> lstAddresses = new List<CustomerAddress>();

            if (customerDto != null && customerDto.CustomerData != null &&
                customerDto.CustomerData.CustomerAddress != null)
            {
                lstAddresses.Add(new CustomerAddress()
                {
                    Address = customerDto.CustomerData.CustomerAddress.ToCore(),
                    UsageType = AddressUsages.PersonalAddress,
                });
            }

            if (customerDto != null && customerDto.CustomerData != null &&
                customerDto.CustomerData.DeliveryAddress != null)
            {
                lstAddresses.Add(new CustomerAddress()
                {
                    Address = customerDto.CustomerData.DeliveryAddress.ToCore(),
                    UsageType = AddressUsages.DeliveryAddress,
                });
            }

            if (customerDto != null && customerDto.CustomerData != null &&
                customerDto.CustomerData.FiscalAddress != null)
            {
                lstAddresses.Add(new CustomerAddress()
                {
                    Address = customerDto.CustomerData.FiscalAddress.ToCore(),
                    UsageType = AddressUsages.FiscalAddress,
                });
            }
            return lstAddresses;
        }
    }
}
