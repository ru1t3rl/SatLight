using System;
using System.IO;
using System.Runtime.CompilerServices;
using Ru1t3rl.Utilities;
using UnityEngine;

namespace Ru1t3rl.Utilities
{
    public class Logger : UnitySingleton<Logger>
    {
        public static void Log<T>(string message)
        {
#if UNITY_EDITOR
            Debug.Log(BuildStringWithType<T>(message));
#endif
        }

        public static void Log(string message, bool withType = true, [CallerFilePath] string callerFilePath = "")
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log(BuildStringWithType(
                message,
                withType ? Path.GetFileNameWithoutExtension(callerFilePath) : String.Empty)
            );
#endif
        }

        public static void LogWarning<T>(string message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(BuildStringWithType<T>(message));
#endif
        }

        public static void LogWarning(string message, bool withType = true, [CallerFilePath] string callerFilePath = "")
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogWarning(BuildStringWithType(
                message,
                withType ? Path.GetFileNameWithoutExtension(callerFilePath) : String.Empty)
            );
#endif
        }

        public static void LogError<T>(string message)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError(BuildStringWithType<T>(message));
#endif
        }

        public static void LogError(string message, bool withType = true, [CallerFilePath] string callerFilePath = "")
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError(BuildStringWithType(
                message,
                withType ? Path.GetFileNameWithoutExtension(callerFilePath) : String.Empty)
            );
#endif
        }

        private static string BuildStringWithType<T>(string message)
        {
            return $"<b>[{typeof(T).Name}]</b> {message}";
        }

        private static string BuildStringWithType(string message, string typeName)
        {
            return $"<b>[{typeName}]</b> {message}";
        }
    }
}