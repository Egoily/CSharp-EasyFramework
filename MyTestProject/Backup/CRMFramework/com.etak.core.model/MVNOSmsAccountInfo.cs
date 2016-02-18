using System;

namespace com.etak.core.model
{
    [Serializable]
    public class MVNOSMSAccountInfo
    {
        #region property
        private int accountId;
        private string appId;
        private string appUserName;
        private string appUserPassword;
        private string appHost;
        private string appPath;
        private string appPort;
        private string appName;
        private string appWord;
        private string sender;
        private string dlrUrl;

        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }

        public string AppId
        {
            get { return appId; }
            set { appId = value; }
        }

        public string AppUserName
        {
            get { return appUserName; }
            set { appUserName = value; }
        }

        public string AppUserPassword
        {
            get { return appUserPassword; }
            set { appUserPassword = value; }
        }

        public string AppHost
        {
            get { return appHost; }
            set { appHost = value; }
        }

        public string AppPath
        {
            get { return appPath; }
            set { appPath = value; }
        }

        public string AppPort
        {
            get { return appPort; }
            set { appPort = value; }
        }

        public string AppName
        {
            get { return appName; }
            set { appName = value; }
        }

        public string AppWord
        {
            get { return appWord; }
            set { appWord = value; }
        }

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        public string DlrUrl
        {
            get { return dlrUrl; }
            set { dlrUrl = value; }
        }

        //By Gary 2011.7.14
        public int? DealerId
        {
            get;
            set;
        }

        #endregion

        public MVNOSMSAccountInfo() { }
        public MVNOSMSAccountInfo(int accountId,string appId,string appUserName,string appUserPassword,string appHost,string appPath,
                                  string appPort,string appName,string appWord,string sender,string dlrUrl)
        {
            this.accountId = accountId;
            this.appId = appId;
            this.appUserName = appUserName;
            this.appUserPassword = appUserPassword;
            this.appHost = appHost;
            this.appPath = appPath;
            this.appPort = appPort;
            this.appName = appName;
            this.appWord = appWord;
            this.sender = sender;
            this.dlrUrl = dlrUrl;
        }
    }
}
