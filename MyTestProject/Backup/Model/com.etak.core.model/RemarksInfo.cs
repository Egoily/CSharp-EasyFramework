using System;

namespace com.etak.core.model
{
    [Serializable]
    public class RemarksInfo
    {
        public virtual CustomerInfo CustomerInfo { get; set; }
        public virtual int? RemarkID { get; set; }
        public virtual string Remark { get; set; }
        public virtual string Subject { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual int? UserID { get; set; }
        public virtual string AttachFileName { get; set; }
        public virtual int? StatusID { get; set; }
        public virtual Byte[] AttachFile { get; set; }

        public virtual RemarksInfo Clone()
        {
            return this.MemberwiseClone() as RemarksInfo;
        }
    }
}
