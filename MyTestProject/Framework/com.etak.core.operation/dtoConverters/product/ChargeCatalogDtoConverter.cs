using System.Linq;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract;

namespace com.etak.core.operation.dtoConverters.product
{
    /// <summary>
    /// Converter for ChargeCatalog DTO.
    /// </summary>
    public class ChargeCatalogDtoConverter : ITypeConverter<Charge, ChargeCatalogDTO>
    {
        /// <summary>
        /// Convert from Charge to ChargeCatalogDTO
        /// </summary>
        /// <param name="charge"></param>
        /// <returns></returns>
        public ChargeCatalogDTO Convert(Charge charge)
        {
            #region Convert Process
            ChargeCatalogDTO ChargeDTO = new ChargeCatalogDTO()
                {
                    Id = charge.Id.ToString(),
                    Name = (from e in charge.Name.Texts
                            select new TextualDescription()
                            {
                                LanguageCode = e.Language,
                                Text = e.Text,
                            }).ToList(),
                    Category = charge.Category.ToString(),
                    CreateDate = charge.CreateTime,
                    Description = (from e in charge.Description.Texts
                                   select new TextualDescription()
                                   {
                                       LanguageCode = e.Language,
                                       Text = e.Text,
                                   }).ToList(),
                    Prices = (from e in charge.Prices
                              select new ChargePriceCatalogDTO()
                              {
                                  Amount = e.Amount,
                                  EndDate = e.EndDate.Value,
                                  Currency = e.Currency,
                                  StartDate = e.StartDate,
                              }).ToList(),
                    ProratingInformation = new ProratingSchemaDTO()
                    {
                        Quantity = charge.ProrateQty.Value,
                        Unit = charge.ProrateUnit.Value,
                    },
                    TimeOfCharge = charge.TypeOfTimeOfCharge,
                }; 
            #endregion

            return ChargeDTO;
        }
    }
}
