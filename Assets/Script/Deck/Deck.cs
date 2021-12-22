using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameHandler gameHandler;

    [Header("Deck")]
    public Transform deckHigh;
    public Transform deckLow;
    public List<Transform> slot = new List<Transform>();
    
    private Transform activeDeck;

    void Awake() {
        HideDeck();
        GetActiveDeck();
        SetSlotList();
    }

    private void GetActiveDeck() {
        if(gameHandler.operandCount >= 8) activeDeck = deckHigh;
        else if(gameHandler.operandCount < 8) activeDeck = deckLow;
    }

    private void SetSlotList() {
        activeDeck.gameObject.SetActive(true);
        foreach(Transform availableSlot in activeDeck) slot.Add(availableSlot);
    }

    private void HideDeck() {
        deckHigh.gameObject.SetActive(false);
        deckLow.gameObject.SetActive(false);
    }
}