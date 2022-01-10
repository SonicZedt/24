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
    [HideInInspector] [SerializeField] private int maxModifier, mark, minMark, maxMark;
    [HideInInspector] [SerializeField] private bool randomMark;
    [HideInInspector] [SerializeField] private bool nonNegative;
    [HideInInspector] [SerializeField] private bool[] operatorsToggle = new bool[4];
    private object expectedResult;
    private List<int> operands = new List<int>();
    private List<string> operators = new List<string>();
    private Answer answer;

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

    void Awake() {
        input = GetComponent<Input>();
        answer = GetComponent<Answer>();

        GenerateFormula();
    }

    private void GenerateFormula() {
        int RandomMark() {
            return (int)Random.Range(minMark, maxMark);
        }

        if(randomMark) mark = RandomMark();
        Formula formula = new Formula(mark, maxModifier, operandCount, operatorsToggle);

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
