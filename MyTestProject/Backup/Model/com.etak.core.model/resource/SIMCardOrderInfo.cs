using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class SIMCardOrderInfo
    {
        private int? _OrderID = null;        
        private string _Batch;        
        private int? _DealerID = null;        
        private DateTime? _OrderDate = null;        
        private int? _Quantity = null;        
        private string _Company;        
        private string _Address;        
        private string _City;        
        private string _Country;        
        private int? _Status = null;        
        private int? _ActivateType = null;
        


        #region Attribute
        public virtual int? OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        public virtual string Batch
        {
            get { return _Batch; }
            set { _Batch = value; }
        }

        public virtual int? DealerID
        {
            get { return _DealerID; }
            set { _DealerID = value; }
        }

        public virtual DateTime? OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }

        public virtual int? Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public virtual string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }

        public virtual string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public virtual string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public virtual string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        public virtual int? Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public virtual int? ActivateType
        {
            get { return _ActivateType; }
            set { _ActivateType = value; }
        }
        #endregion
    }
}
