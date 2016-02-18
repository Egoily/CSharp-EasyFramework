using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.subscription.catalog;
using com.etak.core.operation.dtoConverters;
using NUnit.Framework;

namespace com.etak.core.operation.UnitTests.dtoConverter
{
    [TestFixture]
    public class PurchaseProductOfferingSelectionConverterUnitTests
    {
        private static PurchaseProductOfferingSelectionDTO dto;

        [TestFixtureSetUp]
        public static void Initialize()
        {
            dto = new PurchaseProductOfferingSelectionDTO()
            {
                ProductOfferingId = 100,
                ProductOfferingChargeOptionId = 100,
                Options = new List<PurchaseProductOfferingSelectionDTO>()
                {
                    new PurchaseProductOfferingSelectionDTO()
                    {
                        ProductOfferingId = 2000,
                        ProductOfferingChargeOptionId = 2000,
                        Options = new List<PurchaseProductOfferingSelectionDTO>()
                        {
                            new PurchaseProductOfferingSelectionDTO()
                            {
                                ProductOfferingId = 30000,
                                ProductOfferingChargeOptionId = 30000,
                                Options = null,
                            }
                        }
                    },
                    new PurchaseProductOfferingSelectionDTO()
                    {
                        ProductOfferingId = 3000,
                        ProductOfferingChargeOptionId = 3000,
                        Options = new List<PurchaseProductOfferingSelectionDTO>()
                        {
                            
                        }
                    },

                }
            };
        }

        [Test()]
        public static void ToCore_PurchaseProductOfferingSelection()
        {
            var core = dto.ToCore();

            Assert.AreEqual(core.PurchasedProductOffering.Id, 100);
        }
    }
}
