using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameHandler))]
public class FormulaEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GameHandler gameHandler = (GameHandler)target;
        
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Formula", EditorStyles.boldLabel);
        
        void SetMarkProperties(bool random) {
            GUI.enabled = !random;
            gameHandler.Mark = EditorGUILayout.IntField("Constant Mark", gameHandler.Mark, GUILayout.Width(175));
        
            GUI.enabled = true;
            gameHandler.RandomMark = EditorGUILayout.Toggle("Random Mark", gameHandler.RandomMark);

            GUI.enabled = random;
            EditorGUI.indentLevel++;
                gameHandler.MinMark = EditorGUILayout.IntField("Min", gameHandler.MinMark, GUILayout.Width(175));
                gameHandler.MaxMark = EditorGUILayout.IntField("Max", gameHandler.MaxMark, GUILayout.Width(175));
            EditorGUI.indentLevel--;
            GUI.enabled = true;
        }

        void SetOperandCount() {
            gameHandler.OperandCount = EditorGUILayout.IntField("Operand Count", gameHandler.OperandCount);
        }

        void SetMaxModifier() {
            gameHandler.MaxModifier = EditorGUILayout.IntField("Max Modifier", gameHandler.MaxModifier);
        }

        SetMarkProperties(gameHandler.RandomMark);
        SetOperandCount();
        SetMaxModifier();
    }
}