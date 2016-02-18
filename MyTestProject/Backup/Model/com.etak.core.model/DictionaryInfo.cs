using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class DictionaryInfo
    {
        #region 成员
        private int _ID;
        private int _DictionaryID;
        private int _ParentID;
        private string _DictionaryText;
        private int _DictionaryType;
        private int _Level;
        private int? _Item_Post = null;
        private string _Value;
        private string _ToolTipText;
        private string _Link;
        private string _Description;
        private int? _Update_UserID = null;
        private DateTime? _Update_Time = null;
        private int _Create_UserID;
        private DateTime _Create_Time;
        private int _State;
        #endregion


        #region 属性
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int DictionaryID
        {
            get { return _DictionaryID; }
            set { _DictionaryID = value; }
        }

        public int ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        public string DictionaryText
        {
            get { return _DictionaryText; }
            set { _DictionaryText = value; }
        }

        public int DictionaryType
        {
            get { return _DictionaryType; }
            set { _DictionaryType = value; }
        }

        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        public int? Item_Post
        {
            get { return _Item_Post; }
            set { _Item_Post = value; }
        }

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public string ToolTipText
        {
            get { return _ToolTipText; }
            set { _ToolTipText = value; }
        }

        public string Link
        {
            get { return _Link; }
            set { _Link = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public int? Update_UserID
        {
            get { return _Update_UserID; }
            set { _Update_UserID = value; }
        }

        public DateTime? Update_Time
        {
            get { return _Update_Time; }
            set { _Update_Time = value; }
        }

        public int Create_UserID
        {
            get { return _Create_UserID; }
            set { _Create_UserID = value; }
        }

        public DateTime Create_Time
        {
            get { return _Create_Time; }
            set { _Create_Time = value; }
        }

        public int State
        {
            get { return _State; }
            set { _State = value; }
        }

        #endregion

    }
}
