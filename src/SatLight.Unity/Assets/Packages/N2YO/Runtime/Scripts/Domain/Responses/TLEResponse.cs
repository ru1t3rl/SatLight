using N2YO.Runtime.Domain.Common;
using UnityEngine;

namespace N2YO.Runtime.Domain.Responses
{
    [System.Serializable]
    public class TLEResponse
    {
        /// <summary>
        ///  Basic information about the satellite
        /// </summary>
        [field: SerializeField]
        public SatInfo Info { get; set; }

        /// <summary>
        /// TLE on single line string. Split the line in two by \r\n to get original two lines
        /// </summary>
        [field: SerializeField]
        public string Tle { get; set; }

        internal TLEResponse()
        {
        }
    }
}