using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using StyleSmith.Runtime.Domain;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UEditor = UnityEditor.Editor;

namespace StyleSmith.Editor
{
    [CustomEditor(typeof(Theme))]
    public class ThemeEditorManual : UEditor
    {
        private readonly Regex backingFieldRegex = new(@"<([a-zA-Z][a-zA-Z0-9]*)>");

        [CanBeNull] private Theme _theme;
        private Dictionary<string, SerializedProperty> _properties;


        private void OnEnable()
        {
            _theme = target as Theme;
            _properties = GetProperties();
        }

        private Dictionary<string, SerializedProperty> GetProperties()
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var fields = typeof(Theme).GetFields(bindingFlags);

            var result = new Dictionary<string, SerializedProperty>(fields.Length);

            foreach (var field in fields)
            {
                string name = field.Name;
                if (name.StartsWith("m_"))
                {
                    continue;
                }

                if (backingFieldRegex.IsMatch(name))
                {
                    var matches = backingFieldRegex.Matches(name);
                    name = matches[0].Groups[1].Value;
                }

                result[name] = serializedObject.FindProperty(field.Name);
            }

            return result;
        }

        public override void OnInspectorGUI()
        {
            if (_theme == null)
            {
                RenderHelpBox("The theme scriptable object is not attached correctly!", MessageType.Error);
                return;
            }

            EditorGUILayout.PropertyField(_properties[nameof(Theme.Colors)]);
            EditorGUILayout.PropertyField(_properties[nameof(Theme.Typographies)]);
            
            if(serializedObject.hasModifiedProperties)
                serializedObject.ApplyModifiedProperties();
        }

        private void RenderHelpBox(string message, MessageType messageType = MessageType.Info)
        {
            using EditorGUILayout.HorizontalScope horizontalScope = new EditorGUILayout.HorizontalScope();
            EditorGUILayout.HelpBox(message, messageType);
        }
    }
}