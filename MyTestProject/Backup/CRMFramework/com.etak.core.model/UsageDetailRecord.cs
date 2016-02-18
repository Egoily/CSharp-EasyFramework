using System;

namespace com.etak.core.model
{ 
    public enum UnitIdEnum
    {
        Unknown = -1,
        Money = 1,
        Minute = 2,
        Piece = 3,
        KByte = 4,
        Second = 5,
        Byte = 6,
        MByte = 7,
        GByte = 8
    }

  
    public enum CallDirectionEnum
    {
        Unknown = -1,
        MobileInbound = 3001,
        MobileOutbound = 3000,
        MobileForward = 3002
    }

  
    public enum ServicetypeEnum
    {
        Unknown = -1,
        Cps = 1000,
        Mobile = 3000,
        Wifi = 7000
    }

    
    public enum CallTypeEnum
    {
        Unknown = -1,
        International = 100,
        Mobile = 101,
        National = 102,
        Forwarding = 111,
        Sc18Xy = 112,
        PremiumRateInformation = 114,
        PremiumRateEntertainment = 115,
        PrsMobileCharge = 116,
        OnNet = 120,
        SuperOnNet = 125,
        Voicemail = 130,
        LcnSms = 135,
        SicapDmcSms = 136,
        Roaming = 201,
        CallbackMO = 202,
        CallbackMT = 203,
        RoamingForwarding = 204,
        IntraVpn = 205,
        IntraDealer = 206,
        PremiumSms = 127,
        PremiumSmsMobileCharge = 128,
        SurCharge = 129,
        M2M = 207,
        NonCamel = 211,
        FreeWebAccess = 300,
        SmsShortcode = 301,
        MobilboxRetrieval = 302,
        MobilboxCopsNationalFixedNet = 303,
        MobilboxCopsDirectAccessDTNetwork = 304,
        MobilboxCopsAllNationalNetworks = 305,
        MobilboxCopsInternationalRest = 306,
        MobilboxNotfNational = 307,
        MobilboxNotfDebeitelShortcode = 308,
        MobilboxNotfInternationalZone1And2 = 309,
        MobilboxNotfInternationalRest = 310,
        MobilboxNotfImarsat = 311,
        MobilboxNotfFocZone = 312,
        MobilboxNotfD1DTNetowrk = 313,
        MobilboxNotfConnection = 314,
        MobilboxNotfENetworks = 315,
        MobilboxNotfD1DirectAccess = 316,
        MobilboxNotfIridium = 317,
        MobilboxNotfD2Network = 318
    }

   
    public enum SubservicetypeEnum
    {
        Unknown = -1,
        CpsVoice = 1003,
        MobileVoice = 3001,
        MobileSms = 3002,
        MobileData = 3003,
        MobileMms = 3004,
        MobileVideo = 3005,
        MobileDid = 3006,
        MobileCallback = 3007,
        WifiData = 7001
    }

  
    public enum UnitCategoryEnum
    {
        PerSecond = 10,
        PerMinute = 11
    }

  
    public enum TimeCategoryEnum
    {
        FlatFee = 100
    }

   
    public enum RateTypeEnum
    {
        Aleg = 1,
        Bleg = 2,
        Both = 3
    }
   
    public class UsageDetailRecord 
    {
        #region Enumized

        /// <summary>
        /// 
        /// </summary>
       
        public virtual Int64 Usagedetailid { get; set; }

       
        public virtual String Apn { get; set; }

      
        public virtual String Bnumberaddress { get; set; }

        /// <summary>
        /// 
        /// </summary>	
        public virtual Int32 Servicetypeid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Int32 Subservicetypeid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        
        public virtual String Anumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
       
        public virtual String Bnumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
       
        public virtual String Cnumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime Startdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime Enddate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Decimal Aleg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Decimal Bleg { get; set; }

        #endregion

        #region RatePlans
        /// <summary>
        /// 
        /// </summary>
        public virtual Int32 Unitcategoryid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Int32 Currencyid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Int32 Ratetypeid { get; set; }

        /// <summary>
        /// Rate planed used to rate the call
        /// </summary>
        public virtual Int32 Rateplanid { get; set; }

        /// <summary>
        /// Line of RatePlane used 
        /// </summary>
        public virtual Int32 Rateplandetailid { get; set; }
        #endregion

        /// <summary>
        /// Prefix of rateplan 
        /// </summary>
        public virtual String Countrycode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Int32 Calltypeid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Int32 Calldirectionid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Int32 Timecategoryid { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public virtual String Imsi { get; set; }

        /// <summary>
        /// Amount charged
        /// </summary>
        public virtual Decimal Amount1 { get; set; }

        /// <summary>
        /// STA Amount charged 
        /// </summary>
        public virtual Decimal Amount2 { get; set; }

        /// <summary>
        /// Tariff charged amount for the first tariff applied 
        /// </summary>
        public virtual Decimal Tariff1 { get; set; }

        /// <summary>
        /// Tariff charged amount for the sencond tariff applied 
        /// </summary>
       
        public virtual Decimal Tariff2 { get; set; }

        /// <summary>
        /// Call setup amount
        /// </summary>
       
        public virtual Decimal Setup { get; set; }

        /// <summary>
        /// Call price of the promt
        /// </summary>
       
        public virtual Decimal Prompt { get; set; }

        /// <summary>
        /// </summary>
       
        public virtual int Providerid { get; set; }

        /// <summary>
        /// </summary>
       
        public virtual int Roamingzone1 { get; set; }

        /// <summary>
        /// 
        /// </summary>		
       
        public virtual int Roamingzone2 { get; set; }

        /// <summary>
        /// 
        /// </summary>		
       
        public virtual string Roamingmscnumber { get; set; }

        /// <summary>
        /// 
        /// </summary>		
       
        public virtual int Promotionplanid { get; set; }

        /// <summary>
        /// 
        /// </summary>		
       
        public virtual int Promotionplandetailid { get; set; }

        /// <summary>
        /// 
        /// </summary>		
       
        public virtual string Tsc { get; set; }

        /// <summary>
        /// 
        /// </summary>		
       
        public virtual string Tsp { get; set; }

        /// <summary>
        /// 
        /// </summary>		
       
        public virtual string Gsnipv4 { get; set; }

        /// <summary>
        /// 
        /// </summary>		
       
        public virtual long Promotionid { get; set; }

        /// <summary>
        /// 
        /// </summary>		
       
        public virtual decimal Promotionlimit { get; set; }

        /// <summary>
        /// 
        /// </summary>
       
        public virtual string Imei { get; set; }

        /// <summary>
        /// added by damon,2014-02-17
        /// </summary>
       
        public virtual decimal CreditLimit { get; set; }

    }
}
