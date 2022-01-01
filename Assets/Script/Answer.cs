using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    [SerializeField] private Board board;
    private List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        slots = board.Slots;
    }

    void Update()
    {
        Check();
    }

    private void Check() {
        string hasCard = "";
        string cardValue = "";
        
        for(int i = 0; i < slots.Count; i++) {
            BoardSlot slot = slots[i].GetComponent<BoardSlot>();

            hasCard = $"{slot.HasCard}, {hasCard}";
            cardValue = $"{slot.Value}, {cardValue}";
        }

        Debug.Log(hasCard);
        Debug.Log(cardValue);
    }
}
