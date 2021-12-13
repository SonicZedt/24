using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    public Transform parent;
    public GameObject cardPrefab;
    public Deck deck;
    [HideInInspector] public List<GameObject> cardList = new List<GameObject>();

    private GameHandler gameHandler;

    void Start() {
        gameHandler = GetComponent<GameHandler>();
    }

    void Update() {
        if((deck.slot.Count != 0) && (cardList.Count < deck.slot.Count)) {
            SpawnCards();
            SetCardsValue();
        }
    }

    private void SpawnCards() {
        for(int i = 0; i < deck.slot.Count; i++) {
            GameObject cardInstance = Instantiate(cardPrefab, deck.slot[i].position, Quaternion.identity, parent);
            cardList.Add(cardInstance);
        }
    }

    private void SetCardsValue() {
        for(int i = 0; i < cardList.Count; i++) {
            Card cardGenerator = cardList[i].GetComponent<Card>();
            cardGenerator.value = gameHandler.operands[i];
        }
    }

    /* TODO: Randomize card position on deck
    private void ShuffleDeck() {

    }
    */
}
