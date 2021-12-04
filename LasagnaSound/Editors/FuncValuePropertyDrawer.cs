﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace LasagnaSound
{
    [CustomPropertyDrawer(typeof(FunctionValue))]
    class FuncValuePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            SerializedProperty funcManaged = property.FindPropertyRelative("functionManaged");
            SerializedProperty constantValue = property.FindPropertyRelative("fixedValue");
            SerializedProperty curve = property.FindPropertyRelative("curve");

            EditorGUI.BeginProperty(position, label, property);

            float px = position.x;
            Rect rect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            funcManaged.boolValue = EditorGUI.Toggle(rect, "Function", funcManaged.boolValue);
            rect.x = px;
            EditorGUI.Toggle(position, property.FindPropertyRelative("functionManaged").boolValue);
            if (funcManaged.boolValue)
            {
                EditorGUI.PropertyField(rect, curve);
            }
            else
            {
                EditorGUI.PropertyField(rect, constantValue);
            }


                EditorGUI.EndProperty();
        }
    }
}