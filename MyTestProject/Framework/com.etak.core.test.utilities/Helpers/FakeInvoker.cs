using com.etak.core.operation;

namespace com.etak.core.test.utilities.Helpers
{
    /// <summary>
    /// Fakeinvoker to trace in Bizop
    /// </summary>
    public static class FakeInvoker
    {
        /// <summary>
        /// Set up enviroment
        /// </summary>
        /// <returns></returns>
        public static RequestInvokationEnvironment FakeInvokationEnvironment()
        {
            return new RequestInvokationEnvironment()
            {
                DestinationIp = "127.0.0.1",
                Invoker = null,
                ProxyIp = "127.0.0.1",
                ServingUrl = "dummyURL",
                SessionId = "sessionId",
                SourceIp = "127.0.0.1",
            };
        }
    }
}
