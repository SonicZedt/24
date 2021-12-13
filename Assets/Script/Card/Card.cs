using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public int value;

    void Start() {
        valueText.text = value.ToString();
    }
}
