using UnityEngine;
using UnityEngine.EventSystems;

public class BoardSlot : MonoBehaviour, IDropHandler
{
    public int slotID;
    public GameObject cardObject;

    private int value;
    private bool hasCard;

    public int Value { get { return value; }}
    public bool HasCard { 
        get { return hasCard; }
        set { this.hasCard = value; }
        }

    void Update() {
        SlotState();
    }

    private void SlotState() {
        if(!hasCard) {
            // Confirm hasCard value
            //hasCard = cardObject == null ? false : cardObject != null;

            cardObject = null;
            value = 0;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        GameObject cardObjectOnDrag = eventData.pointerDrag;
        Card cardOnDrag = cardObjectOnDrag.GetComponent<Card>();

        if(hasCard) SwapCard();
        else PutNewCard();

        void PutNewCard() {
            cardObject = cardObjectOnDrag;
            hasCard = true;
            Card cardOnBoard = cardObject.GetComponent<Card>();
            Vector3 slotPosition = GetComponent<RectTransform>().position;

            cardObject.GetComponent<RectTransform>().position = slotPosition;
            cardOnBoard.DroppedOnBoard = true;
            cardOnBoard.SlotOrigin = gameObject;
            value = cardOnBoard.Value;
        }

        void SwapCard() {
            Card cardOnBoard = cardObject.GetComponent<Card>();

            if(cardOnDrag.SlotOrigin.GetComponent<BoardSlot>() != null) cardOnBoard.Swap(cardOnDrag);
            else cardOnBoard.BackToDeck();

            PutNewCard();
        }
    }
}
