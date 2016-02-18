using System;

namespace com.etak.core.model
{
    /// <summary>
    /// this class is temparary for those class inherited from PromotionModelBase. 
    /// </summary>
    [Serializable]
    public class PromotionModelBase
    {

        /// <summary>
        /// Copies the data.
        /// </summary>
        /// <param name="from">From.</param>
        public virtual void CopyFieldDataFrom(object from)
        {
            //PropertyInfo[] pis = this.GetType().GetProperties();
            //FieldInfo[] fields = from.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            //foreach (FieldInfo f in fields)
            //{
            //    object value = f.GetValue(from);
            //    if (value != null)
            //    {
            //        PropertyInfo pro = this.GetType().GetProperty(f.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            //        if (pro != null)
            //        {
            //            pro.SetValue(this, value, null);
            //        }
            //        else
            //        {
            //            //
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Copies the data.
        /// </summary>
        /// <param name="from">From.</param>
        public virtual void CopyPropertyDataFrom(object from)
        {
            //PropertyInfo[] pis;
            //pis = from.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            //foreach (PropertyInfo f in pis)
            //{
            //    object value = f.GetValue(from, null);
            //    if (value != null)
            //    {
            //        PropertyInfo pro = this.GetType().GetProperty(f.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            //        if (pro != null)
            //        {
            //            pro.SetValue(this, value, null);
            //        }
            //        else
            //        {
            //            //
            //        }
            //    }
            //}
        }

        public virtual PromotionModelBase Clone()
        {
            return this.MemberwiseClone() as PromotionModelBase;
        }

        public virtual PromotionModelBase CreateHistory()
        {
            return null;
        }
    }
}
