using Editors.Ru1t3rl;
using TLE.Runtime.Controllers;
using UnityEditor;
using UnityEngine;

namespace Editors.TLE.Controllers
{
    [CustomEditor(typeof(TLEController))]
    public class TLEControllerEditor : BaseControllerEditor<TLEController>
    {
    }
}