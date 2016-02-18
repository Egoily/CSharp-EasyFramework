using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class OperationDetailLog
    {

        #region 成员

        private long? _detailOrderCode;
        private string _operationResult;
        private string _remarks;
        private DateTime? _createDate = DateTime.Now;
        private OperationLog _operationLog;
        
        #endregion

        #region 属性

        public virtual long? DetailOrderCode
        {
            get { return _detailOrderCode; }
            set { _detailOrderCode = value; }
        }
        
        public virtual string OperationResult
        {
            get { return _operationResult; }
            set { _operationResult = value; }
        }
        public virtual string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        public virtual DateTime? CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }
        public virtual OperationLog OperationLog
        {
            get { return _operationLog; }
            set { _operationLog = value; }
        }
        #endregion

        public OperationDetailLog() { }

        public OperationDetailLog(string operationResult, string remarks)
        {
            this._operationResult = operationResult;
            this._remarks = remarks;
        }

        public OperationDetailLog(string operationResult, string remarks, DateTime createDate)
        {
            this._operationResult = operationResult;
            this._remarks = remarks;
            this._createDate = createDate;
        }
    }
}
