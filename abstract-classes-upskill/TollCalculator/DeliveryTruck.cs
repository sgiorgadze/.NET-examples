using System;

namespace TollCalculator
{
    /// <summary>
    /// Represents a delivery truck class.
    /// </summary>
    public class DeliveryTruck : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryTruck"/> class with
        /// the specified <paramref name="baseToll"/> and <paramref name="grossWeightClass"/>.
        /// </summary>
        private int grossWeightClass;

        /// <param name="baseToll">A baseToll of this <see cref="DeliveryTruck"/> class.</param>
        /// <param name="grossWeightClass">A grossWeightClass of this <see cref="DeliveryTruck"/> class.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="baseToll"/>less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="grossWeightClass"/>less than zero.</exception>
        public DeliveryTruck(decimal baseToll, int grossWeightClass)
        {
            if (baseToll <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baseToll), "Base toll cannot be negative.");
            }

            if (grossWeightClass < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(grossWeightClass), "Gross weight class cannot be negative.");
            }

            this.BaseToll = baseToll;
            this.grossWeightClass = grossWeightClass;
        }

        /// <summary>
        /// Gets or sets a gross weight class.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/>less than zero.</exception>
        public int GrossWeightClass
        {
            get
            {
                return this.grossWeightClass;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Gross weight class cannot be negative.");
                }

                this.grossWeightClass = value;
            }
        }

        /// <summary>
        /// Calculates the base toll that relies only on the delivery truck type.
        /// ----------------------------------------------
        /// Weight class        Extra or discount
        /// ----------------------------------------------
        /// over 5000 lbs       extra $5.00
        /// under 3000 lbs      $2.00 discount.
        /// </summary>
        /// <returns>The base toll of delivery truck.</returns>
        protected override decimal Calculate()
        {
            if (this.grossWeightClass > 5000)
            {
                return this.BaseToll + 5.0M;
            }
            else if (this.grossWeightClass < 3000)
            {
                return this.BaseToll - 2.0M;
            }

            return this.BaseToll;
        }
    }
}
