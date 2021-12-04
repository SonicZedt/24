using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Transform deckHigh, deckLow;
    public Transform activeDeck;
    public List<Transform> slot = new List<Transform>();

    void Start() {
        SetSlotList();
    }

    private void GetActiveDeck() {
        // TODO: set [Transform] active (Deck_Low or Deck_High) based on total card (4 or 8)
    }

    private void SetSlotList() {
        foreach(Transform availableSlot in activeDeck) slot.Add(availableSlot);
    }
}