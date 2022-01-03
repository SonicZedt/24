using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private new CameraController camera;
    
    [Header("Formula")]
    [SerializeField] private int result;
    [SerializeField] private int maxModifier;
    [SerializeField] private int operandCount;
    private Answer answer;
    private List<int> operands = new List<int>();
    private List<string> operators = new List<string>();
    private Input input;

    public int OperandCount { get { return operandCount; }}
    public List<int> Operands { get { return operands; }}
    public List<string> Operators { get { return operators; }}

    void Awake() {
        input = GetComponent<Input>();
        answer = GetComponent<Answer>();

        GenerateFormula();
    }

    void Update() {
    }

    private void GenerateFormula() {
        Formula formula = new Formula(result, maxModifier, operandCount);

        formula.GenerateQuestion();
        operands = formula.operands;
        operators = formula.operators;

        Debug.Log($"{formula.question} = {formula.Result()}");
        Debug.Log("=======================");
    }

    public void CheckAnswer() {
        DataTable dt = new DataTable();

        int result = (int)dt.Compute(answer.Get, " ");
        Debug.Log(result);
    }
}
