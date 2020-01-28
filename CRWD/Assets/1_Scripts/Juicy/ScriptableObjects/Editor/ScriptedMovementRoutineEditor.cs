using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CRWD
{
    [CustomEditor(typeof(ScriptedMovementRoutine))]
    public class ScriptedMovementRoutineEditor : Editor
    {
        ScriptedMovementRoutine script;
        SerializedProperty mode;

        private void OnEnable()
        {
            
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            script = target as ScriptedMovementRoutine;

            mode = serializedObject.FindProperty("mode");

            switch (mode.enumValueIndex)
            {
                case 0:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("nameToFind"));
                    break;
                case 1:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("randomInside"));
                    break;
                case 2:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("position"));
                    break;
                default:
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}