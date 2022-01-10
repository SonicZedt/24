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
            gameHandler.IncludeMark = EditorGUILayout.Toggle("Mark", gameHandler.IncludeMark);

            EditorGUI.indentLevel++;
                GUI.enabled = gameHandler.IncludeMark;
                gameHandler.RandomMark = EditorGUILayout.Toggle("Random Mark", gameHandler.RandomMark);
                
                GUI.enabled = !random & gameHandler.IncludeMark;
                gameHandler.Mark = EditorGUILayout.IntField("Constant Mark", gameHandler.Mark, GUILayout.Width(175));

                GUI.enabled = random & gameHandler.IncludeMark;
                EditorGUI.indentLevel++;
                    gameHandler.MinMark = EditorGUILayout.IntField("Min", gameHandler.MinMark, GUILayout.Width(175));
                    gameHandler.MaxMark = EditorGUILayout.IntField("Max", gameHandler.MaxMark, GUILayout.Width(175));
                EditorGUI.indentLevel--;
            EditorGUI.indentLevel--;
            GUI.enabled = true;
        }

        void SetOperandCount() {
            gameHandler.OperandCount = EditorGUILayout.IntField("Operand Count", gameHandler.OperandCount);
        }

        void SetModifierProperties(bool random) {
            EditorGUILayout.LabelField("Modifier");

            EditorGUI.indentLevel++;
                gameHandler.RandomModifier = EditorGUILayout.Toggle("Random Modifier", gameHandler.RandomModifier);
                
                GUI.enabled = !random;
                gameHandler.Modifier = EditorGUILayout.IntField("Constant Modifier", gameHandler.Modifier, GUILayout.Width(175));

                GUI.enabled = random;
                EditorGUI.indentLevel++;
                    gameHandler.MinModifier = EditorGUILayout.IntField("Min", gameHandler.MinModifier, GUILayout.Width(175));
                    gameHandler.MaxModifier = EditorGUILayout.IntField("Max", gameHandler.MaxModifier, GUILayout.Width(175));
                EditorGUI.indentLevel--;
                GUI.enabled = true;
            EditorGUI.indentLevel--;
        }

        void SetResultSign() {
            gameHandler.NonNegative = EditorGUILayout.Toggle("Non Negative Result", gameHandler.NonNegative);
        }

        void SetOperatorsToggle() {
            bool LastOneToggled() {
                int activeOperator = 0;

                for(int i = 0; i < gameHandler.OperatorsToggle.Length; i++) {
                    if(gameHandler.OperatorsToggle[i] == true) activeOperator++;
                    if(activeOperator == 2) break;
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

        SetOperandCount();
        SetResultSign();
        SetMarkProperties(gameHandler.RandomMark);
        SetModifierProperties(gameHandler.RandomModifier);
        SetOperatorsToggle();
    }
}