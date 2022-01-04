using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private new CameraController camera;
    private Input input;

    [Header("Formula")]
    [SerializeField] private int operandCount;
    [SerializeField] private int maxModifier;
    [SerializeField] [HideInInspector] private bool randomMark;
    [SerializeField] [HideInInspector] private int mark, minMark, maxMark;
    private Answer answer;
    private List<int> operands = new List<int>();
    private List<string> operators = new List<string>();

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
    public int OperandCount { get { return operandCount; }}
    public List<int> Operands { get { return operands; }}
    public List<string> Operators { get { return operators; }}

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
        Formula formula = new Formula(mark, maxModifier, operandCount);

        formula.GenerateQuestion();
        operands = formula.operands;
        operators = formula.operators;

        Debug.Log($"{formula.question} = {formula.Result()}");
        Debug.Log("=======================");
    }

    public void CheckAnswer() {
        // TODO: show pop up if answer is true
        DataTable dt = new DataTable();

        int result = (int)dt.Compute(answer.Get, " ");
        Debug.Log(result);
    }
}
