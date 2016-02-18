using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class OperationLog
    {
        private static readonly Type _selfType = typeof(OperationLog);

        #region 成员
        private long? _code;
        private int? _orderCode;
        private int? _dealerID = -1;
        private string _externalCode = string.Empty;
        private long? _oldCode;
        private int? _userID = -1;
        private string _vmo;
        private string _description = string.Empty;
        private string _operationCode = string.Empty;
        private string _remark = string.Empty;
        private DateTime? _operationDate = DateTime.Now;
        private int? _result = (int)ResultType.Correct;
        private string _status = eOperationStatus.CO.ToString();
        private string _messages = string.Empty;
        private string _systemMessages = string.Empty;
        private byte[] _invokeParams;
        private int? _invoker;
        private string _name1;
        private string _name2;
        private string _name3;
        private string _name4;
        private string _name5;
        private string _name6;
        private string _name7;
        private string _name8;
        private string _name9;
        private string _name10;
        private string _value1;
        private string _value2;
        private string _value3;
        private string _value4;
        private string _value5;
        private string _value6;
        private string _value7;
        private string _value8;
        private string _value9;
        private string _value10;
        private long? _topupHistoryID;

        private long? _LOGID;

        private string _traceorder;

        private Int16 _currentParamIndex = 1;

        private OperationInfo _OperationInfo;

        public virtual OperationInfo OperationInfo
        {
            get { return _OperationInfo; }
            set { _OperationInfo = value; }
        }
        #endregion

        #region 属性

        public virtual long? Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public virtual int? DealerID
        {
            get { return _dealerID; }
            set { _dealerID = value; }
        }

        public virtual int? OrderCode
        {
            get { return _orderCode; }
            set { _orderCode = value; }
        }

        public virtual string ExternalCode
        {
            get { return _externalCode; }
            set { _externalCode = string.IsNullOrEmpty(value) ? value : (value.Length > 50 ? value.Substring(0, 46) + "..." : value); }
        }

        public virtual long? OldCode
        {
            get { return _oldCode; }
            set { _oldCode = value; }
        }

        public virtual int? UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public virtual string Vmo
        {
            get { return _vmo; }
            set { _vmo = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            set { _description = string.IsNullOrEmpty(value) ? value : (value.Length > 100 ? value.Substring(0, 96) + "..." : value); }
        }

        public virtual string OperationCode
        {
            get { return _operationCode; }
            set { _operationCode = value; }
        }

        public virtual string Remark
        {
            get { return _remark; }
            set { _remark = string.IsNullOrEmpty(value) ? value : (value.Length > 1000 ? value.Substring(0, 996) + "..." : value); }
        }

        public virtual DateTime? OperationDate
        {
            get { return _operationDate; }
            set { _operationDate = value; }
        }

        public virtual int? Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public virtual string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public virtual string Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        public virtual string SystemMessages
        {
            get { return _systemMessages; }
            set { _systemMessages = value; }
        }

        public virtual byte[] InvokeParams
        {
            get { return _invokeParams; }
            set { _invokeParams = value; }
        }

        public virtual int? Invoker
        {
            get { return _invoker; }
            set { _invoker = value; }
        }

        public virtual string Name1
        {
            get { return _name1; }
            set { _name1 = value; }
        }

        public virtual string Name2
        {
            get { return _name2; }
            set { _name2 = value; }
        }

        public virtual string Name3
        {
            get { return _name3; }
            set { _name3 = value; }
        }

        public virtual string Name4
        {
            get { return _name4; }
            set { _name4 = value; }
        }

        public virtual string Name5
        {
            get { return _name5; }
            set { _name5 = value; }
        }

        public virtual string Name6
        {
            get { return _name6; }
            set { _name6 = value; }
        }

        public virtual string Name7
        {
            get { return _name7; }
            set { _name7 = value; }
        }

        public virtual string Name8
        {
            get { return _name8; }
            set { _name8 = value; }
        }

        public virtual string Name9
        {
            get { return _name9; }
            set { _name9 = value; }
        }

        public virtual string Name10
        {
            get { return _name10; }
            set { _name10 = value; }
        }

        public virtual string Value1
        {
            get { return _value1; }
            set { _value1 = value; }
        }

        public virtual string Value2
        {
            get { return _value2; }
            set { _value2 = value; }
        }

        public virtual string Value3
        {
            get { return _value3; }
            set { _value3 = value; }
        }

        public virtual string Value4
        {
            get { return _value4; }
            set { _value4 = value; }
        }

        public virtual string Value5
        {
            get { return _value5; }
            set { _value5 = value; }
        }

        public virtual string Value6
        {
            get { return _value6; }
            set { _value6 = value; }
        }

        public virtual string Value7
        {
            get { return _value7; }
            set { _value7 = value; }
        }

        public virtual string Value8
        {
            get { return _value8; }
            set { _value8 = value; }
        }

        public virtual string Value9
        {
            get { return _value9; }
            set { _value9 = value; }
        }

        public virtual string Value10
        {
            get { return _value10; }
            set { _value10 = value; }
        }

        public virtual long? TopupHistoryID
        {
            get { return _topupHistoryID; }
            set { _topupHistoryID = value; }
        }

        public virtual long? LOGID
        {
            get { return _LOGID; }
            set { _LOGID = value; }
        }

        public virtual string TraceOrder
        {
            get { return _traceorder; }
            set { _traceorder = value; }
        }

        public virtual string Channel { get; set; }
        //added by neil at 2014/09/03
        public virtual string ForeignCode { get; set; }
        #endregion

        virtual public void AppendNameValueCollection(IList<String> names, IList<String> values)
        {
            if (names == null || names.Count == 0 || values == null || values.Count == 0)
            {
                return;
            }

            if ( (_currentParamIndex -1) + names.Count > 10)
            {
                throw new ArgumentException(
                    string.Format("Important values is out of the maximum:10.(method){0}.{1}", GetType().Name, MethodBase.GetCurrentMethod().Name));
            }

            if (names.Count != values.Count)
            {
                throw new ArgumentException(
                    string.Format("names are not corresponding to its values.(method){0}.{1}", GetType().Name,  MethodBase.GetCurrentMethod().Name));
            }

           
            for ( Int16 localIdx = 0; localIdx < values.Count; localIdx++)
            {
                string nameItem = string.Format("Name{0}", _currentParamIndex);
                string valueItem = string.Format("Value{0}", _currentParamIndex);

                System.Reflection.PropertyInfo nameInfo = _selfType.GetProperty(nameItem);
                System.Reflection.PropertyInfo valueInfo = _selfType.GetProperty(valueItem);
                
                nameInfo.SetValue(this, names[localIdx], null);
                valueInfo.SetValue(this, values[localIdx], null);
                _currentParamIndex++;
            }
        }

        virtual public void LogParameters(object[] apiParameters)
        {
            BinaryFormatter se = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {

                se.Serialize(memStream, apiParameters);
                InvokeParams  = memStream.ToArray();
            }
        }

        virtual public void AppendMessage(string message)
        {
            Messages += Environment.NewLine + message;
        }

        virtual public void AppendSystemMessage(string message)
        {
            SystemMessages += Environment.NewLine + message;
        }

        virtual public void AppendSystemMessage(Exception ex)
        {
            if (ex == null)
                return;

            StringBuilder innerExceptionMessage = new StringBuilder(ex.Message);
            Exception innerException = ex;
            while ((innerException = innerException.InnerException) != null)
            {
                innerExceptionMessage.AppendLine(innerException.Message);
            }

            string exceptionMessage = string.Format(@"Message = {0}. Exception = {1}", innerExceptionMessage, ex.ToString());
            SystemMessages += Environment.NewLine + exceptionMessage;
        }
    }
}
