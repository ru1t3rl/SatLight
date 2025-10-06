using StyleSmith.Runtime.Domain;
using UnityEditor;
using UnityEngine;

namespace StyleSmith.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(OptionCollection<>))]
    public class OptionCollectionPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var optionsProperty = property.FindPropertyRelative("_options");
            EditorGUI.PropertyField(position, optionsProperty ?? property, label, true);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var optionsProperty = property.FindPropertyRelative("_options");
            return EditorGUI.GetPropertyHeight(optionsProperty ?? property, label, true);
        }
    }
}