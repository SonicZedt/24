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

        void SetOperatorsToggle() {
            EditorGUILayout.LabelField("Operator Toggle");
            EditorGUI.indentLevel++;
                gameHandler.OperatorsToggle[0] = EditorGUILayout.Toggle("Adder", gameHandler.OperatorsToggle[0]);
                gameHandler.OperatorsToggle[1] = EditorGUILayout.Toggle("Substractor", gameHandler.OperatorsToggle[1]);
                gameHandler.OperatorsToggle[2] = EditorGUILayout.Toggle("Multiplicator", gameHandler.OperatorsToggle[2]);
                gameHandler.OperatorsToggle[3] = EditorGUILayout.Toggle("Devider", gameHandler.OperatorsToggle[3]);
            EditorGUI.indentLevel--;
        }

        SetMarkProperties(gameHandler.RandomMark);
        SetOperandCount();
        SetMaxModifier();
        SetOperatorsToggle();
    }
}