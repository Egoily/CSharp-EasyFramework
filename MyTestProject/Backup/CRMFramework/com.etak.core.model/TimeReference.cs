using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// Abstract class to allow mutltiple kinds of dates specification
    /// </summary>
    [DataContract(Namespace = "http://com.etak.frontend")]
    [Serializable]
    public abstract class TimeReference
    {
        /// <summary>
        /// Method to be implemented returning the actual date for the reference. 
        /// </summary>
        /// <returns></returns>
        public abstract DateTime GetLocalTime();
    }

    /// <summary>
    /// Class to allow the time specification for an absolute value ie: '2015-06-07 08:08:08'
    /// </summary>
    [DataContract(Namespace = "http://com.etak.frontend")]
    [Serializable]
    public class AbsoluteTime : TimeReference
    {
        /// <summary>
        /// The target date in the remote system
        /// </summary>
        [DataMember] public DateTime TargetTime { get; set; }

        /// <summary>
        /// Gets the actual time of the date reflected by this AbsoluteTime
        /// </summary>
        /// <returns></returns>
        public override DateTime GetLocalTime()
        {
            return TargetTime;
        }
    }

    ///<summary>
    /// Class to allow the time specification as an offset in seconds in the system receiving the request
    ///</summary>
    [DataContract(Namespace = "http://com.etak.frontend")]
    [Serializable]
    public class SecondsOffset : TimeReference
    {
        /// <summary>
        /// The amount of seconds offset in the receiving system
        /// </summary>
        [DataMember] public Int32 Offset { get; set; }

        /// <summary>
        /// The absolute time in which this offset is stranslated to 
        /// </summary>
        /// <returns></returns>
        public override DateTime GetLocalTime()
        {
            return DateTime.Now.AddSeconds(Offset);
        }
    }
}