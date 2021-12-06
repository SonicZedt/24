using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public new CameraController camera;
    
    private Input input;

    void Start() {
        input = gameObject.GetComponent<Input>();
        GenerateFormula();
    }

    void Update() {
    }

    private void GenerateFormula() {
        Formula formula = new Formula();
        formula.GenerateQuestion(8);
    }
}
