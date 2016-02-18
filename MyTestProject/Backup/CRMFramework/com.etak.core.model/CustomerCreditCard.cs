using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// CRM_CUSTOMERS_CREDITCARD:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [DataContract]
    [Serializable]
    public class CustomerCreditCard
    {
      
        #region Model
        private int _customerid;
        private string _cardNumber;
        private DateTime _expirationdate;
        private string _entity;
        private string _nameOnCard;
        private int _status;
       
        /// <summary>
        /// 
        /// </summary>
        public int CUSTOMERID
        {
            set { _customerid = value; }
            get { return _customerid; }
        }

        public string CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }

        public DateTime Expirationdate
        {
            get { return _expirationdate; }
            set { _expirationdate = value; }
        }

        public string Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        public string NameOnCard
        {
            get { return _nameOnCard; }
            set { _nameOnCard = value; }
        }
        
        public int STATUS
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model


        #region FK object
        private CustomerInfo _CustomerInfo;
        public virtual CustomerInfo CustomerInfo
        {
            get { return _CustomerInfo; }
            set { _CustomerInfo = value; }
        }
        #endregion

        /// <summary>
        /// Override Equals method needed by Nhibernate to map the entity 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var other = obj as CustomerCreditCard;
            if (other == null) return false;

            if (other.CardNumber == CardNumber && 
                    (other.CustomerInfo == CustomerInfo || 
                     other.CustomerInfo.CustomerID == CustomerInfo.CustomerID))
                return true;

            return false;

        }

        /// <summary>
        /// Override GetHashCode method needed by Nhibernate to map the entity
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = CardNumber.GetHashCode();
                result = 29 * result + CustomerInfo.GetHashCode();

                return result;
            }
        }
    }
}
