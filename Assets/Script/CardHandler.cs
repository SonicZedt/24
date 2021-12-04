using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    public Transform parent;
    public GameObject card;
    public Deck deck;
    [HideInInspector] public List<GameObject> cardList = new List<GameObject>();

    private CardGenerator cardGenerator;

    void Start() {
        cardGenerator = card.GetComponent<CardGenerator>();
    }

    void Update() {
        if((deck.slot.Count != 0) && (cardList.Count < deck.slot.Count)) {
            SpawnCards();
        }
    }

    private void SpawnCards() {
        for(int i = 0; i < deck.slot.Count; i++) {
            GameObject cardInstance = Instantiate(card, deck.slot[i].position, Quaternion.identity, parent);
            cardList.Add(cardInstance);
        }
    }
}
