using System;
using System.Globalization;

namespace LouVuiDateCode
{
    public static class DateCodeParser
    {
        /// <summary>
        /// Parses a date code and returns a <see cref="manufacturingYear"/> and <see cref="manufacturingMonth"/>.
        /// </summary>
        /// <param name="dateCode">A three or four number date code.</param>
        /// <param name="manufacturingYear">A manufacturing year to return.</param>
        /// <param name="manufacturingMonth">A manufacturing month to return.</param>
        public static void ParseEarly1980Code(string dateCode, out uint manufacturingYear, out uint manufacturingMonth)
        {
            if (dateCode is null)
            {
                throw new ArgumentNullException(nameof(dateCode));
            }

            if (dateCode.Length == 0)
            {
                throw new ArgumentNullException(nameof(dateCode));
            }

            if (!uint.TryParse($"19{dateCode[..2]}", out manufacturingYear) ||
                !uint.TryParse(dateCode[2..], out manufacturingMonth) ||
                manufacturingYear < 1980 || manufacturingYear > 1999 ||
                manufacturingMonth < 1 || manufacturingMonth > 12)
            {
                throw new ArgumentException("Invalid manufacturing year or month.");
            }
        }

        /// <summary>
        /// Parses a date code and returns a <paramref name="factoryLocationCode"/>, <paramref name="manufacturingYear"/>, <paramref name="manufacturingMonth"/> and <paramref name="factoryLocationCountry"/> array.
        /// </summary>
        /// <param name="dateCode">A three or four number date code.</param>
        /// <param name="factoryLocationCountry">A factory location country array.</param>
        /// <param name="factoryLocationCode">A factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year to return.</param>
        /// <param name="manufacturingMonth">A manufacturing month to return.</param>
        public static void ParseLate1980Code(string dateCode, out Country[] factoryLocationCountry, out string factoryLocationCode, out uint manufacturingYear, out uint manufacturingMonth)
        {
            if (string.IsNullOrWhiteSpace(dateCode))
            {
                throw new ArgumentNullException(nameof(dateCode));
            }

            factoryLocationCountry = CountryParser.GetCountry(dateCode[^2..]);
            factoryLocationCode = dateCode[^2..];

            if (!uint.TryParse($"19{dateCode[..2]}", out manufacturingYear) ||
               !uint.TryParse(dateCode[2..^2], out manufacturingMonth) || factoryLocationCountry.Length == 0 ||
               manufacturingYear < 1980 || manufacturingYear > 1989 ||
               manufacturingMonth < 1 || manufacturingMonth > 12)
            {
                throw new ArgumentException("Invalid manufacturing year or month.");
            }
        }

        /// <summary>
        /// Parses a date code and returns a <paramref name="factoryLocationCode"/>, <paramref name="manufacturingYear"/>, <paramref name="manufacturingMonth"/> and <paramref name="factoryLocationCountry"/> array.
        /// </summary>
        /// <param name="dateCode">A six number date code.</param>
        /// <param name="factoryLocationCountry">A factory location country array.</param>
        /// <param name="factoryLocationCode">A factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year to return.</param>
        /// <param name="manufacturingMonth">A manufacturing month to return.</param>
        public static void Parse1990Code(string dateCode, out Country[] factoryLocationCountry, out string factoryLocationCode, out uint manufacturingYear, out uint manufacturingMonth)
        {
            if (string.IsNullOrWhiteSpace(dateCode))
            {
                throw new ArgumentNullException(nameof(dateCode));
            }

            factoryLocationCountry = CountryParser.GetCountry(dateCode[..2]);
            factoryLocationCode = dateCode[..2];
            if (dateCode[3] == '9')
            {
                manufacturingYear = uint.Parse($"19{dateCode[3]}{dateCode[5]}", CultureInfo.InvariantCulture);
            }
            else
            {
                manufacturingYear = uint.Parse($"20{dateCode[3]}{dateCode[^1]}", CultureInfo.InvariantCulture);
            }

            if (!uint.TryParse($"{dateCode[2]}{dateCode[4]}", out manufacturingMonth) || manufacturingYear < 1980 || manufacturingYear > 1989 ||

               manufacturingMonth < 1 || manufacturingMonth > 12)
            {
                throw new ArgumentException("Invalid manufacturing year or month.");
            }
        }

        /// <summary>
        /// Parses a date code and returns a <paramref name="factoryLocationCode"/>, <paramref name="manufacturingYear"/>, <paramref name="manufacturingWeek"/> and <paramref name="factoryLocationCountry"/> array.
        /// </summary>
        /// <param name="dateCode">A six number date code.</param>
        /// <param name="factoryLocationCountry">A factory location country array.</param>
        /// <param name="factoryLocationCode">A factory location code.</param>
        /// <param name="manufacturingYear">A manufacturing year to return.</param>
        /// <param name="manufacturingWeek">A manufacturing week to return.</param>
        public static void Parse2007Code(string dateCode, out Country[] factoryLocationCountry, out string factoryLocationCode, out uint manufacturingYear, out uint manufacturingWeek)
        {
            if (string.IsNullOrWhiteSpace(dateCode))
            {
                throw new ArgumentNullException(nameof(dateCode));
            }

            factoryLocationCountry = CountryParser.GetCountry(dateCode[..2]);
            factoryLocationCode = dateCode[..2];
            if (!uint.TryParse($"20{dateCode[3]}{dateCode[5]}", out manufacturingYear) || factoryLocationCountry.Length == 0 ||
             (!uint.TryParse($"{dateCode[2]}{dateCode[4]}", out manufacturingWeek)))
            {
                throw new ArgumentException("Invalid manufacturing year or month.");
            }
        }
    }
}
