using N2YO.Runtime.Domain.Common;
using N2YO.Runtime.Domain.Responses;

namespace N2YO.Runtime.Domain.Factories
{
    public class TLEResponseFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TLEResponse"/> class using the provided satellite information and TLE data.
        /// </summary>
        /// <param name="info">The satellite information object containing details such as ID, name, and transaction count.</param>
        /// <param name="line1">The first line of the two-line element (TLE) data representing orbital parameters.</param>
        /// <param name="line2">The second line of the two-line element (TLE) data representing orbital parameters.</param>
        /// <returns>A <see cref="TLEResponse"/> object containing the provided satellite information and TLE data.</returns>
        public static TLEResponse Create(SatInfo info, string line1, string line2)
        {
            return new()
            {
                Info = info,
                Tle = $"{line1}\r\n{line2}"
            };
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TLEResponse"/> class using the provided satellite information and the concatenated TLE data.
        /// </summary>
        /// <param name="info">The satellite information object containing details such as ID, name, and transaction count.</param>
        /// <param name="lines">The concatenated two-line element (TLE) data representing orbital parameters. (Line 1 and 2 should be seperated by \r\n)</param>
        /// <returns>A <see cref="TLEResponse"/> object containing the provided satellite information and concatenated TLE data.</returns>
        public static TLEResponse Create(SatInfo info, string lines)
        {
            return new()
            {
                Info = info,
                Tle = lines
            };
        }
    }
}