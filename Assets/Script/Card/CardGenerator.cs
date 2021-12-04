using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class CardGenerator : MonoBehaviour
{
    public List<Card> cardList = new List<Card>();
    public TextMeshProUGUI valueText;
    [HideInInspector] public int value;

    void Start() {
        SetCard();
    }

    private void SetCard() {
        Card card = cardList[Random.Range(0, cardList.Count)];
        valueText.text = card.value.ToString();
        value = card.value;
    }
}
