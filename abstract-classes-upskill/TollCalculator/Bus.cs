using System;

namespace TollCalculator
{
    /// <summary>
    /// Represents a bus class.
    /// </summary>
    public class Bus : Vehicle
    {
        private int capacity;
        private int passengers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bus"/> class with the specified the base toll, capacity and passengers.
        /// </summary>
        /// <param name="basicToll">A toll of this <see cref="Bus"/> class.</param>
        /// <param name="capacity">A capacity of this <see cref="Bus"/> class.</param>
        /// <param name="passengers">A passengers of this <see cref="Bus"/> class.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="basicToll"/>less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/>less than or equals zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="passengers"/>less than zero.</exception>
        public Bus(decimal basicToll, int capacity, int passengers)
        {
            if (basicToll < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(basicToll), "Basic toll cannot be negative.");
            }

            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");
            }

            if (passengers < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(passengers), "Passengers count cannot be negative.");
            }

            this.BaseToll = basicToll;
            this.capacity = capacity;
            this.passengers = passengers;
        }

        /// <summary>
        /// Gets or sets the capacity of this <see cref="Bus"/> class.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/>less than zero.</exception>
        public int Capacity
        {
            get
            {
                return this.capacity;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Capacity cannot be negative.");
                }

                this.capacity = value;
            }
        }

        /// <summary>
        /// Gets or sets the passengers of this <see cref="Bus"/> class.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/>less than zero.</exception>
        public int Passengers
        {
            get
            {
                return this.passengers;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Passengers count cannot be negative.");
                }

                this.passengers = value;
            }
        }

        /// <summary>
        /// Calculates the base toll that relies only on the bus type.
        /// ----------------------------------------------
        /// Passenger filling in %      Extra or discount
        /// ----------------------------------------------
        /// less than 50%               extra $2.00
        /// more than 90%               $1.00 discount.
        /// </summary>
        /// <returns>The base toll of bus.</returns>
        protected override decimal Calculate()
        {
            decimal fillingPercentage = (decimal)this.passengers / this.capacity * 100;

            if (fillingPercentage < 50)
            {
                return this.BaseToll + 2.00M;
            }
            else if (fillingPercentage > 90)
            {
                return this.BaseToll - 1.00M;
            }

            return this.BaseToll;
        }
    }
}
