using System;
using UnityEngine;

namespace StyleSmith.Runtime.Domain
{
    [Serializable]
    public class Option<T> : IOption where T : notnull, new()
    {
        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        public T Value { get; set; } = new();
    }
}