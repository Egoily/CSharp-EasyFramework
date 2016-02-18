using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2009-12-9 11:28:36
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2009-12-9 11:28:36
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2009-12-9 11:28:36
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2009-12-9 11:28:36
   /// </summary>
   [DataContract]
    [Serializable]
    public class MappingInfo
    {
      

        
        #region 成员
        //private int? _CUSTOMERID;
        private int? _MAPPINGID;
        private int? _FISCALUNITID;
        private int? _RESELLERID;
        private int? _AGENTID;
        private int? _SUBAGENTID;
        private int? _ORGID;
        private int? _OLDID;
        private bool _STAT1;
        private bool _STAT2;
        private bool _STAT3;
        private bool _STAT4;
        private bool _STAT5;
        private bool _STAT6;
        private bool _STAT7;
        private CustomerInfo _CustomerInfo;
        #endregion


        #region FK object
        public virtual CustomerInfo CustomerInfo
        {
            get { return _CustomerInfo; }
            set { _CustomerInfo = value; }
        }
        #endregion


        #region 属性
        public virtual int? MappingId
        {
            get { return _MAPPINGID; }
            set { _MAPPINGID = value; }
        }

        public virtual int? FiscalUnitId
        {
            get { return _FISCALUNITID; }
            set { _FISCALUNITID = value; }
        }

        public virtual int? ReSellerId
        {
            get { return _RESELLERID; }
            set { _RESELLERID = value; }
        }

        public virtual int? AgentId
        {
            get { return _AGENTID; }
            set { _AGENTID = value; }
        }

        public virtual int? SubAgentId
        {
            get { return _SUBAGENTID; }
            set { _SUBAGENTID = value; }
        }

        public virtual int? OrgId
        {
            get { return _ORGID; }
            set { _ORGID = value; }
        }

        public virtual int? OldId
        {
            get { return _OLDID; }
            set { _OLDID = value; }
        }

        public virtual bool Stat1
        {
            get { return _STAT1; }
            set { _STAT1 = value; }
        }

        public virtual bool Stat2
        {
            get { return _STAT2; }
            set { _STAT2 = value; }
        }

        public virtual bool Stat3
        {
            get { return _STAT3; }
            set { _STAT3 = value; }
        }

        public virtual bool Stat4
        {
            get { return _STAT4; }
            set { _STAT4 = value; }
        }

        public virtual bool Stat5
        {
            get { return _STAT5; }
            set { _STAT5 = value; }
        }

        public virtual bool Stat6
        {
            get { return _STAT6; }
            set { _STAT6 = value; }
        }

        public virtual bool Stat7
        {
            get { return _STAT7; }
            set { _STAT7 = value; }
        }

        #endregion

        public virtual MappingInfo Clone()
        {
            MappingInfo mappingInfo = this.MemberwiseClone() as MappingInfo;


            return mappingInfo;
        }
    }
}
