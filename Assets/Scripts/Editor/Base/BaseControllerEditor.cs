using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SatLight.Utilities;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editors.Ru1t3rl
{
    public class BaseControllerEditor : Editor
    {
        private int _selectedMethod;
        private object[] _parameters = Array.Empty<object>();

        private MethodInfo _previousMethod;

        private static bool _open;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(25);


            _open = EditorGUILayout.Foldout(_open, "Debug Tools");

            if (_open)
            {
                using var debugToolsBox = new EditorGUILayout.VerticalScope(EditorStyles.helpBox);
                RenderDebugTools();
            }
        }

        protected virtual void RenderDebugTools()
        {
            var methods = AllMethods;
            GUILayout.Label("Method:");
            _selectedMethod = EditorGUILayout.Popup(_selectedMethod,
                methods.Select(method => MethodToText(method)).ToArray());

            if (methods.Length > 0)
            {
                GUILayout.Space(5);
                DrawParameterFields(methods[_selectedMethod]);
            }
            else
            {
                EditorGUILayout.HelpBox("There aren't any methods to test!", MessageType.Warning);
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Invoke"))
            {
                InvokeMethod(methods[_selectedMethod], _parameters);
            }
        }

        protected void DrawParameterFields(MethodInfo methodInfo)
        {
            GUILayout.Label("Parameters:");

            var parameters = methodInfo.GetParameters();
            using var box = new EditorGUILayout.VerticalScope(EditorStyles.helpBox);

            if (parameters.Length <= 0)
            {
                GUILayout.Label("This method has no parameters.");
                return;
            }

            if (methodInfo != _previousMethod)
            {
                _parameters = new object[parameters.Length];

                for (int i = 0; i < _parameters.Length; i++)
                {
                    if (parameters[i].ParameterType == typeof(int))
                    {
                        _parameters[i] = 0;
                    }
                    else if (
                        parameters[i].ParameterType == typeof(float) ||
                        parameters[i].ParameterType == typeof(Single))
                    {
                        _parameters[i] = 0.0f;
                    }
                    else
                    {
                        _parameters[i] = String.Empty;
                    }
                }
            }

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType == typeof(int))
                {
                    _parameters[i] = EditorGUILayout.IntField(ParameterNameToReadable(parameters[i].Name),
                        (int)_parameters[i]);
                }
                else if (parameters[i].ParameterType == typeof(Single))
                {
                    _parameters[i] = EditorGUILayout.FloatField(ParameterNameToReadable(parameters[i].Name),
                        (Single)_parameters[i]);
                }
                else if (parameters[i].ParameterType == typeof(float))
                {
                    _parameters[i] = EditorGUILayout.FloatField(ParameterNameToReadable(parameters[i].Name),
                        (float)_parameters[i]);
                }
                else if (parameters[i].ParameterType == typeof(Component))
                {
                    _parameters[i] = EditorGUILayout.ObjectField(
                        ParameterNameToReadable(parameters[i].Name),
                        _parameters[i] as Object,
                        parameters[i].ParameterType,
                        true
                    );
                }
                else
                {
                    _parameters[i] = EditorGUILayout.TextField(parameters[i].Name, _parameters[i].ToString());
                }
            }

            _previousMethod = methodInfo;
        }

        private MethodInfo[] AllMethods => typeof(N2YOController).GetMethods(
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.Public |
            BindingFlags.DeclaredOnly
        );

        private string MethodToText(MethodInfo method)
        {
            return
                $"{method.Name}({string.Join(", ", method.GetParameters().Select(param => param.ParameterType.Name))})";
        }

        private string ParameterNameToReadable(string parameterName)
        {
            return Char.ToUpper(parameterName[0]) + Regex.Replace(parameterName.Substring(1), @"\B[A-Z]", " $0");
        }

        protected void InvokeMethod(MethodInfo methodInfo, params object[] parameters)
        {
            methodInfo.Invoke(target, parameters);
        }
    }
}