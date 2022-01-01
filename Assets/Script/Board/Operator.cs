using UnityEngine;
using TMPro;

public class Operator : MonoBehaviour
{
    private string type;
    private TextMeshProUGUI typeText;

    public string Type {
        get { return type; }
        set { type = value; }
    }

    void Awake() {
        typeText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Start() {
        typeText.text = type;
    }
}
