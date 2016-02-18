using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class AlertRuleInfo
    {
        private int _AlertRuleID;

        public int AlertRuleID
        {
            get { return _AlertRuleID; }
            set { _AlertRuleID = value; }
        }
        private int _MNVOID;

        public int MNVOID
        {
            get { return _MNVOID; }
            set { _MNVOID = value; }
        }
        private string _MNVOName;

        public string MNVOName
        {
            get { return _MNVOName; }
            set { _MNVOName = value; }
        }
        private string _AlertRuleName;

        public string AlertRuleName
        {
            get { return _AlertRuleName; }
            set { _AlertRuleName = value; }
        }
        private string _MatterName;

        public string MatterName
        {
            get { return _MatterName; }
            set { _MatterName = value; }
        }
        private int _ISActive;

        public int ISActive
        {
            get { return _ISActive; }
            set { _ISActive = value; }
        }
        private string _ParametersName;

        public string ParametersName
        {
            get { return _ParametersName; }
            set { _ParametersName = value; }
        }
        private string _ParametersType;

        public string ParametersType
        {
            get { return _ParametersType; }
            set { _ParametersType = value; }
        }
        private string _ParametersValues;

        public string ParametersValues
        {
            get { return _ParametersValues; }
            set { _ParametersValues = value; }
        }
        private string _ParametersUnit;

        public string ParametersUnit
        {
            get { return _ParametersUnit; }
            set { _ParametersUnit = value; }
        }
        private string _Name1;

        public string Name1
        {
            get { return _Name1; }
            set { _Name1 = value; }
        }
        private string _Name2;

        public string Name2
        {
            get { return _Name2; }
            set { _Name2 = value; }
        }
        private string _Name3;

        public string Name3
        {
            get { return _Name3; }
            set { _Name3 = value; }
        }
        private string _Name4;

        public string Name4
        {
            get { return _Name4; }
            set { _Name4 = value; }
        }
        private string _Name5;

        public string Name5
        {
            get { return _Name5; }
            set { _Name5 = value; }
        }
        private string _Values1;

        public string Values1
        {
            get { return _Values1; }
            set { _Values1 = value; }
        }
        private string _Values2;

        public string Values2
        {
            get { return _Values2; }
            set { _Values2 = value; }
        }
        private string _Values3;

        public string Values3
        {
            get { return _Values3; }
            set { _Values3 = value; }
        }
        private string _Values4;

        public string Values4
        {
            get { return _Values4; }
            set { _Values4 = value; }
        }
        private string _Values5;

        public string Values5
        {
            get { return _Values5; }
            set { _Values5 = value; }
        }
        private string _CreateUser;

        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private DateTime _CreateDate;

        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
        private string _UpdateUser;

        public string UpdateUser
        {
            get { return _UpdateUser; }
            set { _UpdateUser = value; }
        }
        private DateTime _UpdateDate;

        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        private int _UserID;

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
    }
}
