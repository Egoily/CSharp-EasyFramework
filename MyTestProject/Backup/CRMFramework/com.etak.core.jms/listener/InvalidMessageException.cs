using System;


namespace com.etak.core.jms.listener
{
    /// <summary>
    /// This kind of exception is to instruct JMS listener 
    /// that the message had an invalid format and it's impossible to process it.
    /// </summary>
    public class InvalidMessageException : Exception
    {
        
    }
}
