using System;
using UnityEngine;

namespace TLE.Runtime.Domain.Responses
{
    public record TLECollectionResponse(
        string @context,
        string @id,
        string @type,
        int totalItems,
        TLEModel[] member,
        dynamic parameters,
        dynamic view
    );
}
