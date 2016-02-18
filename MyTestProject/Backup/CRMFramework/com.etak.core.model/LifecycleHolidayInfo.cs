using System;

namespace com.etak.core.model
{
   /// <summary>
   ///功能描述    :    
   ///开发者      :    
   ///建立时间    :    2010-8-19 18:31:11
   ///修订描述    :    
   ///进度描述    :    
   ///版本号      :    1.0
   ///最后修改时间:    2010-8-19 18:31:11
   ///
   ///Function Description :    
   ///Developer                :    
   ///Builded Date:    2010-8-19 18:31:11
   ///Revision Description :    
   ///Progress Description :    
   ///Version Number        :    1.0
   ///Last Modify Date     :    2010-8-19 18:31:11
   /// </summary>
    [Serializable]
    public class LifecycleHolidayInfo
   {
      #region 构造函数
      public LifecycleHolidayInfo()
      {}

	  public LifecycleHolidayInfo(string HOLIDAYDESCRIPTION)
      {
		 //this._MVNOID=MVNOID;
		 //this._INTDATE=INTDATE;
         this._HOLIDAYDESCRIPTION=HOLIDAYDESCRIPTION;
      }
      #endregion

	  #region 成员
	  //private int _MVNOID;
	  //private int _INTDATE;
	  private string _HOLIDAYDESCRIPTION;
	  private Holidaykey _HolidayCompositekey;
	  #endregion


      #region 属性
	  //public  int MVNOID
	  //{
	  //   get {  return _MVNOID; }
	  //   set {  _MVNOID = value; }
	  //}

	  //public  int INTDATE
	  //{
	  //   get {  return _INTDATE; }
	  //   set {  _INTDATE = value; }
	  //}

	  public string HOLIDAYDESCRIPTION
	  {
		  get { return _HOLIDAYDESCRIPTION; }
		  set { _HOLIDAYDESCRIPTION = value; }
	  }

	  public Holidaykey HolidayCompositekey
	  {
		  get { return _HolidayCompositekey; }
		  set { _HolidayCompositekey = value; }
	  }

	  #endregion

   }
}
