using System.Globalization;

namespace FormattingStrings
{
    public static class NumbersToString
    {
        public static string NumberToString1(float number)
        {
            return number.ToString("F4", NumberFormatInfo.InvariantInfo);
        }

        public static string NumberToString2(double number)
        {
            return number.ToString("F2", NumberFormatInfo.InvariantInfo);
        }

        public static string NumberToString3(double number)
        {
            return number.ToString("N4", NumberFormatInfo.InvariantInfo);
        }

        public static string NumberToString4(decimal number)
        {
            return number.ToString("N8", NumberFormatInfo.InvariantInfo);
        }

        public static string NumberToString5(float number)
        {
            return number.ToString("C1", CultureInfo.InvariantCulture);
        }

        public static string NumberToString6(decimal number)
        {
            return number.ToString("C3", CultureInfo.InvariantCulture);
        }

        public static string NumberToString7(double number)
        {
            var b = number * 100;
            var a = b.ToString("N2", CultureInfo.InvariantCulture);
            return $"{a} %";
        }

        public static string NumberToString8(float number)
        {
            return $"{number.ToString("P5", CultureInfo.InvariantCulture)}";
        }

        public static string NumberToString9(float number)
        {
            return number.ToString("G6", CultureInfo.InvariantCulture);
        }

        public static string NumberToString10(double number)
        {
            if (Math.Abs(number) >= 0.1 && Math.Abs(number) < 1000)
            {
                return number.ToString("F1", CultureInfo.InvariantCulture);
            }
            else
            {
                return number.ToString("0.##E+00", CultureInfo.InvariantCulture);
            }
        }
    }
}
