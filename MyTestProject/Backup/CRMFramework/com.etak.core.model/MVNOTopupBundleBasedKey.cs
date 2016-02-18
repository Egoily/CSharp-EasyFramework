using System;

namespace com.etak.core.model
{
    [Serializable]
    public class MVNOTopupBundleBasedKey
    {
        public int PackageID { get; set; }
        public int BundleID { get; set; }
        public decimal ThresholdTopupAmount { get; set; }

        /// <summary>
        /// 判断两个对象是否相同，这个方法需要重写
        /// </summary>
        /// <param name="obj">进行比较的对象</param>
        /// <returns>真true或假false</returns>
        public override bool Equals(object obj)
        {
            if (obj is MVNOTopupBundleBasedKey)
            {
                MVNOTopupBundleBasedKey second = obj as MVNOTopupBundleBasedKey;
                if (this.PackageID == second.PackageID
                     && this.BundleID == second.BundleID
                    && this.ThresholdTopupAmount == second.ThresholdTopupAmount)
                {
                    return true;
                }
                else return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
