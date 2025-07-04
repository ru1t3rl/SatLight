﻿using System;
using N2YO.Runtime.Domain.Enums;
using UnityEngine;

namespace N2YO.Runtime.Domain.Common
{
    [Serializable]
    public class SatInfoAboveRequest
    {
        /// <summary>
        /// Category name (ANY if category id requested was 0)
        /// </summary>
        [field: SerializeField]
        public SatCategory Category { get; set; }

        /// <summary>
        /// Count of transactions performed with this API key in last 60 minutes
        /// </summary>
        [field: SerializeField]
        public int TransactionCount { get; set; }

        /// <summary>
        /// Count of satellites returned
        /// </summary>
        [field: SerializeField]
        public int SatCount { get; set; }
    }
}