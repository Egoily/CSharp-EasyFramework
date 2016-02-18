using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
	[DataContract]
    [Serializable]
    public class InterfaceHistory 
	{

		#region Private Members

		private long _seqid; 
		private string _referencecode; 
		private string _requestxml; 
		private string _responsexml; 
		private DateTime _createtime; 		
		#endregion

		#region Constructor

		public InterfaceHistory()
		{
			 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public  InterfaceHistory(
			long seqid, 
			string referencecode, 
			string requestxml, 
			DateTime createtime)
			 
		{
			_seqid = seqid;
			_referencecode = referencecode;
			_requestxml = requestxml;
			_responsexml = String.Empty;
			_createtime = createtime;
		}
		#endregion // End Constructor

		#region Public Properties
			
		public virtual long Seqid
		{
			get
			{ 
				return _seqid;
			}
			set
			{
				_seqid = value;
			}

		}
			
		public virtual string Referencecode
		{
			get
			{ 
				return _referencecode;
			}

			set	
			{	
				 
				
				_referencecode = value;
			}
		}
			
		public virtual string Requestxml
		{
			get
			{ 
				return _requestxml;
			}

			set	
			{	
				 
				
				_requestxml = value;
			}
		}
			
		public virtual string Responsexml
		{
			get
			{ 
				return _responsexml;
			}

			set	
			{	
				 
				
				_responsexml = value;
			}
		}
			
		public virtual DateTime Createtime
		{
			get
			{ 
				return _createtime;
			}
			set
			{
				_createtime = value;
			}

		}
			
				
		#endregion 

		 

		 
	}
}
