using System;
using GenericMethods.Interfaces;

namespace DoubleTransformer
{
    /// <summary>
    /// Transformer class for double.
    /// </summary>
    public class GetIeee754Format : ITransformer<double, string>
    {
        /// <summary>
        /// Transform double value to IEEE754 format <see cref="https://www.wikiwand.com/en/IEEE_754"/> in the string form.
        /// </summary>
        /// <param name="obj">The double value.</param>
        /// <returns>The IEEE754 format in the string form.</returns>
        public string Transform(double obj)
        {
            long bits = BitConverter.DoubleToInt64Bits(obj);
            string binaryRepresentation = Convert.ToString(bits, 2);
            while (binaryRepresentation.Length < 64)
            {
                binaryRepresentation = "0" + binaryRepresentation;
            }

            string signBit = binaryRepresentation.Substring(0, 1);
            string exponentBits = binaryRepresentation.Substring(1, 11);
            string fractionBits = binaryRepresentation.Substring(12);

            string ieee754Format = $"{signBit}{exponentBits}{fractionBits}";

            return ieee754Format;
        }
    }
}
