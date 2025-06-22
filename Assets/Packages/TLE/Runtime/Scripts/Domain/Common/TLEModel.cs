using Newtonsoft.Json;

namespace TLE.Runtime.Domain
{
    public class TLEModel
    {
        [JsonProperty("@context")]
        public string Context { get; init; }
        [JsonProperty("@id")]
        public string Id { get; init; }
        [JsonProperty("@type")]
        public string Type { get; init; }
        public int satelliteId { get; init; }
        public string name { get; init; }
        public string date { get; init; }
        public string line1  { get; init; }
        public string line2 { get; init; }
    }
}