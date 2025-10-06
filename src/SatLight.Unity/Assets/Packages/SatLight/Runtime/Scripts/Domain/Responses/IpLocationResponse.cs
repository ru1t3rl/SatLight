using System;
using SatLight.Runtime.Domain.Common;

namespace Ru1t3rl.Models
{
    [Serializable]
    public class IpLocationResponse
    {
        public string Ip { get; set; }
        public string Hostname { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Loc { get; set; }
        public string Org { get; set; }
        public string Postal { get; set; }
        public string Timezone { get; set; }
        public string Readme { get; set; }

        public static implicit operator Location(IpLocationResponse ipLocation)
        {
            if (ipLocation.Loc == null)
            {
                throw new NullReferenceException("The location field was null!");
            }

            var splitLocation = ipLocation.Loc.Split(',');

            return new Location(
                float.Parse(splitLocation[0]),
                float.Parse(splitLocation[1])
            );
        }
    }
}