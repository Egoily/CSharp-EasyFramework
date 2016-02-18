using System;

namespace com.etak.core.model
{
    [Serializable]
    public class SystemConfigIDInfo
    {
        private string idName;
        private int idValue;

        public SystemConfigIDInfo()
        {

        }

        public SystemConfigIDInfo(string idName,int idValue)
        {
            this.idName = idName;
            this.idValue = idValue;
        }

        public virtual string IDName
        {
            get { return idName; }
            set { idName = value; }
        }

        public virtual int IDValue
        {
            get { return idValue; }
            set { idValue = value; }
        }
    }
}
