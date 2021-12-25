using UnityEngine;
using UnityEngine.EventSystems;

public class BoardSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData){
        GameObject cardObject = eventData.pointerDrag;

        if(cardObject != null) {
            cardObject.GetComponent<Card>().DroppedOnBoard = true;
            cardObject.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        }
    }
}
