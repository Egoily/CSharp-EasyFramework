using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    ///功能描述    :    
    ///开发者      :    
    ///建立时间    :    2009/9/15 AM 10:54:21
    ///修订描述    :    
    ///进度描述    :    
    ///版本号      :    1.0
    ///最后修改时间:    2009/9/15 AM 10:54:21
    ///
    ///Function Description :    
    ///Developer                :    
    ///Builded Date:    2009/9/15 AM 10:54:21
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number        :    1.0
    ///Last Modify Date     :    2009/9/15 AM 10:54:21
    /// </summary>
    [DataContract]
    [Serializable]
    public class MNPIncomingEffectInfo
    {
        #region 构造函数
        public MNPIncomingEffectInfo()
        { }

        public MNPIncomingEffectInfo(int CUSTOMERID, string TEMPNO, string PORTEDNO, DateTime EFFECTDATE, int STATUS, int INCOMINGTYPE, DateTime CREATEDATE,int seqID)
        {
            this._CUSTOMERID = CUSTOMERID;
            this._TEMPNO = TEMPNO;
            this._PORTEDNO = PORTEDNO;
            this._EFFECTDATE = EFFECTDATE;
            this._STATUS = STATUS;
            this._INCOMINGTYPE = INCOMINGTYPE;
            this._CREATEDATE = CREATEDATE;
            this.seqID = seqID;
        }
        #endregion

        #region 成员
        private long seqID;
        private int? _CUSTOMERID;
        private string _TEMPNO;
        private string _PORTEDNO;
        private DateTime? _EFFECTDATE;
        private int? _STATUS;
        private int? _INCOMINGTYPE;
        private DateTime? _CREATEDATE;
        #endregion


        #region 属性
        public virtual int? CustomerID
        {
            get { return _CUSTOMERID; }
            set { _CUSTOMERID = value; }
        }
        /// <summary>
        /// Temprary Number
        /// </summary>
        public virtual string TempNO
        {
            get { return _TEMPNO; }
            set { _TEMPNO = value; }
        }
        /// <summary>
        /// Ported No
        /// </summary>
        public virtual string PortedNO
        {
            get { return _PORTEDNO; }
            set { _PORTEDNO = value; }
        }

        public virtual DateTime? EffectDate
        {
            get { return _EFFECTDATE; }
            set { _EFFECTDATE = value; }
        }

        public virtual int? Status
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public virtual int? IncomingType
        {
            get { return _INCOMINGTYPE; }
            set { _INCOMINGTYPE = value; }
        }

        public virtual DateTime? CreateDate
        {
            get { return _CREATEDATE; }
            set { _CREATEDATE = value; }
        }

        public virtual long SEQID
        {
            get { return seqID; }
            set { seqID = value; }
        }
        #endregion

    }
}
