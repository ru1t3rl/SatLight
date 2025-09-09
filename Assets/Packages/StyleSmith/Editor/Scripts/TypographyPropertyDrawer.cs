using StyleSmith.Runtime.Domain;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace StyleSmith.Editor
{
    [CustomPropertyDrawer(typeof(TypographyOption))]
    public class TypographyPropertyDrawer : PropertyDrawer
    {
        private float _height = 0;
        private static Dictionary<string, bool> _expandedStates = new Dictionary<string, bool>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            _height = 0;
            var nameProperty = property.FindPropertyRelative(nameof(TypographyOption.Name).AsBackingField());
            var valueProperty = property.FindPropertyRelative(nameof(TypographyOption.Value).AsBackingField());

            // Create a unique key for this property instance
            string propertyKey = property.propertyPath;
            
            // Get or initialize the expanded state for this specific property
            if (!_expandedStates.ContainsKey(propertyKey))
            {
                _expandedStates[propertyKey] = true; // Default to expanded
            }

            var foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            _expandedStates[propertyKey] = EditorGUI.Foldout(foldoutRect, _expandedStates[propertyKey], nameProperty.stringValue);
            _height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            if (!_expandedStates[propertyKey])
            {
                EditorGUI.EndProperty();
                return;
            }
            
            RenderProperty(position, nameProperty, nameProperty.displayName);
            
            // Render Typography fields directly instead of as a foldout
            var fontProperty = valueProperty.FindPropertyRelative(nameof(Typography.Font).AsBackingField());
            var sizeProperty = valueProperty.FindPropertyRelative(nameof(Typography.Size).AsBackingField());
            
            RenderProperty(position, fontProperty, "Font");
            RenderProperty(position, sizeProperty, "Size");

            EditorGUI.EndProperty();
        }

        private void RenderProperty(Rect position, SerializedProperty property, string label, bool includeChildren = false)
        {
            var rect = new Rect(position.x, position.y + _height, position.width,
                EditorGUIUtility.singleLineHeight);
            
            EditorGUI.PropertyField(rect, property, new GUIContent(label), includeChildren);
            _height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            string propertyKey = property.propertyPath;
            bool notExpanded = _expandedStates.ContainsKey(propertyKey) && !_expandedStates[propertyKey];
            return notExpanded ? EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2 : _height; 
        }
    }
}