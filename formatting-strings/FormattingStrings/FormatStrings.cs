using System.Globalization;

namespace FormattingStrings
{
    public static class FormatStrings
    {
        public static string Format1(int number1, int number2, float number3, double number4)
        {
            string formattedString = $"{number1} {number3.ToString(CultureInfo.InvariantCulture)} {number2} {number4.ToString(CultureInfo.InvariantCulture)}";
            return formattedString;
        }

        public static string Format2(string str1, double number1, string str2, float number2, int number3)
        {
            string formattedString = $"{str2} {number2.ToString(CultureInfo.InvariantCulture)} {number1.ToString(CultureInfo.InvariantCulture)} {number3} {str1}";
            return formattedString;
        }

        public static string Format3(string str1, float number1, double number2, int number3)
        {
            var a = string.Format(CultureInfo.InvariantCulture, "¤{0:N2}", number2);
            var b = number1 * 100;
            string formattedPercentage = b.ToString("0", CultureInfo.InvariantCulture) + " %";
            string formattedString = $"{number3}, {str1}, {formattedPercentage}, {a}";
            return formattedString;
        }

        public static string Format4(int number1, int number2, long number3, string str1, string str2)
        {
            string hexString = number1.ToString("X", CultureInfo.InvariantCulture);
            string formattedString = $"{number2},{str1},{hexString},{number3},{str2}";
            return formattedString;
        }

        public static string Format5(string str1, double number1, int number2, int number3)
        {
            string daysFormatted = number2.ToString("D3", CultureInfo.InvariantCulture);
            var c = string.Format(CultureInfo.InvariantCulture, "{0:F2}", number1);
            string formattedString = $"{{ \"id\": {number3}, \"name\": \"{str1}\", \"deposit\": {c}, \"days\": {daysFormatted} }}";
            return formattedString;
        }

        public static string Format6(int number1, double number2, string str1, string str2, string str3)
        {
            string formattedNumber = number1.ToString("X4", CultureInfo.InvariantCulture);
            var c = string.Format(CultureInfo.InvariantCulture, "{0:F2}", number2);
            string formattedString = $"{{ \"parameter\": \"{str3}\", \"code\": {{ \"type\": \"{str2}\", \"label\": \"{formattedNumber}\" }}, \"value\": {{ \"data\": \"{c}\", \"units\": \"{str1}\" }}}}";
            return formattedString;
        }

        public static string Format7(string str1, string str2, float number1, float number2, int number3)
        {
            string formattedString = $"|{number3,6}|{str1,9}|{number1.ToString(CultureInfo.InvariantCulture),8}|{str2,9}|{number2.ToString(CultureInfo.InvariantCulture),6}|";
            return formattedString;
        }

        public static string Format8(string str1, string str2, string str3, double number1, double number2, double number3)
        {
            string formattedString = $"| {str3,8} | {number1.ToString(CultureInfo.InvariantCulture),7} | {number2.ToString(CultureInfo.InvariantCulture),10} | {number3.ToString(CultureInfo.InvariantCulture),11} |{str2,11} | {str1,8} |";
            return formattedString;
        }

        public static string Format9(string str1, string str2, float number1, float number2, float number3)
        {
            var a = Math.Round(number3 * 100, 2).ToString(CultureInfo.InvariantCulture);
            var b = string.Format(CultureInfo.InvariantCulture, "¤{0:N2}", number2);
            var c = Math.Round(number1, 4).ToString("#,##0.0000", CultureInfo.InvariantCulture);
            string formattedString = $"| {a,8} % | {str1,-12} | {str2,-14} | {b,12} | {c,-19}  |";
            return formattedString;
        }

        public static string Format10(decimal number1, decimal number2, double number3, string str1, string str2, string str3)
        {
            var a = string.Format(CultureInfo.InvariantCulture, "{0:F0}", number1);
            var b = number3.ToString("#,##0.00", CultureInfo.InvariantCulture);
            var c = string.Format(CultureInfo.InvariantCulture, "¤{0:N4}", number2);
            string formattedString = $"| {str1,-9}| {a,-13}| {b,12} | {str3,15} | {str2,11} | {c,-19}  |";
            return formattedString;
        }
    }
}
