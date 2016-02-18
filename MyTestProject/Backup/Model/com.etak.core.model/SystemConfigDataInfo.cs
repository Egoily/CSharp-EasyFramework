using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class SystemConfigDataInfo
    {
        private string _Item;        
        private string _Name;        
        private string _Value;        
        private string _Description;        
        private string _Version;        
        

        #region Attribute
        public virtual string Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        public virtual string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public virtual string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public virtual string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }
        #endregion
    }
}
