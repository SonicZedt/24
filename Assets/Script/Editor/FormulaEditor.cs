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
        gameHandler.RandomMark = EditorGUILayout.Toggle("Random Mark", gameHandler.RandomMark);
        
        void SetMarkProperties(bool random) {
            GUI.enabled = !random;
                gameHandler.Mark = EditorGUILayout.IntField("Constant Mark", gameHandler.Mark, GUILayout.Width(175));
            GUI.enabled = random;
                gameHandler.MinMark = EditorGUILayout.IntField("Min", gameHandler.MinMark, GUILayout.Width(175));
                gameHandler.MaxMark = EditorGUILayout.IntField("Max", gameHandler.MaxMark, GUILayout.Width(175));
            GUI.enabled = !random;
        }

        void SetOperandCount() {
            gameHandler.OperandCount = EditorGUILayout.IntField("Operand Count", gameHandler.OperandCount);
        }

        void SetMaxModifier() {
            gameHandler.MaxModifier = EditorGUILayout.IntField("Max Modifier", gameHandler.MaxModifier);
        }

        EditorGUI.indentLevel++;
            SetMarkProperties(gameHandler.RandomMark);
        EditorGUI.indentLevel--;
        SetOperandCount();
        SetMaxModifier();
    }
}