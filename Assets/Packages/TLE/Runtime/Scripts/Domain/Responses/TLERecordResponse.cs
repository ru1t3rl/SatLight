using UnityEngine;

// https://tle.ivanstanojevic.me/#operation/record
namespace TLE.Runtime.Domain
{
    public record struct TLERecordResponse(
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