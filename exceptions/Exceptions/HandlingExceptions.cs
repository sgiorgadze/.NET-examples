﻿namespace Exceptions
{
    public static class HandlingExceptions
    {
        public static bool CatchArgumentOutOfRangeException1(int i, Func<int, bool> foo)
        {
            try
            {
                return foo(i);
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        public static void CatchArgumentOutOfRangeException2(int i, object o, string s, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                DoSomething(-1);
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            {
                Console.WriteLine("Exception is thrown");
            }
        }

        public static string CatchArgumentNullException3(object o, Func<object, string> foo)
        {
            try
            {
                return foo(o);
            }
            catch (ArgumentNullException)
            {
                return "P456";
            }
        }

        //public static string CatchArgumentNullException4(int i, object o, string s, out string errorMessage)
        //{
        //    errorMessage = null;

        //    try
        //    {
        //        return DoSomething(i);
        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        errorMessage = e.Message;
        //        return "A732";
        //    }
        //}

        public static int CatchArgumentException5(int[] integers, Func<int[], int> foo)
        {
            try
            {
                return foo(integers);
            }
            catch (ArgumentException)
            {
                return 0;
            }
        }

        //public static string CatchArgumentException6(int i, object o, string s, out string errorMessage)
        //{
        //    errorMessage = null;
        //    try
        //    {
        //        return DoSomething(i, o, s);
        //    }
        //    catch (ArgumentException e)
        //    {
        //        errorMessage = e.Message;
        //        return "D948";
        //    }
        //}

        //public static string CatchArgumentException7(int i, object o, string s, out string errorMessage)
        //{
        //    errorMessage = null;

        //    try
        //    {
        //        return DoSomething(i, o, s);
        //    }
        //    catch (ArgumentOutOfRangeException e)
        //    {
        //        errorMessage = e.Message;
        //        return "Z029";
        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        errorMessage = e.Message;
        //        return "W694";
        //    }
        //    catch (ArgumentException e)
        //    {
        //        errorMessage = e.Message;
        //        return "J954";
        //    }
        //}

        public static void DoSomething(int i)
        {
            if (i < 0)
            {
                throw new ArgumentException("wrong argument");
            }
        }
    }
}
