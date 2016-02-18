using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class UnitCategoryInfo
    {
        #region 成员
        private int? _UnitCategoryID;
        private string _UnitCategoryName;
        private bool _RoundUp;
        private int? _Unit1;
        private int? _Unit2;
        private int? _Prompt;

        #endregion

        #region attribute

        public int? UnitCategoryID
        {
            get { return _UnitCategoryID; }
            set { _UnitCategoryID = value; }
        }

        public string UnitCategoryName
        {
            get { return _UnitCategoryName; }
            set { _UnitCategoryName = value; }
        }

        public bool RoundUp
        {
            get { return _RoundUp; }
            set { _RoundUp = value; }
        }
 
        public int? Unit1
        {
            get { return _Unit1; }
            set { _Unit1 = value; }
        }

        public int? Unit2
        {
            get { return _Unit2; }
            set { _Unit2 = value; }
        }

        public int? Prompt
        {
            get { return _Prompt; }
            set { _Prompt = value; }
        }
        #endregion
    }
}
