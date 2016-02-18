namespace com.etak.core.jms.nmstracing
{
    /// <summary>
    /// Helper class to enabling tracing in NMS Adapter
    /// </summary>
    static public class TracerEnabler
    {
        /// <summary>
        /// Helper method to enable tracing in NMS
        /// </summary>
        static public void EnableTracerAdapter()
        {
            NMSTraceAdapter tracer = new NMSTraceAdapter();
            Apache.NMS.Tracer.Trace = tracer;
        }

        /// <summary>
        /// Enables tracing based on reflection, shows the invoker on the tracer
        /// at expense of CPU by the use of reflection.
        /// </summary>
        static public void EnableReflectionTracerAdapter()
        {
            NMSReflectionTraceAdapter tracer = new NMSReflectionTraceAdapter();
            Apache.NMS.Tracer.Trace = tracer;
        }
    }
}
