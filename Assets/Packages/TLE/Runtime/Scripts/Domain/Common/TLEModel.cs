namespace TLE.Runtime.Domain
{
    public record TLEModel(
        string @context,
        string @id,
        string @type,
        int satelliteId,
        string name,
        string date,
        string line1,
        string line2
    );
}