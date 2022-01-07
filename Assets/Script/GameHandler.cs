using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private new CameraController camera;
    private Input input;

    [HideInInspector] [SerializeField] private int operandCount;
    [HideInInspector] [SerializeField] private int maxModifier, mark, minMark, maxMark;
    [HideInInspector] [SerializeField] private bool randomMark;
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
    public List<int> Operands { get { return operands; }}
    public List<string> Operators { get { return operators; }}

    void Awake() {
        input = GetComponent<Input>();
        answer = GetComponent<Answer>();

        GenerateFormula();
    }

    private void GenerateFormula() {
        int RandomMark() {
            return (int)Random.Range(minMark, maxMark++);
        }

        if(randomMark) mark = RandomMark();
        Formula formula = new Formula(mark, maxModifier, operandCount);

        string question = formula.GenerateQuestion();
        operands = formula.Operands;
        operators = formula.Operators;

        Debug.Log($"{question} = {formula.Result()}");
        Debug.Log("=======================");
    }

    public void CheckAnswer() {
        // TODO: show pop up if answer is true
        DataTable dt = new DataTable();

        int result = (int)dt.Compute(answer.Get, " ");
        Debug.Log(result);
    }
}
