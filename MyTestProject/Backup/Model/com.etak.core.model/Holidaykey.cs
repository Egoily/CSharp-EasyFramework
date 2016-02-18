using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{

	[DataContract]
    [Serializable]
    public class Holidaykey
	{
		private int _MVNOID;
		private int _INTDATE;

		public int MVNOID
		{
			get { return _MVNOID; }
			set { _MVNOID = value; }
		}

		public int INTDATE
		{
			get { return _INTDATE; }
			set { _INTDATE = value; }
		}

		/// <summary>
		/// 判断两个对象是否相同，这个方法需要重写
		/// </summary>
		/// <param name="obj">进行比较的对象</param>
		/// <returns>真true或假false</returns>
		public override bool Equals(object obj)
		{
			if (obj is Holidaykey)
			{
				Holidaykey second = obj as Holidaykey;
				if (this.MVNOID == second.MVNOID
					 && this.INTDATE == second.INTDATE)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

	}
}

