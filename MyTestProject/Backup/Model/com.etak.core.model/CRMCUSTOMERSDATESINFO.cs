using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class CRMCUSTOMERSDATESINFO
    {
        #region
        private DateTime? _DATEVALUE = null;

        private CRMCUSTOMERSDATESPKINFO _PKInfo;
        public CRMCUSTOMERSDATESPKINFO PKInfo
        {
            get { return _PKInfo; }
            set { _PKInfo = value; }
        }
        #endregion

        #region Attribute
        public DateTime? DATEVALUE
        {
            get { return _DATEVALUE; }
            set { _DATEVALUE = value; }
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj is CRMCUSTOMERSDATESINFO)
            {
                CRMCUSTOMERSDATESINFO second = obj as CRMCUSTOMERSDATESINFO;
                if (this.PKInfo.CUSTOMERID == second.PKInfo.CUSTOMERID
                    && this.PKInfo.DATETYPEID == second.PKInfo.DATETYPEID)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = PKInfo.CUSTOMERID.GetHashCode();
                result = 29 * result + PKInfo.DATETYPEID.GetHashCode();
                return result;
            }
        }
    }

    [DataContract]
    [Serializable]
    public class CRMCUSTOMERSDATESPKINFO
    {
        #region
        private int? _CUSTOMERID = null;
        private int? _DATETYPEID = null;     
        #endregion

        #region Attribute
        public int? CUSTOMERID
        {
            get { return _CUSTOMERID; }
            set { _CUSTOMERID = value; }
        }

        public int? DATETYPEID
        {
            get { return _DATETYPEID; }
            set { _DATETYPEID = value; }
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
            var other = obj as CRMCUSTOMERSDATESPKINFO;
            if (other == null) return false;

            if (CUSTOMERID == other.CUSTOMERID && DATETYPEID == other.DATETYPEID) 
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
                int result;
                result = CUSTOMERID.GetHashCode();
                result = 29 * result + DATETYPEID.GetHashCode();
                return result;
            }
        }
        
    }
}
