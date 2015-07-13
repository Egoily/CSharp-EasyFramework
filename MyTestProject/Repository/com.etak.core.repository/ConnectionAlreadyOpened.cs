using System;

namespace com.etak.core.repository
{
    /// <summary>
    /// Exception used when a threads attempts to open a second connection.
    /// </summary>
    public class ConnectionAlreadyOpened : Exception
    {
    }
}