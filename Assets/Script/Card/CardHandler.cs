using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    public bool draggingCard;
    
    [SerializeField] private GameHandler gameHandler;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Deck deck;
    private List<GameObject> cardList = new List<GameObject>();

    void Start() {
        SetDeck();
    }

    private void SetDeck() {
        List<Transform> deckSlots = deck.Slots;

        void SpawnCards() {
            for(int i = 0; i < deckSlots.Count; i++) {
                GameObject cardInstance = Instantiate(cardPrefab, deckSlots[i].position, Quaternion.identity, gameObject.transform);
                cardList.Add(cardInstance);
            }
        }

        void SetCardsValueAndShuffle() {
            List<Transform> slotTransform = new List<Transform>();
            List<int> operands = gameHandler.Operands;

            foreach(Transform slot in deckSlots) slotTransform.Add(slot);
            for(int i = 0; i < cardList.Count; i++) {
                // Set card value
                Card card = cardList[i].GetComponent<Card>();
                card.Value = operands[i];

                // Move card to random deck slot position
                int index = Random.Range(0, slotTransform.Count);
                cardList[i].transform.position = slotTransform[index].position;
                card.DeckSlot = slotTransform[index].gameObject;
                card.handler = this;
                slotTransform.RemoveAt(index);
            }
        }

        if((deckSlots.Count == 0) || (cardList.Count >= deckSlots.Count)) return;

        SpawnCards();
        SetCardsValueAndShuffle();
    }
}
