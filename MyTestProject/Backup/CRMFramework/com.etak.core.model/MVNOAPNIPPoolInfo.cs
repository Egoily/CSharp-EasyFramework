using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MVNOAPNIPPoolInfo
    {
        private int? _ID;
        private int? _BatchId;
        private string _APN;
        private string _IPAddress;
        private int? _Status;
        private string _MSISDN;
        private int? _UserId;
        private DateTime? _LastModify;
        private int? _count;
        private bool _isAdd = false;
        private bool _isDelete = false;

        public bool IsAdd
        {
            set { this._isAdd = value; }
            get { return this._isAdd; }
        }

        public bool IsDelete
        {
            set { this._isDelete = value; }
            get { return this._isDelete; }
        }

        public int? ID
        {
            set { this._ID = value; }
            get { return this._ID; }
        }

        public int? BatchId
        {
            set { this._BatchId = value; }
            get { return this._BatchId; }
        }

        public string APN
        {
            set { this._APN = value; }
            get { return this._APN; }
        }

        public string IPAddress
        {
            set { this._IPAddress = value; }
            get { return this._IPAddress; }
        }

        public int? Status
        {
            set { this._Status = value; }
            get { return this._Status; }
        }

        public string MSISDN
        {
            set { this._MSISDN = value; }
            get { return this._MSISDN; }
        }

        public int? UserId
        {
            set { this._UserId = value; }
            get { return this._UserId; }
        }

        public DateTime? LastModify
        {
            set { this._LastModify = value; }
            get { return this._LastModify; }
        }

        public int? Count
        {
            set { this._count = value; }
            get { return this._count; }
        }
    }
}
