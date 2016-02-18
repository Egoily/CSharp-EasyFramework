using com.etak.core.model;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.usage
{
    class UsageDetailRecordMap : ClassMap<UsageDetailRecord>
    {
        public UsageDetailRecordMap ()
        {
            Schema("dbo");
            Table("USAGE_DETAIL");
            ReadOnly();

            Id(x => x.Usagedetailid, "usage_detail_id").GeneratedBy.Assigned();
            Map(x => x.Imei, "IMEI");
            Map(x => x.Promotionlimit, "PROMOTIONLIMIT");
            Map(x => x.Promotionid, "PROMOTIONID");
            Map(x => x.Prompt, "Prompt");
            Map(x => x.Promotionplandetailid, "PROMOTIONPLANDETAILID");
            Map(x => x.Promotionplanid, "PROMOTIONPLANID");
            Map(x => x.Gsnipv4, "GSNIPV4");
            Map(x => x.Tsc, "TSC");
            Map(x => x.Tsp, "TSP");
            Map(x => x.Bnumberaddress, "BNUMBERADDRESS");
            //Map(x => x.TaxTypeId, "TAXTYPEID");
            Map(x => x.Roamingmscnumber, "ROAMINGMSCNUMBER");
            Map(x => x.Roamingzone2, "ROAMINGZONE2");
            Map(x => x.Roamingzone1, "ROAMINGZONE1");
            //Map(x => x.BundleType, "BUNDLETYPE");
            //Map(x => x.CityId, "CITYID");
           // Map(x => x.ProvinceId, "PROVINCEID");
           // Map(x => x.StatusId, "STATUSID");
            //Map(x => x.TariffCode, "TARIFFCODE");
            Map(x => x.Providerid, "PROVIDERID");
            Map(x => x.Setup, "SETUP");
            Map(x => x.Tariff2, "TARIFF2");
            Map(x => x.Tariff1, "TARIFF1");
            //Map(x => x.CellId, "CELLID");
            Map(x => x.Apn, "APN");
            //Map(x => x.CauseCode, "CAUSECODE");
            //Map(x => x.Cause, "CAUSE");
            //Map(x => x.Oli, "OLI");
            //Map(x => x.Ili, "ILI");
            Map(x => x.Calltypeid, "CALLTYPEID");
            Map(x => x.Calldirectionid, "CALLDIRECTIONID");
            Map(x => x.Timecategoryid, "TIMECATEGORYID");
            Map(x => x.Unitcategoryid, "UNITCATEGORYID");
            Map(x => x.Currencyid, "CURRENCYID");
            Map(x => x.Ratetypeid, "RATETYPEID");
            Map(x => x.Imsi, "IMSI");
            //Map(x => x.MopAddress, "MOPADDRESS");
            //Map(x => x.Charge, "CHARGE");
            Map(x => x.Amount1, "AMOUNT1");
            Map(x => x.Amount2, "AMOUNT2");
            //Map(x => x.CPC, "CPC");
            //Map(x => x.Calls, "CALLS");
            Map(x => x.Aleg, "ALEG");
            Map(x => x.Bleg, "BLEG");
            //Map(x => x.PortIn, "PORTIN");
            //Map(x => x.PortOut1, "PORTOUT1");
            //Map(x => x.PortOut2, "PORTOUT2");
            //Map(x => x.RoutingCode1, "ROUTINGCODE1");
            //Map(x => x.RoutingCode2, "ROUTINGCODE2");
            //Map(x => x.SwitchId, "SWITCHID");
            Map(x => x.Rateplanid, "RATEPLANID");
            Map(x => x.Rateplandetailid, "RATEPLANDETAILID");
            Map(x => x.Countrycode, "COUNTRYCODE");
            //Map(x => x.CallId, "CALLID");
            //Map(x => x.CustomerId, "CUSTOMERID");
            Map(x => x.Servicetypeid, "SERVICETYPEID");
            Map(x => x.Subservicetypeid, "SUBSERVICETYPEID");
            Map(x => x.Anumber, "ANUMBER");
            Map(x => x.Bnumber, "BNUMBER");
            Map(x => x.Cnumber, "CNUMBER");
            Map(x => x.Startdate, "STARTDATE");
            Map(x => x.Enddate, "ENDDATE");
        }
    }
}
