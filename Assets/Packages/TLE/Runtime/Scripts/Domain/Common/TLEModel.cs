namespace TLE.Runtime.Domain
{
    public class TLEModel
    {
        public string @context { get; init; }
        public string @id { get; init; }
        public string @type { get; init; }
        public int SatelliteId { get; init; }        
        public string name { get; init; }
        public string date { get; init; }
        public string line1  { get; init; }
        public string line2 { get; init; }
    }
}