using System;
using System.Runtime.ConstrainedExecution;

namespace TollCalculator
{
    /// <summary>
    /// Represents a vehicle base class in pricing and toll calculation system.
    /// </summary>
    public abstract class Vehicle
    {
        private decimal baseToll;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        ///
        protected Vehicle()
        {
            this.baseToll = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class with the specified base toll.
        /// </summary>
        /// <param name="baseToll">A base toll for the <see cref="Vehicle"/> class.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="baseToll"/> is less than zero.</exception>
        protected Vehicle(decimal baseToll)
        {
            if (baseToll < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baseToll), "Base toll cannot be negative.");
            }

            this.baseToll = baseToll;
        }

        /// <summary>
        /// Gets or sets a base toll for the vehicle.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than zero.</exception>
        public decimal BaseToll
        {
            get => this.baseToll;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Base toll cannot be negative.");
                }

                this.baseToll = value;
            }
        }

        /// <summary>
        /// Calculates the final toll for the vehicle that adjusts for time peaks and traffic direction.
        /// </summary>
        /// <param name="timeOfToll">A time of toll.</param>
        /// <param name="direction">A traffic direction.</param>
        /// <returns>The final toll for the vehicle that adjusts for time peaks and traffic direction.</returns>
        public decimal CalculateToll(DateTime timeOfToll, TrafficDirection direction)
        {
            decimal weightFactor = PeakTimePremium(timeOfToll, direction);
            return this.Calculate() * weightFactor;
        }

        /// <summary>
        /// Calculates the base toll that relies only on the vehicle type.
        /// </summary>
        /// <returns>The base toll of vehicle.</returns>
        protected abstract decimal Calculate();

        /// <summary>
        /// Calculates a weighting factor for the base toll, taking into account time peaks and direction of travel.
        /// ----------------------------------------------------------
        /// Day         Time            Direction       Weight factor
        /// ----------------------------------------------------------
        /// Weekday     morning rush    inbound             x 2.00
        /// Weekday     morning rush    outbound            x 1.00
        /// Weekday     daytime         inbound             x 1.50
        /// Weekday     daytime         outbound            x 1.50
        /// Weekday     evening rush    inbound             x 1.00
        /// Weekday     evening rush    outbound            x 2.00
        /// Weekday     overnight       inbound             x 0.75
        /// Weekday     overnight       outbound            x 0.75
        /// Weekend     morning rush    inbound             x 1.00
        /// Weekend     morning rush    outbound            x 1.00
        /// Weekend     daytime         inbound             x 1.00
        /// Weekend     daytime         outbound            x 1.00
        /// Weekend     evening rush    inbound             x 1.00
        /// Weekend     evening rush    outbound            x 1.00
        /// Weekend     overnight       inbound             x 1.00
        /// Weekend     overnight       outbound            x 1.00.
        /// </summary>
        /// <param name="timeOfToll">A time of toll.</param>
        /// <param name="direction">A traffic direction.</param>
        /// <returns>A weight factor that adjusts for time peaks and traffic direction.</returns>
        private static decimal PeakTimePremium(DateTime timeOfToll, TrafficDirection direction)
        {
            decimal weightFactor = 1.0M;
            if (IsWeekDay(timeOfToll))
            {
                TimeBand timeBand = GetTimeBand(timeOfToll);

                switch (timeBand)
                {
                    case TimeBand.MorningRush:
                        weightFactor = direction == TrafficDirection.Inbound ? 2.00M : 1.00M;
                        break;

                    case TimeBand.Daytime:
                        weightFactor = 1.50M;
                        break;

                    case TimeBand.EveningRush:
                        weightFactor = direction == TrafficDirection.Inbound ? 1.00M : 2.00M;
                        break;

                    case TimeBand.Overnight:

                        weightFactor = 0.75M;
                        if (direction == TrafficDirection.Outbound)
                        {
                            weightFactor = 0.75M;
                        }

                        break;
                }
            }

            return weightFactor;
        }

        /// <summary>
        /// Defines whether a DateTime represents a weekend or a weekday.
        /// </summary>
        /// <param name="timeOfToll">The time when the toll was collected.</param>
        /// <returns>true if <paramref name="timeOfToll"/> is weekday; false otherwise.</returns>
        private static bool IsWeekDay(DateTime timeOfToll)
        {
            return timeOfToll.DayOfWeek != DayOfWeek.Saturday && timeOfToll.DayOfWeek != DayOfWeek.Sunday;
        }

        /// <summary>
        /// Categorizes the time into the time bands.
        /// </summary>
        /// <param name="timeOfToll">The time when the toll was collected.</param>
        /// <returns><see cref="TimeBand"/> instance.</returns>
        private static TimeBand GetTimeBand(DateTime timeOfToll) =>
    timeOfToll.Hour switch
    {
        < 6 or > 19 => TimeBand.Overnight,
        < 10 => TimeBand.MorningRush,
        < 16 => TimeBand.Daytime,
        _ => TimeBand.EveningRush,
    };
    }
}
