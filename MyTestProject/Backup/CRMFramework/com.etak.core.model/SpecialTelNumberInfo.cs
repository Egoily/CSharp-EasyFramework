using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class SpecialTelNumberInfo
    {
        #region 构造函数
        public SpecialTelNumberInfo()
        {

        }

        public SpecialTelNumberInfo(long securityNumberId, int customerId, string phoneNumber, int type, DateTime createDate, string description)
        {
            this.securityNumberId = securityNumberId;
            this.customerId = customerId;
            this.phoneNumber = phoneNumber;
            this.type = type;
            this.createDate = createDate;
            this.description = description;
        }

        #endregion

        #region 成员
        private long securityNumberId;
        private int customerId;
        private string phoneNumber;
        private int type;
        private DateTime createDate;
        private string description;
        #endregion

        #region 属性
        public virtual long SecurityNumberId
        {
            get { return securityNumberId; }
            set { securityNumberId = value; }
        }

        public virtual int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        public virtual string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public virtual int Type
        {
            get { return type; }
            set { type = value; }
        }

        public virtual DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion

    }
}
