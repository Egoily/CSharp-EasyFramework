using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class OperationInfo
    {      
        private IList<OperationLog> _OperationLogInfoList = new List<OperationLog>();

        virtual public long? ID { get; set; }
        virtual public int? MVNOID { get; set; }
        virtual public string MVNOName {get;set;}
        virtual public string CustomerName { get; set; }
        virtual public int? CustomerID { get; set; }
        virtual public string Business { get; set; }
        virtual public string Operation { get; set; }
        virtual public int? OperationLevel { get; set; }
        virtual public string Detail { get; set; }
        virtual public string IP { get; set; }
        virtual public int? CreateUserID { get; set; }
        virtual public DateTime? CreateDate { get; set; }
        virtual public string CreateUserName { get; set; }
        virtual public int? SourceID { get; set; }
        virtual public string SourceName { get; set; }
        virtual public string OperationResult { get; set; }
        virtual public string MSISDN { get; set; }
        virtual public string ACTIONTYPE { get; set; }
        virtual public int? LOGTYPE { get; set; }
        virtual public string OPERATIONCODE { get; set; }
        virtual public string MESSAGES { get; set; }
        virtual public long? MESSAGESID { get; set; }
        virtual public string OLDVALUE { get; set; }
        virtual public string NEWVALUE { get; set; }

        virtual public IList<OperationLog> OperationLogInfoList
        {
            get { return _OperationLogInfoList; }
            set { _OperationLogInfoList = value; }
        }
    }
}
