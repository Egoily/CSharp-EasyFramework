using System;

namespace com.etak.core.repository
{
    /// <summary>
    /// Exception thrown when thrown when tried to get the connection without an underlaying DB connection
    /// </summary>
    public class ConnectionNotOpened : Exception
    {
    }
}