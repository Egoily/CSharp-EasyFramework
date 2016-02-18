using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009/9/15 AM 10:54:20
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009/9/15 AM 10:54:20
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009/9/15 AM 10:54:20
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009/9/15 AM 10:54:20
    /// </summary>
    [DataContract]
    [Serializable]
    public class MNPPortabilityInfo
    {
        #region 构造函数
        public MNPPortabilityInfo()
        { }

        public MNPPortabilityInfo(string REFERENCECODE, string TYPE, string IDENTIFICATIONTYPE, string IDENTIFICATION, string FIRSTNAME, string LASTNAME1, string LASTNAME2, string COMPANYNAME, int COUNTRYCODE, string PTYPE, string DONATIONOPERATOR, string RECEPTIONOPERATOR, string SRFC, string RECEIVERNRN, DateTime REQUESTDATE, DateTime RCW, DateTime EFFECTDATE, string MSISDN, string ICCID, string STATUS, int USERID, int PORTABILITYDIRECTION, int DEALERID, string REALREFCODE)
        {
            this._REFERENCECODE = REFERENCECODE;
            this._TYPE = TYPE;
            this._IDENTIFICATIONTYPE = IDENTIFICATIONTYPE;
            this._IDENTIFICATION = IDENTIFICATION;
            this._FIRSTNAME = FIRSTNAME;
            this._LASTNAME1 = LASTNAME1;
            this._LASTNAME2 = LASTNAME2;
            this._COMPANYNAME = COMPANYNAME;
            this._COUNTRYCODE = COUNTRYCODE;
            this._PTYPE = PTYPE;
            this._DONATIONOPERATOR = DONATIONOPERATOR;
            this._RECEPTIONOPERATOR = RECEPTIONOPERATOR;
            this._SRFC = SRFC;
            this._RECEIVERNRN = RECEIVERNRN;
            this._REQUESTDATE = REQUESTDATE;
            this._RCW = RCW;
            this._EFFECTDATE = EFFECTDATE;
            this._MSISDN = MSISDN;
            this._ICCID = ICCID;
            this._STATUS = STATUS;
            this._USERID = USERID;
            this._PORTABILITYDIRECTION = PORTABILITYDIRECTION;
            this._DEALERID = DEALERID;
            this._REALREFCODE = REALREFCODE;
        }
        #endregion

        #region 成员
        private string _REFERENCECODE;
        private string _TYPE;
        private string _IDENTIFICATIONTYPE;
        private string _IDENTIFICATION;
        private string _FIRSTNAME;
        private string _LASTNAME1;
        private string _LASTNAME2;
        private string _COMPANYNAME;
        private int? _COUNTRYCODE;
        private string _PTYPE;
        private string _DONATIONOPERATOR;
        private string _RECEPTIONOPERATOR;
        private string _SRFC;
        private string _RECEIVERNRN;
        private DateTime? _REQUESTDATE;
        private DateTime? _RCW;
        private DateTime? _EFFECTDATE;
        private string _MSISDN;
        private string _ICCID;
        private string _STATUS;
        private int? _USERID;
        private int? _PORTABILITYDIRECTION;
        private int? _DEALERID;
        private string _REALREFCODE;
        private string reasonCode;
        private DateTime? createDate;
        private DateTime? deleteDate;
        private DateTime? changeStatusDate;
        private DateTime? readDate;
        private string cancellationCode;
        //modified by richard 20120607 for hits
        private int? convertCountryCode;

        #region for multi port in
        private IList<ICCIDRelativeMSISDN> iccidRelativeMsisdn = new List<ICCIDRelativeMSISDN>();
        private IList<MSISDNRange> msisdnRange = new List<MSISDNRange>();
        #endregion

        #endregion

        #region 属性
        public virtual string ReferenceCode
        {
            get { return _REFERENCECODE; }
            set { _REFERENCECODE = value; }
        }

        public virtual string Type
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }

        public virtual string IdentificationType
        {
            get { return _IDENTIFICATIONTYPE; }
            set { _IDENTIFICATIONTYPE = value; }
        }

        public virtual string Identification
        {
            get { return _IDENTIFICATION; }
            set { _IDENTIFICATION = value; }
        }

        public virtual string FirstName
        {
            get { return _FIRSTNAME; }
            set { _FIRSTNAME = value; }
        }

        public virtual string LastName1
        {
            get { return _LASTNAME1; }
            set { _LASTNAME1 = value; }
        }

        public virtual string LastName2
        {
            get { return _LASTNAME2; }
            set { _LASTNAME2 = value; }
        }

        public virtual string CompanyName
        {
            get { return _COMPANYNAME; }
            set { _COMPANYNAME = value; }
        }

        public virtual int? CountryCode
        {
            get { return _COUNTRYCODE; }
            set { _COUNTRYCODE = value; }
        }

        public virtual string PType
        {
            get { return _PTYPE; }
            set { _PTYPE = value; }
        }

        public virtual string DonationOperator
        {
            get { return _DONATIONOPERATOR; }
            set { _DONATIONOPERATOR = value; }
        }

        public virtual string ReceptionOperator
        {
            get { return _RECEPTIONOPERATOR; }
            set { _RECEPTIONOPERATOR = value; }
        }

        public virtual string SRFC
        {
            get { return _SRFC; }
            set { _SRFC = value; }
        }

        public virtual string ReceiverNRN
        {
            get { return _RECEIVERNRN; }
            set { _RECEIVERNRN = value; }
        }

        public virtual DateTime? RequestDate
        {
            get { return _REQUESTDATE; }
            set { _REQUESTDATE = value; }
        }

        public virtual DateTime? RCW
        {
            get { return _RCW; }
            set { _RCW = value; }
        }

        public virtual DateTime? EffectDate
        {
            get { return _EFFECTDATE; }
            set { _EFFECTDATE = value; }
        }

        public virtual string Msisdn
        {
            get { return _MSISDN; }
            set { _MSISDN = value; }
        }

        public virtual string ICCID
        {
            get { return _ICCID; }
            set { _ICCID = value; }
        }

        public virtual string Status
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public virtual int? UserID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }

        /// <summary>
        /// Portability Diretion
        /// </summary>
        public virtual int? PortabilityDirection
        {
            get { return _PORTABILITYDIRECTION; }
            set { _PORTABILITYDIRECTION = value; }
        }
        /// <summary>
        /// Delaer Id
        /// </summary>
        public virtual int? DealerID
        {
            get { return _DEALERID; }
            set { _DEALERID = value; }
        }

        public virtual string RealRefCode
        {
            get { return _REALREFCODE; }
            set { _REALREFCODE = value; }
        }
        /// <summary>
        /// Reason Code
        /// </summary>
        public virtual string ReasonCode
        {
            get { return reasonCode; }
            set { reasonCode = value; }
        }
        /// <summary>
        /// Create Date
        /// </summary>
        public virtual DateTime? CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        /// <summary>
        /// Date of deletion
        /// </summary>
        public virtual DateTime? DeleteDate
        {
            get { return deleteDate; }
            set { deleteDate = value; }
        }
        /// <summary>
        /// Date of status changed
        /// </summary>
        public virtual DateTime? ChangeStatusDate
        {
            get { return changeStatusDate; }
            set { changeStatusDate = value; }
        }
        /// <summary>
        /// Read Date
        /// </summary>
        public virtual DateTime? ReadDate
        {
            get { return readDate; }
            set { readDate = value; }
        }
        /// <summary>
        /// Cancelation Code
        /// </summary>
        public virtual string CancellationCode
        {
            get { return cancellationCode; }
            set { cancellationCode = value; }
        }
        /// <summary>
        /// Row Number
        /// </summary>
        public virtual long? RowNumber
        {
            get;
            set;
        }

        //modified by richard 20120607 for hits
        public virtual int? ConvertCountryCode
        {
            get { return convertCountryCode; }
            set { convertCountryCode = value; }
        }

        #region for multi port in
        /// <summary>
        /// ICCID and MSISDN Relationship of multi portability
        /// </summary>
        public virtual IList<ICCIDRelativeMSISDN> ICCIDRELATIVEMSISDN
        {
            get { return iccidRelativeMsisdn; }
            set { iccidRelativeMsisdn = value; }
        }
        /// <summary>
        /// MSISDN Range to portability
        /// </summary>
        public virtual IList<MSISDNRange> MSISDNRANGE
        {
            get { return msisdnRange; }
            set { msisdnRange = value; }
        }
        #endregion
        /// <summary>
        /// temprary storage of ProductPurchased information for off-line portability
        /// </summary>
        public virtual string ProductPurchasedId { get; set; }
        /// <summary>
        /// ResourceId related to portability.
        /// </summary>
        public virtual int? ResourceId { get; set; }


        #endregion
    }

    #region for multi port in
    [DataContract]
    [Serializable]
    public class ICCIDRelativeMSISDN
    {
        private string mSISDN;
        private string iCCID;

        public string MSISDN
        {
            get { return mSISDN; }
            set { mSISDN = value; }
        }

        public string ICCID
        {
            get { return iCCID; }
            set { iCCID = value; }
        }
    }

    [DataContract]
    [Serializable]
    public class MSISDNRange
    {
        private string startRange;
        private string endRange;

        public string StartRange
        {
            get { return startRange; }
            set { startRange = value; }
        }

        public string EndRange
        {
            get { return endRange; }
            set { endRange = value; }
        }
    }

    #endregion
}
