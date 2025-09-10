using StyleSmith.Runtime.Domain;
using StyleSmith.Editor.Extensions;
using UnityEditor;
using UnityEngine;

namespace StyleSmith.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ColorOption))]
    public class ColorOptionPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float halfWidth = position.width * 0.5f;
            var nameRect = new Rect(position.x, position.y, halfWidth - 2, position.height);
            var valueRect = new Rect(position.x + halfWidth + 2, position.y, halfWidth - 2, position.height);

            var valueProperty = property.FindPropertyRelative(nameof(ColorOption.Value).AsBackingField());
            var nameProperty = property.FindPropertyRelative(nameof(ColorOption.Name).AsBackingField());
            
            EditorGUI.PropertyField(nameRect, nameProperty, GUIContent.none);
            EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}