using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class ImportDetailsInfo
    {
        private string _TransactionID;        
        private string _ICCID;        
        private string _IMSI;        
        private string _PIN1;        
        private string _PIN2;        
        private string _PUK1;        
        private string _PUK2;        
        private string _KI;        
        private string _OPC;        
        private string _KIC_0F;        
        private string _KID_0F;        
        private string _KIK_0F;        
        private string _ADM1;        
        private string _ADM2;        
        private string _ACC;        
        private string _MSISDN;        
        private bool _Flag;
        private string _IMSI2;
        private string _IMSI3;
        private string _IMSI4;
        private string _IMSI5;
        private string _IMSI6;
        private string _IMSI7;
        private string _IMSI8;
        private string _IMSI9;
        private string _IMSI10;
        private string _IMSI11;
        private string _IMSI12;
        private string _IMSI13;
        private string _IMSI14;
        private string _IMSI15;
        //add 20120207
        private string _KIC2;
        private string _KID2;
        private string _KIK2; 
        

        #region Attribute
        public virtual string TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }

        public virtual string ICCID
        {
            get { return _ICCID; }
            set { _ICCID = value; }
        }

        public virtual string IMSI
        {
            get { return _IMSI; }
            set { _IMSI = value; }
        }

        public virtual string PIN1
        {
            get { return _PIN1; }
            set { _PIN1 = value; }
        }

        public virtual string PIN2
        {
            get { return _PIN2; }
            set { _PIN2 = value; }
        }

        public virtual string PUK1
        {
            get { return _PUK1; }
            set { _PUK1 = value; }
        }

        public virtual string PUK2
        {
            get { return _PUK2; }
            set { _PUK2 = value; }
        }

        public virtual string KI
        {
            get { return _KI; }
            set { _KI = value; }
        }

        public virtual string OPC
        {
            get { return _OPC; }
            set { _OPC = value; }
        }

        public virtual string KIC_0F
        {
            get { return _KIC_0F; }
            set { _KIC_0F = value; }
        }

        public virtual string KID_0F
        {
            get { return _KID_0F; }
            set { _KID_0F = value; }
        }

        public virtual string KIK_0F
        {
            get { return _KIK_0F; }
            set { _KIK_0F = value; }
        }

        public virtual string ADM1
        {
            get { return _ADM1; }
            set { _ADM1 = value; }
        }

        public virtual string ADM2
        {
            get { return _ADM2; }
            set { _ADM2 = value; }
        }

        public virtual string ACC
        {
            get { return _ACC; }
            set { _ACC = value; }
        }

        public virtual string MSISDN
        {
            get { return _MSISDN; }
            set { _MSISDN = value; }
        }

        public virtual bool Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }

        public virtual string IMSI2
        {
            get { return _IMSI2; }
            set { _IMSI2 = value; }
        }

        public virtual string IMSI3
        {
            get { return _IMSI3; }
            set { _IMSI3 = value; }
        }

        public virtual string IMSI4
        {
            get { return _IMSI4; }
            set { _IMSI4 = value; }
        }

        public virtual string IMSI5
        {
            get { return _IMSI5; }
            set { _IMSI5 = value; }
        }
        
        public virtual string IMSI6
        {
            get { return _IMSI6; }
            set { _IMSI6 = value; }
        }

        public virtual string IMSI7
        {
            get { return _IMSI7; }
            set { _IMSI7 = value; }
        }

        public virtual string IMSI8
        {
            get { return _IMSI8; }
            set { _IMSI8 = value; }
        }

        public virtual string IMSI9
        {
            get { return _IMSI9; }
            set { _IMSI9 = value; }
        }

        public virtual string IMSI10
        {
            get { return _IMSI10; }
            set { _IMSI10 = value; }
        }

        public virtual string IMSI11
        {
            get { return _IMSI11; }
            set { _IMSI11 = value; }
        }

        public virtual string IMSI12
        {
            get { return _IMSI12; }
            set { _IMSI12 = value; }
        }

        public virtual string IMSI13
        {
            get { return _IMSI13; }
            set { _IMSI13 = value; }
        }

        public virtual string IMSI14
        {
            get { return _IMSI14; }
            set { _IMSI14 = value; }
        }

        public virtual string IMSI15
        {
            get { return _IMSI15; }
            set { _IMSI15 = value; }
        }
        //add 20120207
        public virtual string KIC2
        {
            get { return _KIC2; }
            set { _KIC2 = value; }
        }

        public virtual string KID2
        {
            get { return _KID2; }
            set { _KID2 = value; }
        }

        public virtual string KIK2
        {
            get { return _KIK2; }
            set { _KIK2 = value; }
        }
        #endregion

        /// <summary>
        /// Override Equals method needed by Nhibernate to map the entity 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == this) return true;

            var other = obj as ImportDetailsInfo;
            if (other == null) return false;

            if (TransactionID == other.TransactionID && ICCID == other.ICCID)
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
                int result = TransactionID.GetHashCode();
                result = 29*result + ICCID.GetHashCode();

                return result;
            }
        }
    }
}
