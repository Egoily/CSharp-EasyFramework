using System;

namespace com.etak.core.model
{
    [Serializable]
    public class UserInfo
    {
        private int? _UserID = null;
        private int? _TitleID = null;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _Company;
        private int? _LanguageID = null;
        private int? _DepartmentID = null;
        private int? _StatusID = null;
        private string _UserName;
        private string _Password;
        private string _Telephone;
        private string _Mobile;
        private string _Email;
        private DateTime? _CreateDate = null;
        private int? _CreateUserID = null;
        private int? _Status = null;
        private DateTime? _UpdateDate = null;
        private int? _UpdateUserID = null;
        private int? _FiscalUnit = null;
        private int? _LogoID = null;
        private int? _AccountStatus = null;
        private int? _AccountExpire = null;
        private int? _AccountChange = null;
        private DateTime? _AccountFirst = null;
        private DateTime? _AccountLast = null;
        private DateTime? _AccountGUI = null;
        private DateTime? _AccountPassword = null;
        private int? _ShowAllDealer = null;
        private int? _StyleID = 10001;
        private int? _ThemeID = 10100001;
        private int? _ChangeAlert = 5;

        public int? ChangeAlert
        {
            get { return _ChangeAlert; }
            set { _ChangeAlert = value; }
        }

        private int? _PasswordPeriod = 30;

        public int? PasswordPeriod
        {
            get { return _PasswordPeriod; }
            set { _PasswordPeriod = value; }
        }

        #region Attribute

        public virtual int? UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public virtual int? TitleID
        {
            get { return _TitleID; }
            set { _TitleID = value; }
        }

        public virtual string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public virtual string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        public virtual string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public virtual string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }

        public virtual int? LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }

        public virtual int? DepartmentID
        {
            get { return _DepartmentID; }
            set { _DepartmentID = value; }
        }

        public virtual int? StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }

        public virtual string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public virtual string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public virtual string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }

        public virtual string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public virtual DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public virtual int? CreateUserID
        {
            get { return _CreateUserID; }
            set { _CreateUserID = value; }
        }

        public virtual int? Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public virtual DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        public virtual int? UpdateUserID
        {
            get { return _UpdateUserID; }
            set { _UpdateUserID = value; }
        }

        public virtual int? FiscalUnit
        {
            get { return _FiscalUnit; }
            set { _FiscalUnit = value; }
        }

        public virtual int? LogoID
        {
            get { return _LogoID; }
            set { _LogoID = value; }
        }

        public virtual int? AccountStatus
        {
            get { return _AccountStatus; }
            set { _AccountStatus = value; }
        }

        public virtual int? AccountExpire
        {
            get { return _AccountExpire; }
            set { _AccountExpire = value; }
        }

        public virtual int? AccountChange
        {
            get { return _AccountChange; }
            set { _AccountChange = value; }
        }

        public virtual DateTime? AccountFirst
        {
            get { return _AccountFirst; }
            set { _AccountFirst = value; }
        }

        public virtual DateTime? AccountLast
        {
            get { return _AccountLast; }
            set { _AccountLast = value; }
        }

        public virtual DateTime? AccountGUI
        {
            get { return _AccountGUI; }
            set { _AccountGUI = value; }
        }

        public virtual DateTime? AccountPassword
        {
            get { return _AccountPassword; }
            set { _AccountPassword = value; }
        }

        public virtual int? ShowAllDealer
        {
            get { return _ShowAllDealer; }
            set { _ShowAllDealer = value; }
        }

        public virtual int? StyleID
        {
            get { return _StyleID; }
            set { _StyleID = value; }
        }

        public virtual int? ThemeID
        {
            get { return _ThemeID; }
            set { _ThemeID = value; }
        }

        #endregion Attribute
    }
}