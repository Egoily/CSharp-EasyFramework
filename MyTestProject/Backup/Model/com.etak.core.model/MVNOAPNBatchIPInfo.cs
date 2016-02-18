using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class MVNOAPNBatchIPInfo
    {
        private IList<MVNOAPNIPPoolInfo> _IPAddressList = new List<MVNOAPNIPPoolInfo>();
        private int? _BatchId;
        private string _APN;
        private string _DealerId;
        private int? _ShareType;
        private int? _IPStartPosition1;
        private int? _IPStartPosition2;
        private int? _IPStartPosition3;
        private int? _IPStartPosition4;
        private int? _IPEndPosition1;
        private int? _IPEndPosition2;
        private int? _IPEndPosition3;
        private int? _IPEndPosition4;
        private string _IPStart;
        private string _IPEnd;

        private int? _Count;
        private int? _UserId;
        private int? _MVNOID;
        private DateTime? _CreateDate;
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


        public IList<MVNOAPNIPPoolInfo> IPAddressList
        {
            set { this._IPAddressList = value; }
            get { return this._IPAddressList; }
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
        public string DealerId
        {
            set { this._DealerId = value; }
            get { return this._DealerId; }
        }

        public int? ShareType
        {
            set { this._ShareType = value; }
            get { return this._ShareType; }
        }
        public int? IPStartPosition1
        {
            set { this._IPStartPosition1 = value; }
            get { return this._IPStartPosition1; }
        }
        public int? IPStartPosition2
        {
            set { this._IPStartPosition2 = value; }
            get { return this._IPStartPosition2; }
        }
        public int? IPStartPosition3
        {
            set { this._IPStartPosition3 = value; }
            get { return this._IPStartPosition3; }
        }
        public int? IPStartPosition4
        {
            set { this._IPStartPosition4 = value; }
            get { return this._IPStartPosition4; }
        }
        public int? IPEndPosition1
        {
            set { this._IPEndPosition1 = value; }
            get { return this._IPEndPosition1; }
        }
        public int? IPEndPosition2
        {
            set { this._IPEndPosition2 = value; }
            get { return this._IPEndPosition2; }
        }
        public int? IPEndPosition3
        {
            set { this._IPEndPosition3 = value; }
            get { return this._IPEndPosition3; }
        }
        public int? IPEndPosition4
        {
            set { this._IPEndPosition4 = value; }
            get { return this._IPEndPosition4; }
        }
        public int? Count
        {
            set { this._Count = value; }
            get { return this._Count; }
        }
        public int? UserId
        {
            set { this._UserId = value; }
            get { return this._UserId; }
        }
        public int? MVNOID
        {
            set { this._MVNOID = value; }
            get { return this._MVNOID; }
        }
        public DateTime? CreateDate
        {
            set { this._CreateDate = value; }
            get { return this._CreateDate; }
        }

        public string IPStart
        {
            set { this._IPStart = value; }
            get { return this._IPStart; }
        }
        public string IPEnd
        {
            set { this._IPEnd = value; }
            get { return this._IPEnd; }
        }
    }
}
