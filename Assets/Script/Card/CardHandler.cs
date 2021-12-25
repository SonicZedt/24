using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    public GameHandler gameHandler;
    public GameObject cardPrefab;
    public Deck deck;
    public List<GameObject> cardList = new List<GameObject>();

    void Start() {
        SetDeck();
    }

    private void SetDeck() {
        void SpawnCards() {
            for(int i = 0; i < deck.slots.Count; i++) {
                GameObject cardInstance = Instantiate(cardPrefab, deck.slots[i].position, Quaternion.identity, gameObject.transform);
                cardList.Add(cardInstance);
            }
        }

        void SetCardsValueAndShuffle() {
            List<Vector3> slotPosition = new List<Vector3>();

            foreach(Transform slot in deck.slots) slotPosition.Add(slot.position);
            for(int i = 0; i < cardList.Count; i++) {
                // Set card value
                Card card = cardList[i].GetComponent<Card>();
                card.value = gameHandler.operands[i];

                // Move card to random deck slot position
                int index = Random.Range(0, slotPosition.Count);
                cardList[i].transform.position = slotPosition[index];
                slotPosition.RemoveAt(index);
            }
        }

        if((deck.slots.Count == 0) || (cardList.Count >= deck.slots.Count)) return;

        SpawnCards();
        SetCardsValueAndShuffle();
    }
}
