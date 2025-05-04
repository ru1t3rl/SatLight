using UnityEngine;

namespace SatLight.Enums
{
    public enum StartupMode
    {
        OnAwake,
        OnStart,
        [Tooltip("When set to manually you will make sure the behaviour is start by you! Either using the inspector button or through another script.")]
        Manually
    }
}