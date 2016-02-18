using System;

namespace com.etak.core.model.provisioning
{
    /// <summary>
    ///Function Description :    
    ///Developer: Oliver    
    ///Builded Date:   2014-10-23 17:59:55
    ///Revision Description :    
    ///Progress Description :    
    ///Version Number:    1.0
    /// </summary>
    [Serializable]
    public class HLRRequestErrors : ModelBase
    {
        #region 构造函数
        public HLRRequestErrors()
        { }

        public HLRRequestErrors(string OBJECTNAME, int SEQID, string ERRORCODE, int REPEAT, DateTime? CREATETIME)
        {
            this._OBJECTNAME = OBJECTNAME;
            this._SEQID = SEQID;
            this._ERRORCODE = ERRORCODE;
            this._REPEAT = REPEAT;
            this._CREATETIME = CREATETIME;
        }

        #endregion

        #region 成员
        private string _OBJECTNAME;
        private long _SEQID;
        private string _ERRORCODE = "";
        private int _REPEAT;
        private DateTime? _CREATETIME;
        long _QueueId;
        string _ResponseMessage;
        string _RequestMessage;
        #endregion

        public override object GetKey()
        {
            return SEQID;
        }
        #region 属性
        public virtual long SEQID
        {
            get { return _SEQID; }
            set { _SEQID = value; }
        }

        public virtual string ResponseMessage
        {
            get { return _ResponseMessage; }
            set { _ResponseMessage = value; }
        }
        public virtual string RequestMessage
        {
            get { return _RequestMessage; }
            set { _RequestMessage = value; }
        }

        public virtual string OBJECTNAME
        {
            get { return _OBJECTNAME; }
            set { _OBJECTNAME = value; }
        }

        public virtual long QueueId
        {
            get { return _QueueId; }
            set { _QueueId = value; }
        }



        public virtual string ERRORCODE
        {
            get { return _ERRORCODE; }
            set { _ERRORCODE = value; }
        }

        public virtual int REPEAT
        {
            get { return _REPEAT; }
            set { _REPEAT = value; }
        }

        public virtual DateTime? CREATETIME
        {
            get { return _CREATETIME; }
            set { _CREATETIME = value; }
        }

        #endregion

    }
}
