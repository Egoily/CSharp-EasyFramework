using System;
using System.Reflection;
using System.Text;
using System.Xml;

namespace com.etak.core.model
{
    /// <summary>
    /// this class is temparary for those class inherited from Model Base. 
    /// </summary>
    [Serializable]
    public abstract class ModelBase
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public virtual ModelBase Clone()
        {
            ModelBase model = this.MemberwiseClone() as ModelBase;
            if (HlrInfo != null)
            {
                model.HlrInfo = new Hlr(HlrInfo.HlrServerName, HlrInfo.HlrServerPort, HlrInfo.HlrUserName, HlrInfo.HlrUserPwd);
            }
            return model;
        }

        Hlr _hlr;
        public virtual Hlr HlrInfo
        {
            get
            {
                return _hlr;
            }
            set
            {
                _hlr = value;
            }
        }


        /// <summary>
        /// Gets or sets the mapping data.
        /// </summary>
        /// <value>The mapping data.</value>
        public virtual object MappingData
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        /// <value>The name of the type.</value>
        public virtual String TypeName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <returns></returns>
        public virtual object GetKey()
        {
            return null;
        }

        /// <summary>
        /// Copies the data.
        /// </summary>
        /// <param name="from">From.</param>
        public virtual void CopyFieldDataFrom(object from)
        {
            FieldInfo[] fields = from.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            foreach (FieldInfo f in fields)
            {
                object value = f.GetValue(from);
                if (value != null)
                {
                    System.Reflection.PropertyInfo pro = this.GetType().GetProperty((f.Name == "SubscriptionID" ? "IMSI" : f.Name), BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase); //Hlr5.2 use SubscriptionID replace the imsi.
                    if (pro != null)
                    {
                        pro.SetValue(this, value, null);
                    }
                }
            }
        }

        /// <summary>
        /// Copies the data.
        /// </summary>
        /// <param name="from">From.</param>
        public virtual void CopyPropertyDataFrom(object from)
        {
            System.Reflection.PropertyInfo[] 
            pis = from.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            foreach (System.Reflection.PropertyInfo f in pis)
            {
                object value = f.GetValue(from, null);
                if (value != null)
                {
                    System.Reflection.PropertyInfo pro = this.GetType().GetProperty(f.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (pro != null)
                    {
                        pro.SetValue(this, value, null);
                    }
                }
            }
        }




        /// <summary>
        ///convert this object To the XML string.
        /// </summary>
        /// <returns></returns>
        public virtual string ToXml()
        {
            System.Reflection.PropertyInfo[] pis = this.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Insert(0, "<Entity>");
            foreach (System.Reflection.PropertyInfo p in pis)
            {
                if (p.Name.Equals("HlrInfo"))
                {
                    System.Reflection.PropertyInfo[] hlrProps = p.PropertyType.GetProperties();
                    sb.Append("<HlrInfo>");
                    object hlr = p.GetValue(this, null);
                    if (hlr != null)
                    {
                        foreach (System.Reflection.PropertyInfo hlrP in hlrProps)
                        {
                            sb.AppendFormat("<{0}>{1}</{0}>", hlrP.Name, hlrP.GetValue(hlr, null));
                        }
                    }
                    sb.Append("</HlrInfo>");
                }
                else
                {
                    if (p.PropertyType == typeof(DateTime))
                    {
                        object value = p.GetValue(this, null);
                        if (value != null)
                        {
                            sb.AppendFormat("<{0}>{1}</{0}>", p.Name, Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<{0}>{1}</{0}>", p.Name, p.GetValue(this, null));
                    }
                }
            }
            sb.Append("</Entity>");
            return sb.ToString();           
        }

        /// <summary>
        ///convert this object To the XML string.
        /// </summary>
        /// <returns></returns>
        public virtual void FromXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode node = doc.FirstChild;
            foreach (XmlNode cn in node.ChildNodes)
            {
                if (cn.Name.Equals("HlrInfo"))
                {
                    this.HlrInfo = new Hlr();
                    foreach (XmlNode hlr in cn.ChildNodes)
                    {
                        switch (hlr.Name)
                        {
                            case "HlrServerName":
                                this.HlrInfo.HlrServerName = hlr.InnerText;
                                break;
                            case "HlrUserName":
                                this.HlrInfo.HlrUserName = hlr.InnerText;
                                break;
                            case "HlrUserPwd":
                                this.HlrInfo.HlrUserPwd = hlr.InnerText;
                                break;
                            case "HlrServerPort":
                                this.HlrInfo.HlrServerPort = hlr.InnerText;
                                break;
                        }
                    }
                }
                else
                {
                    System.Reflection.PropertyInfo pro = this.GetType().GetProperty(cn.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (pro != null)
                    {
                        try
                        {
                            object value = Convert.ChangeType(cn.InnerText, pro.PropertyType);
                            pro.SetValue(this, value, null);
                        }
                        catch (Exception)
                        {
                            //throw ex;
                        }
                    }
                }
            }
        }


        public virtual ModelBase CreateHistory()
        {
            return null;
        }



    }

    [Serializable]
    public class Hlr
    {
        public Hlr()
        {
        }

        public Hlr(string server, string port, string userName, string passWord)
        {
            _hlrServerName = server;
            _hlrServerPort = port;
            _hlrUserName = userName;
            _hlrUserPwd = passWord;
        }

        #region hlr info
        string _hlrServerName, _hlrServerPort, _hlrUserName, _hlrUserPwd;

        /// <summary>
        /// Gets or sets the HLR server.
        /// </summary>
        /// <value>The HLR server.</value>
        public virtual string HlrServerName
        {
            get
            {
                return _hlrServerName;
            }
            set
            {
                this._hlrServerName = value;
            }
        }

        /// <summary>
        /// Gets or sets the HLR server port.
        /// </summary>
        /// <value>The HLR server port.</value>
        public virtual string HlrServerPort
        {
            get
            {
                return _hlrServerPort;
            }
            set
            {
                this._hlrServerPort = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the HLR user.
        /// </summary>
        /// <value>The name of the HLR user.</value>
        public virtual string HlrUserName
        {
            get
            {
                return _hlrUserName;
            }
            set
            {
                this._hlrUserName = value;
            }
        }

        /// <summary>
        /// Gets or sets the HLR user PWD.
        /// </summary>
        /// <value>The HLR user PWD.</value>
        public virtual string HlrUserPwd
        {
            get
            {
                return _hlrUserPwd;
            }
            set
            {
                this._hlrUserPwd = value;
            }
        }
        #endregion

    }
}
