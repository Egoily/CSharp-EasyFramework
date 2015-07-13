using System;
using System.Configuration;
using NHibernate.Id;

namespace com.etak.core.repository.NHibernate.IDGeneration
{
    /// <summary>
    /// Id generator for Nhibernate based on a configured middle fix.
    /// </summary>
    public class MiddlefixIdGenerator : IIdentifierGenerator, IConfigurable
    {
        private readonly DateTime _unixTimeZero = new DateTime(1970, 1, 1);
        private UInt16 _middleFix;
        private UInt16 _counter;

        /// <summary>
        /// Implementaion of IIdentifierGenerator, generates a new ID everytime we attempt to save
        /// an object
        /// </summary>
        /// <param name="session">the current session that is trying to create the object</param>
        /// <param name="obj">the object that is being persisted and needs ID</param>
        /// <returns>the Id generated</returns>
        public object Generate(global::NHibernate.Engine.ISessionImplementor session, object obj)
        {
            Int64 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(_unixTimeZero)).TotalSeconds;
            //This causes a known overflow on 2039, from that point onwards
            //Ids generated will be enegatives.
#pragma warning disable 0675
            Int64 id = (unixTimestamp << 32) | (_middleFix << 16) | (_counter++);
#pragma warning restore 0675
            return id;
        }

        /// <summary>
        /// Implementation of IConfigurable interface
        /// </summary>
        /// <param name="type">the type of the ID to confgiure</param>
        /// <param name="parms">
        /// the set of parameters configured in the Id generation in the Nhibernate mapping
        /// </param>
        /// <param name="dialect">the current dialect for the DB</param>
        public void Configure(global::NHibernate.Type.IType type, System.Collections.Generic.IDictionary<string, string> parms, global::NHibernate.Dialect.Dialect dialect)
        {
            String strMiddleFix = ConfigurationManager.AppSettings["dbmiddlefix"];
            if (String.IsNullOrWhiteSpace(strMiddleFix))
                throw new Exception("There's a entity declared with Id generation MiddlefixIdGenerator, but the app setting dbmiddlefix is not configured");

            _middleFix = UInt16.Parse(strMiddleFix);
        }
    }
}