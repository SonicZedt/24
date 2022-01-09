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
        expectedResultText.text = string.Format("{0:0.##}", float.Parse(gameHandler.ExpectedResult.ToString()));
    }
}
