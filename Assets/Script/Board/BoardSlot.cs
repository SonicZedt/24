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
        cardObject = eventData.pointerDrag;

        if(cardObject != null) {
            Card card = cardObject.GetComponent<Card>();

            cardObject.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            card.DroppedOnBoard = true;
            value = card.Value;
        }
    }
}
