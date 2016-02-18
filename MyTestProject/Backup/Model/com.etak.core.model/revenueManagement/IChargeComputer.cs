using System;

namespace com.etak.core.model.revenueManagement
{
    public interface IChargeComputer
    {
        Decimal ComputePrice(Charge charge, DateTime chargingDate, ProductChargeOption purchaseOption, String msisdn);
    }
}
