using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public new CameraController camera;
    
    [Header("Formula")]
    public int result;
    public int maxModifier;
    public int operandCount;
    public List<int> operands = new List<int>();
    public List<string> operators = new List<string>();
    
    private Input input;

    void Awake() {
        input = gameObject.GetComponent<Input>();
    }

    void Start() {
        GenerateFormula(result, maxModifier, operandCount);
    }

    void Update() {
        /*
        if(generate) {
            GenerateFormula(result, maxModifier);
            generate = false;
        }
        */
    }

    private void GenerateFormula(int result, int maxModifier, int operandCount) {
        Formula formula = new Formula(result, maxModifier);
        formula.GenerateQuestion(operandCount);
        operands = formula.operands;
        operators = formula.operators;
        Debug.Log($"{formula.question} = {formula.Result()}");
        Debug.Log("=======================");
    }
}
