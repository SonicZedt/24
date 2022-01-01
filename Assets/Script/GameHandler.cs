using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private new CameraController camera;
    
    [Header("Formula")]
    [SerializeField] private int result;
    [SerializeField] private int maxModifier;
    [SerializeField] private int operandCount;
    private List<int> operands = new List<int>();
    private List<string> operators = new List<string>();
    private Input input;

    public int OperandCount { get { return operandCount; }}
    public List<int> Operands { get { return operands; }}
    public List<string> Operators { get { return Operators; }}

    void Awake() {
        input = gameObject.GetComponent<Input>();
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
}
