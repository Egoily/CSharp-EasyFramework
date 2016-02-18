using System;

namespace com.etak.core.model
{
    [Serializable]
    public class HLRTemplateConfig
    {
        public HLRServiceConfig ServiceConfig { get; set; }
        public HLRCamelConfig CamelConfig { get; set; }
        public HLRGprsConfig GprsConfig { get; set; }
    }

    [Serializable]
    public class HLRServiceConfig
    {
        public string[] TS { get; set; }
        public string[] BS { get; set; }
        public string[] ODB { get; set; }
        public HLRSSConfig SS { get; set; }
        public HLRCFConfig CF { get; set; }
    }

    [Serializable]
    public class HLRSSConfig
    {
        public string CLIP { get; set; }
        public string CLIR { get; set; }
        public string CAW { get; set; }
        public string HOLD { get; set; }
        public string MPTY { get; set; }
    }

    [Serializable]
    public class HLRCFConfig
    {
        public string CFU { get; set; }
        public string CFB { get; set; }
        public string CFNRY { get; set; }
        public string CFNRC { get; set; }
    }

    [Serializable]
    public class HLRCamelConfig
    {
        public string UCSI_Template { get; set; }
    }

    [Serializable]
    public class HLRGprsConfig
    {
        public string NetowrkType { get; set; }
        public string APNName { get; set; }
        public string QOSGbitRateDwn { get; set; }
        public string QOSMaxbitRateDwn { get; set; }
        public string QOSGbitRateUp { get; set; }
        public string QOSGMaxRateUp { get; set; }
        public string MMS { get; set; }
        public string WAP { get; set; }
        public string OBOPRE { get; set; }
        public string OBOPRI { get; set; }
    }
}
