using System;
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
    public class MNPConsolidationLogInfo
    {
        #region 构造函数
        public MNPConsolidationLogInfo()
        { }

        public MNPConsolidationLogInfo(string OPERATORCODE, int USERID, string FILETYPE, string FILENAME, DateTime RELATEDDATE, string TURN, DateTime OPERATEDATE, int STATUS)
        {
            this._OPERATORCODE = OPERATORCODE;
            this._USERID = USERID;
            this._FILETYPE = FILETYPE;
            this._FILENAME = FILENAME;
            this._RELATEDDATE = RELATEDDATE;
            this._TURN = TURN;
            this._OPERATEDATE = OPERATEDATE;
            this._STATUS = STATUS;
        }
        #endregion

        #region 成员
        private string _OPERATORCODE;
        private int? _USERID;
        private string _FILETYPE;
        private string _FILENAME;
        private DateTime? _RELATEDDATE;
        private string _TURN;
        private DateTime? _OPERATEDATE;
        private int? _STATUS;
        #endregion


        #region 属性
        public virtual string OperatorCode
        {
            get { return _OPERATORCODE; }
            set { _OPERATORCODE = value; }
        }

        public virtual int? UserID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }

        public virtual string FileType
        {
            get { return _FILETYPE; }
            set { _FILETYPE = value; }
        }

        public virtual string FileName
        {
            get { return _FILENAME; }
            set { _FILENAME = value; }
        }

        public virtual DateTime? RelateDate
        {
            get { return _RELATEDDATE; }
            set { _RELATEDDATE = value; }
        }

        public virtual string Turn
        {
            get { return _TURN; }
            set { _TURN = value; }
        }

        public virtual DateTime? OperateDate
        {
            get { return _OPERATEDATE; }
            set { _OPERATEDATE = value; }
        }

        public virtual int? Status
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        #endregion

    }
}
