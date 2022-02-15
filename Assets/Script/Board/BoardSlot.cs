using UnityEngine;
using UnityEngine.EventSystems;

public class BoardSlot : MonoBehaviour, IDropHandler
{
    private GameObject cardObject;
    private int value;
    private bool hasCard;

    public int Value { get { return value; }}
    public bool HasCard { get { return hasCard; }}

    void Update() {
        CheckCard();
    }

    private void CheckCard() {
        hasCard = cardObject == null ? false : cardObject.transform.position == transform.position;

        if(!hasCard) {
            cardObject = null;
            value = 0;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        // TODO: Swappable on board card
        GameObject cardObjectOnDrag = eventData.pointerDrag;
        Card cardOnDrag = cardObjectOnDrag.GetComponent<Card>();

        if(hasCard) SwapCard();
        else PutNewCard();

        void PutNewCard() {
            cardObject = cardObjectOnDrag;
            Card cardOnBoard = cardObject.GetComponent<Card>();
            Vector3 slotPosition = GetComponent<RectTransform>().position;

            cardObject.GetComponent<RectTransform>().position = slotPosition;
            cardOnBoard.DroppedOnBoard = true;
            cardOnBoard.SlotOrigin = gameObject;
            value = cardOnBoard.Value;
        }

        void SwapCard() {
            Card cardOnBoard = cardObject.GetComponent<Card>();
            Vector3 cardOnBoardPosition = cardObject.GetComponent<RectTransform>().position;

            if(cardOnDrag.DroppedOnBoard)
                cardObject.GetComponent<RectTransform>().position = cardOnDrag.SlotOrigin.transform.position;
            else
                cardObject.GetComponent<RectTransform>().position = cardOnBoard.DeckSlot.transform.position;

            PutNewCard();
        }
    }
}
