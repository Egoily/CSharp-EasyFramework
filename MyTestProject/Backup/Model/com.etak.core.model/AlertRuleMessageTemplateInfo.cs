using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class AlertRuleMessageTemplateInfo
    {
        private int _TemplateID;

        public int TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }
        private int _MNVOID;

        public int MNVOID
        {
            get { return _MNVOID; }
            set { _MNVOID = value; }
        }

        private string _AlertRuleName;

        public string AlertRuleName
        {
            get { return _AlertRuleName; }
            set { _AlertRuleName = value; }
        }
        private int _MessageID;

        public int MessageID
        {
            get { return _MessageID; }
            set { _MessageID = value; }
        }
        private int _TemplateType;

        public int TemplateType
        {
            get { return _TemplateType; }
            set { _TemplateType = value; }
        }
        private int _TemplateLanguage;

        public int TemplateLanguage
        {
            get { return _TemplateLanguage; }
            set { _TemplateLanguage = value; }
        }
        private string _TemplateName;

        public string TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }
        private string _TemplateTheme;

        public string TemplateTheme
        {
            get { return _TemplateTheme; }
            set { _TemplateTheme = value; }
        }
        private string _TemplateText;

        public string TemplateText
        {
            get { return _TemplateText; }
            set { _TemplateText = value; }
        }
        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
    }
}
