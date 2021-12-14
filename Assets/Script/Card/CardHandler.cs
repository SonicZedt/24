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
        SetDeck();
    }

    private void SetDeck() {    
        void SpawnCards() {
            for(int i = 0; i < deck.slot.Count; i++) {
                GameObject cardInstance = Instantiate(cardPrefab, deck.slot[i].position, Quaternion.identity, parent);
                cardList.Add(cardInstance);
            }
        }

        void SetCardsValue() {
            for(int i = 0; i < cardList.Count; i++) {
                Card cardGenerator = cardList[i].GetComponent<Card>();
                cardGenerator.value = gameHandler.operands[i];
            }
        }

        void Shuffle() {
            List<Vector3> slotPosition = new List<Vector3>();

            foreach(Transform slot in deck.slot) slotPosition.Add(slot.position);
            for(int i = 0; i < cardList.Count + 1; i++) {
                int index = Random.Range(0, slotPosition.Count);
                cardList[i].transform.position = slotPosition[index];

                slotPosition.RemoveAt(index);
            }
        }

        if((deck.slot.Count == 0) || (cardList.Count >= deck.slot.Count)) return;

        SpawnCards();
        SetCardsValue();
        Shuffle();
    }
}
