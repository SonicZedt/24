using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameHandler))]
public class FormulaMarkEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GameHandler gameHandler = (GameHandler)target;
        gameHandler.RandomMark = EditorGUILayout.Toggle("Random Mark", gameHandler.RandomMark);
        
        void RandomMarkProperties(bool random) {
            EditorGUI.indentLevel++;
            GUI.enabled = !random;
                gameHandler.Mark = EditorGUILayout.IntField("Constant Mark", gameHandler.Mark, GUILayout.Width(175));
            GUI.enabled = random;
                EditorGUILayout.BeginHorizontal();
                gameHandler.MinMark = EditorGUILayout.IntField("Min", gameHandler.MinMark, GUILayout.Width(175));
                gameHandler.MaxMark = EditorGUILayout.IntField("Max", gameHandler.MaxMark, GUILayout.Width(175));
                EditorGUILayout.EndHorizontal();
            GUI.enabled = !random;
        }

        RandomMarkProperties(gameHandler.RandomMark);
    }
}