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
            bool LastOneToggled() {
                int activeOperator = 0;

                for(int i = 0; i < gameHandler.OperatorsToggle.Length; i++) {
                    if(gameHandler.OperatorsToggle[i] == true) activeOperator++;
                }

                return activeOperator == 1;
            }

            void Toggle(int index, string label) {
                bool toogleable = true;

                if(LastOneToggled() && gameHandler.OperatorsToggle[index]) toogleable = false;

                GUI.enabled = toogleable;
                gameHandler.OperatorsToggle[index] = EditorGUILayout.Toggle(label, gameHandler.OperatorsToggle[index]);
            }

            EditorGUILayout.LabelField("Operator Toggle");
            EditorGUI.indentLevel++;
                Toggle(0, "Adder");
                Toggle(1, "Substractor");
                Toggle(2, "Multiplicator");
                Toggle(3, "Divider");
            EditorGUI.indentLevel--;
        }

        SetMarkProperties(gameHandler.RandomMark);
        SetOperandCount();
        SetMaxModifier();
        SetOperatorsToggle();
    }
}