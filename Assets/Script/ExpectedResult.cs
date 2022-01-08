using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExpectedResult : MonoBehaviour
{
    [SerializeField] private GameHandler gameHandler;
    [SerializeField] private TextMeshProUGUI expectedResultText;

    void Start() {
        SetExpectedResult();
    }

    private void SetExpectedResult() {
        expectedResultText.text = gameHandler.ExpectedResult.ToString();
    }
}
