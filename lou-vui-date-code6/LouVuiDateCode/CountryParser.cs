using System;
using System.Globalization;

namespace LouVuiDateCode
{
    public static class CountryParser
    {
        /// <summary>
        /// Gets a an array of <see cref="Country"/> enumeration values for a specified factory location code. One location code can belong to many countries.
        /// </summary>
        /// <param name="factoryLocationCode">A two-letter factory location code.</param>
        /// <returns>An array of <see cref="Country"/> enumeration values.</returns>
        public static Country[] GetCountry(string factoryLocationCode)
        {
            switch (factoryLocationCode?.ToUpper(CultureInfo.InvariantCulture))
            {
                case "A0":
                case "A1":
                case "A2":
                case "AA":
                case "AH":
                case "AN":
                case "AR":
                case "AS":
                case "BA":
                case "BJ":
                case "BU":
                case "DR":
                case "DU":
                case "DT":
                case "CO":
                case "CT":
                case "CX":
                case "ET":
                case "MB":
                case "MI":
                case "NO":
                case "RA":
                case "RI":
                case "SF":
                case "SL":
                case "SN":
                case "SP":
                case "SR":
                case "TJ":
                case "TH":
                case "TR":
                case "TS":
                case "VI":
                case "VX":
                    return new Country[] { Country.France };

                case "FL":
                case "SD":
                    return new Country[] { Country.France, Country.USA };

                case "LW":
                    return new Country[] { Country.France, Country.Spain };

                case "LP":
                case "OL":
                    return new Country[] { Country.Germany };

                case "BC":
                case "BO":
                case "CE":
                case "FO":
                case "MA":
                case "OB":
                case "RC":
                case "RE":
                case "SA":
                case "TD":
                    return new Country[] { Country.Italy };

                case "CA":
                case "LO":
                case "LB":
                case "LM":
                case "GI":
                    return new Country[] { Country.Spain };

                case "DI":
                case "FA":
                    return new Country[] { Country.Switzerland };

                case "FC":
                case "FH":
                case "LA":
                case "OS":
                    return new Country[] { Country.USA };

                case "":
                    throw new ArgumentNullException(nameof(factoryLocationCode));
                case null:
                    throw new ArgumentNullException(nameof(factoryLocationCode));

                default:
                    return Array.Empty<Country>();
            }
        }
    }
}
