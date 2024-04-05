namespace Exceptions
{
    public static class ThrowingArgumentNull
    {
        public static bool CheckParameterAndThrowException1(object o)
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            return true;
        }

        public static bool CheckParametersAndThrowException2(object o1, object o2)
        {
            if (o1 == null)
            {
                throw new ArgumentNullException(nameof(o1), "argument is not null");
            }

            if (o2 == null)
            {
                throw new ArgumentNullException(nameof(o2), "argument is not null");
            }

            return true;
        }

        public static int CheckParametersAndThrowException3(int[] integers, long[] longs, float[] floats)
        {
            if (integers == null)
            {
                throw new ArgumentNullException(nameof(integers), "argument is not null");
            }

            if (longs == null)
            {
                throw new ArgumentNullException(nameof(longs), "argument is not null");
            }

            if (floats == null)
            {
                throw new ArgumentNullException(nameof(floats), "argument is not null");
            }

            return integers.Length + longs.Length + floats.Length;
        }

        public static int CheckParameterAndThrowException4(string s)
        {
            if (s is null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return s.Length;
        }

        public static int CheckParametersAndThrowException5(string s1, string s2)
        {
            if (s1 is null)
            {
                throw new ArgumentNullException(nameof(s1), "argument is not null");
            }

            if (s2 is null)
            {
                throw new ArgumentNullException(nameof(s2), "argument is not nulls");
            }

            return s1.Length + s2.Length;
        }

        public static int CheckParametersAndThrowException6(string s, int[] integers, string[] strings)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s), "argument is not nulls");
            }

            if (integers == null)
            {
                throw new ArgumentNullException(nameof(integers), "argument is not nulls");
            }

            if (strings == null)
            {
                throw new ArgumentNullException(nameof(strings), "argument is not nulls");
            }

            return s.Length + integers.Length + strings.Length;
        }

        public static int CheckParameterAndThrowException7(int[] integers)
        {
            int integersCount;

            integersCount = (integers ?? throw new ArgumentNullException(nameof(integers))).Length;

            return integersCount;
        }

        public static int CheckParametersAndThrowException8(int[] integers, string s)
        {
            int integersCount, stringLength;

            integersCount = (integers ?? throw new ArgumentNullException(nameof(integers))).Length;
            stringLength = (s ?? throw new ArgumentNullException(nameof(s))).Length;

            return integersCount + stringLength;
        }

        public static int CheckParametersAndThrowException9(float[] floats, string s1, double[] doubles, string s2)
        {
            int floatsCount, s1Length, doublesCount, s2Length;

            floatsCount = (floats ?? throw new ArgumentNullException(nameof(floats))).Length;
            doublesCount = (doubles ?? throw new ArgumentNullException(nameof(doubles))).Length;
            s1Length = (s1 ?? throw new ArgumentNullException(nameof(s1))).Length;
            s2Length = (s2 ?? throw new ArgumentNullException(nameof(s2))).Length;

            return floatsCount + s1Length + doublesCount + s2Length;
        }
    }
}
