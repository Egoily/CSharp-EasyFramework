using System;

namespace com.etak.core.model
{
    [Serializable]
    public class MNPPortabilityMultiPortInInfo
    {
       public MNPPortabilityMultiPortInInfo()
       {

       }

       public MNPPortabilityMultiPortInInfo(long seqID,string referenceCode,string msisdn,string iccid,int status)
       {
           this.seqID = seqID;
           this.referenceCode = referenceCode;
           this.msisdn = msisdn;
           this.iccid = iccid;
           this.status = status;
       }

       private long seqID;
       private string referenceCode;
       private string msisdn;
       private string iccid;
       private int status;

       public virtual long SEQID
       {
           get { return seqID; }
           set { seqID = value; }
       }

       public virtual string ReferenceCode
       {
           get { return referenceCode; }
           set { referenceCode = value; }
       }


       public virtual string Msisdn
       {
           get { return msisdn; }
           set { msisdn = value; }
       }

       public virtual string Iccid
       {
           get { return iccid; }
           set { iccid = value; }
       }

       public virtual int Status
       {
           get { return status; }
           set { status = value; }
       }


    }
}
