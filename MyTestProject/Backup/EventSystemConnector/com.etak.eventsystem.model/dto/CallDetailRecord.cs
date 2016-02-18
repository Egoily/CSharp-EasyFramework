using System;
using System.Runtime.Serialization;

namespace com.etak.eventsystem.model.dto
{

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]    
    public enum UnitIdEnum
    {
        [EnumMember] Unknown = -1,
        [EnumMember] Money = 1,
        [EnumMember] Minute = 2,
        [EnumMember] Piece = 3,
        [EnumMember] KByte = 4,
        [EnumMember] Second = 5,
        [EnumMember] Byte = 6,
        [EnumMember] MByte = 7,
        [EnumMember] GByte = 8
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]    
    public enum CallDirectionEnum
    {
        [EnumMember] Unknown = -1,
        [EnumMember] MobileInbound = 3001,
        [EnumMember] MobileOutbound = 3000,
        [EnumMember] MobileForward = 3002
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum ServicetypeEnum
    {
        [EnumMember] Unknown = -1,
        [EnumMember] Cps = 1000,
        [EnumMember] Mobile = 3000,
        [EnumMember] Wifi = 7000
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum CallTypeEnum
    {
        [EnumMember] Unknown = -1,
        [EnumMember] International = 100,
        [EnumMember] Mobile = 101,
        [EnumMember] National = 102,
        [EnumMember] Forwarding = 111,
        [EnumMember] Sc18Xy = 112,
        [EnumMember] PremiumRateInformation = 114,
        [EnumMember] PremiumRateEntertainment = 115,
        [EnumMember] PrsMobileCharge = 116,
        [EnumMember] OnNet = 120,
        [EnumMember] SuperOnNet = 125,
        [EnumMember] Voicemail = 130,
        [EnumMember] LcnSms = 135,
        [EnumMember] SicapDmcSms = 136,
        [EnumMember] Roaming = 201,
        [EnumMember] CallbackMO = 202,
        [EnumMember] CallbackMT = 203,
        [EnumMember] RoamingForwarding = 204,
        [EnumMember] IntraVpn = 205,
        [EnumMember] IntraDealer = 206,
        [EnumMember] PremiumSms = 127,
        [EnumMember] PremiumSmsMobileCharge = 128,
        [EnumMember] SurCharge = 129,
        [EnumMember] M2M = 207,
        [EnumMember] NonCamel = 211,
        [EnumMember] FreeWebAccess = 300,
        [EnumMember] SmsShortcode = 301,
        [EnumMember] MobilboxRetrieval = 302,
        [EnumMember] MobilboxCopsNationalFixedNet = 303,
        [EnumMember] MobilboxCopsDirectAccessDTNetwork = 304,
        [EnumMember] MobilboxCopsAllNationalNetworks = 305,
        [EnumMember] MobilboxCopsInternationalRest = 306,
        [EnumMember] MobilboxNotfNational = 307,
        [EnumMember] MobilboxNotfDebeitelShortcode = 308,
        [EnumMember] MobilboxNotfInternationalZone1And2 = 309,
        [EnumMember] MobilboxNotfInternationalRest = 310,
        [EnumMember] MobilboxNotfImarsat = 311,
        [EnumMember] MobilboxNotfFocZone = 312,
        [EnumMember] MobilboxNotfD1DTNetowrk = 313,
        [EnumMember] MobilboxNotfConnection = 314,
        [EnumMember] MobilboxNotfENetworks = 315,
        [EnumMember] MobilboxNotfD1DirectAccess = 316,
        [EnumMember] MobilboxNotfIridium = 317,
        [EnumMember] MobilboxNotfD2Network = 318
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum SubservicetypeEnum
    {
        [EnumMember] Unknown = -1,
        [EnumMember] CpsVoice = 1003,
        [EnumMember] MobileVoice = 3001,
        [EnumMember] MobileSms = 3002,
        [EnumMember] MobileData = 3003,
        [EnumMember] MobileMms = 3004,
        [EnumMember] MobileVideo = 3005,
        [EnumMember] MobileDid = 3006,
        [EnumMember] MobileCallback = 3007,
        [EnumMember] WifiData = 7001
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum UnitCategoryEnum
    {
        [EnumMember] PerSecond = 10,
        [EnumMember] PerMinute = 11
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum TimeCategoryEnum
    {
        [EnumMember] FlatFee = 100
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public enum RateTypeEnum
    {
        [EnumMember] Aleg = 1,
        [EnumMember] Bleg = 2,
        [EnumMember] Both = 3
    }

    [DataContract(Namespace = Definitions.EV_SYSTEM_NAMESPACE)]
    public class CallDetailRecord : LoadeableEntity
    {
        #region Enumized 

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual Int64 Usagedetailid { get; set; }        

        [DataMember] public virtual String Apn { get; set; }

        [DataMember] public virtual String Bnumberaddress { get; set; }

        /// <summary>
        /// 
        /// </summary>	
        [DataMember] public virtual Int32 Servicetypeid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual Int32 Subservicetypeid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual String Anumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual String Bnumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual String Cnumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual DateTime Startdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual DateTime Enddate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual Decimal Aleg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual Decimal Bleg { get; set; }

        #endregion

        #region RatePlans
        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual Int32 Unitcategoryid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual Int32 Currencyid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual Int32 Ratetypeid { get; set; }
        
        /// <summary>
        /// Rate planed used to rate the call
        /// </summary>
        [DataMember] public virtual Int32 Rateplanid { get; set; }

        /// <summary>
        /// Line of RatePlane used 
        /// </summary>
        [DataMember]public virtual Int32 Rateplandetailid { get; set; }
        #endregion

        /// <summary>
        /// Prefix of rateplan 
        /// </summary>
        [DataMember] public virtual String Countrycode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual Int32 Calltypeid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual Int32 Calldirectionid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual Int32 Timecategoryid { get; set; }

        

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public virtual String Imsi { get; set; }

        /// <summary>
        /// Amount charged
        /// </summary>
        [DataMember] public virtual Decimal Amount1 { get; set; }

        /// <summary>
        /// STA Amount charged 
        /// </summary>
        [DataMember] public virtual Decimal Amount2 { get; set; }

        /// <summary>
        /// Tariff charged amount for the first tariff applied 
        /// </summary>
        [DataMember] public virtual Decimal Tariff1 { get; set; }

        /// <summary>
        /// Tariff charged amount for the sencond tariff applied 
        /// </summary>
        [DataMember] public virtual Decimal Tariff2 { get; set; }

        /// <summary>
        /// Call setup amount
        /// </summary>
        [DataMember]
        public virtual Decimal Setup { get; set; }

        /// <summary>
        /// Call price of the promt
        /// </summary>
        [DataMember]
        public virtual Decimal Prompt { get; set; }

        /// <summary>
        /// </summary>
        [DataMember]
        public virtual int Providerid { get; set; }

        /// <summary>
        /// </summary>
        [DataMember]
        public virtual int Roamingzone1 { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        [DataMember]
        public virtual int Roamingzone2 { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        [DataMember]
        public virtual string Roamingmscnumber { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        [DataMember]
        public virtual int Promotionplanid { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        [DataMember]
        public virtual int Promotionplandetailid { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        [DataMember]
        public virtual string Tsc { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        [DataMember]
        public virtual string Tsp { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        [DataMember]
        public virtual string Gsnipv4 { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        [DataMember]
        public virtual long Promotionid { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        [DataMember]
        public virtual decimal Promotionlimit { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Imei { get; set; }

        /// <summary>
        /// added by damon,2014-02-17
        /// </summary>
        [DataMember]
        public virtual decimal CreditLimit { get; set; }        

    }
}
