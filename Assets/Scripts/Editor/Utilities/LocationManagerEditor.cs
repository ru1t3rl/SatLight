using System;
using System.Threading.Tasks;
using Ru1t3rl.Utilities;
using UnityEditor;
using UnityEngine;
using Logger = Ru1t3rl.Utilities.Logger;

namespace Editors.Ru1t3rl.Utilities
{
    [CustomEditor(typeof(LocationManager))]
    public class LocationManagerEditor : Editor
    {
        public override async void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Space(15);

            GUILayout.Label("Test Location Gathering");
            
            using var hLayout = new EditorGUILayout.HorizontalScope();

            try
            {
                if (GUILayout.Button("IPv4 Location"))
                {
                    var location = await (target as LocationManager).GetCurrentLocation(true);
                    Logger.Log($"Location ({location.Latitude}, {location.Longitude}, {location.Altitude})");
                }

                if (GUILayout.Button("Location Service"))
                {
                    var location = await (target as LocationManager).GetCurrentLocation(false);
                    Logger.Log($"Location ({location.Latitude}, {location.Longitude}, {location.Altitude})");
                }
            }
            catch (NullReferenceException)
            {
                
            }
        }
    }
}