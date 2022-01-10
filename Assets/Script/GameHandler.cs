using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private new CameraController camera;
    [SerializeField] private NotifyResult notifyResult;
    private Input input;
    [HideInInspector] [SerializeField] private int operandCount;
    [HideInInspector] [SerializeField] private int modifier, mark, minMark, maxMark, minModifier, maxModifier;
    [HideInInspector] [SerializeField] private bool includeMark, randomMark, randomModifier, nonNegative;
    [HideInInspector] [SerializeField] private bool[] operatorsToggle = new bool[4];
    private object expectedResult;
    private List<int> operands = new List<int>();
    private List<string> operators = new List<string>();
    private Answer answer;

    #region Getter & Setter
    public bool IncludeMark {
        get { return includeMark; }
        set { this.includeMark = value; }
        }
    public bool RandomMark {
        get { return randomMark; }
        set { this.randomMark = value; }
        }
    public int Mark {
        get { return mark; }
        set { this.mark = value; }
        }
    public int MinMark { 
        get { return minMark; }
        set { this.minMark = value; }
        }
    public int MaxMark { 
        get { return maxMark; }
        set { this.maxMark = value; }
        }
    public int OperandCount {
        get { return operandCount; }
        set { this.operandCount = value; }
        }
    public bool RandomModifier {
        get { return randomModifier; }
        set { this.randomModifier = value; }
        }
    public int Modifier { 
        get { return modifier; }
        set { this.modifier = value; }
        }
    public int MinModifier {
        get { return minModifier; }
        set { this.minModifier = value; }
        }
    public int MaxModifier {
        get { return maxModifier; }
        set { this.maxModifier = value; }
        }
    public bool[] OperatorsToggle {
        get { return operatorsToggle; }
        set { this.operatorsToggle = value; }
        }
    public bool NonNegative {
        get { return nonNegative; }
        set { this.nonNegative = value; }
        }
    public List<int> Operands { get { return operands; }}
    public List<string> Operators { get { return operators; }}
    public object ExpectedResult { get { return expectedResult; }}
    #endregion

    void Awake() {
        input = GetComponent<Input>();
        answer = GetComponent<Answer>();

        GenerateFormula();
    }

    private void GenerateFormula() {
        Formula formula;

        int RandomMark() {
            return (int)Random.Range(minMark, maxMark);
        }

        if(randomMark) mark = RandomMark();
        
        if(randomModifier) {
            int[] modifierRange = {minModifier, maxModifier};
            formula = new Formula(mark, modifierRange, operandCount, operatorsToggle, includeMark);
        }
        else {
            formula = new Formula(mark, modifier, operandCount, operatorsToggle, includeMark);
        }

        string question = formula.GenerateQuestion();
        operands = formula.Operands;
        operators = formula.Operators;
        expectedResult = formula.Result();
        
        Debug.Log($"{question} = {expectedResult}");
        Debug.Log("=======================");
    }

    public void CheckAnswer() {
        // TODO: show pop up if answer is true
        DataTable dt = new DataTable();

        object resultGiven = dt.Compute(answer.Get, " ");
        notifyResult.ShowNotification(resultGiven.ToString());

        Debug.Log("answer: " + resultGiven);
        Debug.Log(resultGiven.ToString() == expectedResult.ToString());
    }
}
