using System;


namespace com.etak.core.model.utils
{
    static class Comparators
    {
        public static bool SafeStringComparer(String a, String b)
        {
            if (String.IsNullOrWhiteSpace(a))
                a = String.Empty;

            if (String.IsNullOrWhiteSpace(b))
                b = String.Empty;

            return a == b;
        }

        public static int SafeHashCode(Object o)
        {
            if (o == null)
                return 0;

            return o.GetHashCode();
        }
    }
}
