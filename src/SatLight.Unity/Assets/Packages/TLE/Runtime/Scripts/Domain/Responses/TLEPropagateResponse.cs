using UnityEngine;

namespace TLE.Runtime.Domain.Responses
{
    public record TLEPropagateResponse(
        string @context,
        string @id,
        string @type,
        TLEModel tle,
        string algorithm,
        TLETransform vector,
        TLELocation geodetic
    );
}
