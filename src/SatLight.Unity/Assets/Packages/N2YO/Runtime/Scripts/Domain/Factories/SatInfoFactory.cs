using N2YO.Runtime.Domain.Common;

namespace N2YO.Runtime.Domain.Factories
{
    public class SatInfoFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SatInfo"/> class with the specified properties.
        /// </summary>
        /// <param name="id">The identifier for the satellite.</param>
        /// <param name="name">The name of the satellite.</param>
        /// <returns>A new instance of <see cref="SatInfo"/> initialized with the specified id and name.</returns>
        public static SatInfo Create(int id, string name)
        {
            return new()
            {
                SatId = id,
                SatName = name,
                TransactionCount = 0,
            };
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SatInfo"/> class with the specified properties.
        /// </summary>
        /// <param name="id">The identifier for the satellite.</param>
        /// <param name="name">The name of the satellite.</param>
        /// <param name="transactionCount">The transaction count associated with the satellite.</param>
        /// <returns>A new instance of <see cref="SatInfo"/> initialized with the specified id, name, and transaction count.</returns>
        public static SatInfo Create(int id, string name, int transactionCount)
        {
            return new()
            {
                SatId = id,
                SatName = name,
                TransactionCount = transactionCount,
            };
        }
    }
}