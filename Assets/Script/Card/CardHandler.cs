using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    [SerializeField] private GameHandler gameHandler;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Deck deck;
    private List<GameObject> cardList = new List<GameObject>();

    void Start() {
        SetDeck();
    }

    private void SetDeck() {
        List<Transform> deckSlots = deck.Slots;
        Debug.Log(deckSlots);

        void SpawnCards() {
            for(int i = 0; i < deckSlots.Count; i++) {
                GameObject cardInstance = Instantiate(cardPrefab, deckSlots[i].position, Quaternion.identity, gameObject.transform);
                cardList.Add(cardInstance);
            }
        }

        void SetCardsValueAndShuffle() {
            List<Vector3> slotPosition = new List<Vector3>();
            List<int> operands = gameHandler.Operands;

            foreach(Transform slot in deckSlots) slotPosition.Add(slot.position);
            for(int i = 0; i < cardList.Count; i++) {
                // Set card value
                Card card = cardList[i].GetComponent<Card>();
                card.Value = operands[i];

                // Move card to random deck slot position
                int index = Random.Range(0, slotPosition.Count);
                cardList[i].transform.position = slotPosition[index];
                slotPosition.RemoveAt(index);
            }
        }

        if((deckSlots.Count == 0) || (cardList.Count >= deckSlots.Count)) return;

        SpawnCards();
        SetCardsValueAndShuffle();
    }
}
