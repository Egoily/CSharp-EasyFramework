using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2011-4-16 13:16:43
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2011-4-16 13:16:43
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2011-4-16 13:16:43
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2011-4-16 13:16:43
   /// </summary>
    [DataContract]
    [Serializable]
    public class RmblackwhitelistInfo
    {
        #region 构造函数
        public RmblackwhitelistInfo()
        { }

        public RmblackwhitelistInfo(int LISTID, string LISTNAME, int LISTTYPEID, string REMARK)
        {
            this._LISTID = LISTID;
            this._LISTNAME = LISTNAME;
            this._LISTTYPEID = LISTTYPEID;
            this._REMARK = REMARK;
        }
        #endregion

        #region 成员
        private int _LISTID;
        private string _LISTNAME;
        private int _LISTTYPEID;
        private string _REMARK;
        #endregion


        #region 属性
        public virtual int ListId
        {
            get { return _LISTID; }
            set { _LISTID = value; }
        }

        public virtual string ListName
        {
            get { return _LISTNAME; }
            set { _LISTNAME = value; }
        }

        public virtual int ListTypeId
        {
            get { return _LISTTYPEID; }
            set { _LISTTYPEID = value; }
        }

        public virtual string Remark
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        #endregion

    }
}
