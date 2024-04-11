using System;
using System.Globalization;

namespace LouVuiDateCode
{
    public static class DateCodeGenerator
    {
        /// <summary>
        /// Generates a date code using rules from early 1980s.
        /// </summary>
        /// <param name="manufacturingYear">A manufacturing year.</param>
        /// <param name="manufacturingMonth">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        ///
        public static string GenerateEarly1980Code(uint manufacturingYear, uint manufacturingMonth)
        {
            if (manufacturingYear < 1980 || manufacturingYear > 1989 || manufacturingMonth < 1 || manufacturingMonth > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingYear));
            }

            string yearCode = (manufacturingYear % 100).ToString("00", CultureInfo.InvariantCulture);
            string monthCode = manufacturingMonth.ToString("0", CultureInfo.InvariantCulture);
            return $"{yearCode}{monthCode}";
        }

        /// <summary>
        /// Generates a date code using rules from early 1980s.
        /// </summary>
        /// <param name="manufacturingDate">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string GenerateEarly1980Code(DateTime manufacturingDate)
        {
            if (manufacturingDate.Year < 1980 || manufacturingDate.Year > 1989)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingDate));
            }

            string yearCode = (manufacturingDate.Year % 100).ToString("00", CultureInfo.InvariantCulture);
            string monthCode = manufacturingDate.Month.ToString("0", CultureInfo.InvariantCulture);

            return $"{yearCode}{monthCode}";
        }

        /// <summary>
        /// Generates a date code using rules from late 1980s.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year.</param>
        /// <param name="manufacturingMonth">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string GenerateLate1980Code(string factoryLocationCode, uint manufacturingYear, uint manufacturingMonth)
        {
            return GenerateLate1980CodePrivate(factoryLocationCode, manufacturingYear, manufacturingMonth);
        }

        /// <summary>
        /// Generates a date code using rules from late 1980s.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingDate">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string GenerateLate1980Code(string factoryLocationCode, DateTime manufacturingDate)
        {
            return GenerateLate1980CodePrivate(factoryLocationCode, (uint)manufacturingDate.Year, (uint)manufacturingDate.Month);
        }

        /// <summary>
        /// Generates a date code using rules from 1990 to 2006 period.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year.</param>
        /// <param name="manufacturingMonth">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string Generate1990Code(string factoryLocationCode, uint manufacturingYear, uint manufacturingMonth)
        {
            if (string.IsNullOrWhiteSpace(factoryLocationCode))
            {
                throw new ArgumentNullException(nameof(factoryLocationCode));
            }

            if (manufacturingYear < 1980 || manufacturingYear > 1989)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingYear));
            }

            var country = CountryParser.GetCountry(factoryLocationCode);
            if (country.Length == 0)
            {
                throw new ArgumentException("Factory location code is invalid.", nameof(factoryLocationCode));
            }

            var formattedLocationCode = factoryLocationCode.ToUpper(CultureInfo.InvariantCulture);
            string a1 = manufacturingMonth.ToString("D2", CultureInfo.InvariantCulture).Substring(1, 1);
            string a2 = manufacturingMonth.ToString("D2", CultureInfo.InvariantCulture).Substring(0, 1);
            var n = manufacturingYear.ToString("D4", CultureInfo.InvariantCulture).Substring(3, 1);
            var b = manufacturingYear.ToString("D4", CultureInfo.InvariantCulture).Substring(1, 1);
            return $"{formattedLocationCode}{a2}{b}{a1}{n}";
        }

        /// <summary>
        /// Generates a date code using rules from 1990 to 2006 period.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingDate">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string Generate1990Code(string factoryLocationCode, DateTime manufacturingDate)
        {
            if (string.IsNullOrWhiteSpace(factoryLocationCode))
            {
                throw new ArgumentNullException(nameof(factoryLocationCode));
            }

            if (manufacturingDate.Year >= 1989)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingDate));
            }

            string formattedLocationCode = factoryLocationCode.ToUpper(CultureInfo.InvariantCulture);
            string b2 = manufacturingDate.Year.ToString("D2", CultureInfo.InvariantCulture).Substring(3, 1);
            string b1 = manufacturingDate.Year.ToString("D2", CultureInfo.InvariantCulture).Substring(2, 1);
            string a1 = manufacturingDate.Month.ToString("D2", CultureInfo.InvariantCulture).Substring(0, 1);
            string a2 = manufacturingDate.Month.ToString("D2", CultureInfo.InvariantCulture).Substring(1, 1);

            return $"{formattedLocationCode}{a1}{b1}{a2}{b2}";
        }

        /// <summary>
        /// Generates a date code using rules from post 2007 period.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year.</param>
        /// <param name="manufacturingWeek">A manufacturing week number.</param>
        /// <returns>A generated date code.</returns>
        public static string Generate2007Code(string factoryLocationCode, uint manufacturingYear, uint manufacturingWeek)
        {
            var country = CountryParser.GetCountry(factoryLocationCode);

            if (country.Length == 0)
            {
                throw new ArgumentException("Factory location code is invalid.", nameof(factoryLocationCode));
            }

            if (manufacturingYear >= 2017)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingYear));
            }

            if (manufacturingWeek == 53)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingWeek));
            }

            if (manufacturingWeek >= 54 || manufacturingWeek <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingWeek));
            }

            string formattedLocationCode = factoryLocationCode.ToUpper(CultureInfo.InvariantCulture);
            string w1 = manufacturingWeek.ToString("D2", CultureInfo.InvariantCulture).Substring(0, 1);
            string w2 = manufacturingWeek.ToString("D2", CultureInfo.InvariantCulture).Substring(1, 1);

            string y1 = manufacturingYear.ToString("D2", CultureInfo.InvariantCulture).Substring(2, 1);
            string y2 = manufacturingYear.ToString("D2", CultureInfo.InvariantCulture).Substring(3, 1);

            return $"{formattedLocationCode}{w1}{y1}{w2}{y2}";
        }

        /// <summary>
        /// Generates a date code using rules from post 2007 period.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <param name="manufacturingDate">A manufacturing date.</param>
        /// <returns>A generated date code.</returns>
        public static string Generate2007Code(string factoryLocationCode, DateTime manufacturingDate)
        {
            var country = CountryParser.GetCountry(factoryLocationCode);
            var a = manufacturingDate.Year;
            if (country.Length == 0)
            {
                throw new ArgumentException("Factory location code is invalid.", nameof(factoryLocationCode));
            }

            if ((manufacturingDate.Year == 2016 && manufacturingDate.Day < 4) || (manufacturingDate.Year == 2017 && manufacturingDate.Day == 1))
            {
                a = manufacturingDate.Year - 1;
            }

            if (a == 2006)
            {
                throw new ArgumentOutOfRangeException(nameof(factoryLocationCode));
            }

            var calendar = CultureInfo.InvariantCulture.Calendar;
            int manufacturingWeek = calendar.GetWeekOfYear(manufacturingDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            string formattedLocationCode = factoryLocationCode.ToUpper(CultureInfo.InvariantCulture);
            string w1 = manufacturingWeek.ToString("D2", CultureInfo.InvariantCulture)[..1];
            string w2 = manufacturingWeek.ToString("D2", CultureInfo.InvariantCulture).Substring(1, 1);
            string y1 = a.ToString("D2", CultureInfo.InvariantCulture).Substring(2, 1);
            string y2 = a.ToString("D2", CultureInfo.InvariantCulture).Substring(3, 1);

            return $"{formattedLocationCode}{w1}{y1}{w2}{y2}";
        }

        public static string GenerateLate1980CodePrivate(string factoryLocationCode, uint manufacturingYear, uint manufacturingMonth)
        {
            if (string.IsNullOrWhiteSpace(factoryLocationCode))
            {
                throw new ArgumentNullException(nameof(factoryLocationCode));
            }

            if (manufacturingYear < 1980 || manufacturingYear > 1989 || manufacturingMonth < 1 || manufacturingMonth > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingMonth));
            }

            var country = CountryParser.GetCountry(factoryLocationCode);
            if (country.Length == 0)
            {
                throw new ArgumentException("Factory location code is invalid.", nameof(factoryLocationCode));
            }

            string formattedLocationCode = factoryLocationCode.ToUpper(CultureInfo.InvariantCulture);
            string yearCode = (manufacturingYear % 100).ToString("00", CultureInfo.InvariantCulture);
            string monthCode = manufacturingMonth.ToString("0", CultureInfo.InvariantCulture);

            return $"{yearCode}{monthCode}{formattedLocationCode}";
        }
    }
}
