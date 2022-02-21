using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public CardHandler handler;

    private int value;
    private bool droppedOnBoard, onEndDrag;
    private TextMeshProUGUI valueText;
    private RectTransform rectTransform;
    private Canvas cardCanvas;
    private CanvasGroup cardCanvasGroup;
    private Vector3 defaultPosition;
    private GameObject slotOrigin, deckSlot;
    private Input input;

    public int Value {
        get { return value; }
        set { this.value = value; }
        }
    public bool DroppedOnBoard {
        get { return droppedOnBoard; }
        set { this.droppedOnBoard = value; }
        }
    public bool BlockRaycast {
        get { return cardCanvasGroup.blocksRaycasts; }
        set { this.cardCanvasGroup.blocksRaycasts = value; }
        }
    public GameObject SlotOrigin {
        get { return slotOrigin == null ? deckSlot : slotOrigin; }
        set { this.slotOrigin = value; }
        }
    public GameObject DeckSlot {
        get { return deckSlot; }
        set { this.deckSlot = value; }
        }

    void Awake() {
        valueText = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        cardCanvas = gameObject.transform.root.GetComponent<Canvas>();
        cardCanvasGroup = GetComponent<CanvasGroup>();
    }

    void Start() {
        input = GameObject.FindGameObjectWithTag("Handler").GetComponent<Input>();
        valueText.text = value.ToString();
        defaultPosition = rectTransform.localPosition;
    }

    void Update() {
    }

    void LateUpdate() {
        // Send card back to deck slot if not dropped on board slot
        if(!droppedOnBoard && onEndDrag) BackToDeck();
        if(droppedOnBoard) PostDropState();
    }

    private bool TouchOnCard() {
        Vector2 inputPosition = input.TouchPosition;
        float[] cardEdges = {
            // Card edges coordinate. Top, Bottom, Left, Right
            rectTransform.position.y + rectTransform.rect.height / 2,
            rectTransform.position.y - rectTransform.rect.height / 2,
            rectTransform.position.x - rectTransform.rect.width / 2,
            rectTransform.position.x + rectTransform.rect.width / 2
        };

        bool TouchInXZone() {
            return (inputPosition.x >= cardEdges[2]) && (inputPosition.x <= cardEdges[3]);
        }

        bool TouchInYZone() {
            return (inputPosition.y >= cardEdges[1]) && (inputPosition.y <= cardEdges[0]);
        }

        return (TouchInXZone() && TouchInYZone());
    }

    private void PostDropState() {
        // Card state or behavior after dropped on board

        // Block raycast if not dragging another card
        // Unblock raycast if dragging another card and it's position is on top of a card
        // This will let the card become swappable and draggable again
        if(TouchOnCard()) cardCanvasGroup.blocksRaycasts = !handler.draggingCard;
    }

    public void Swap(Card target) {
        // target is card on drag
        // Move to target slotOrigin
        rectTransform.position = target.SlotOrigin.transform.position;
        cardCanvasGroup.blocksRaycasts = true;

        // Update slotOrigin
        slotOrigin = target.SlotOrigin;
        BoardSlot boardSlotOrigin = slotOrigin.GetComponent<BoardSlot>();
        boardSlotOrigin.HasCard = true;
        boardSlotOrigin.cardObject = gameObject;
    }

    public void BackToDeck() {
        rectTransform.position = deckSlot.transform.position;
        cardCanvasGroup.blocksRaycasts = true;
        slotOrigin = null;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        handler.draggingCard = true;
        onEndDrag = false;
        droppedOnBoard = false;
        cardCanvasGroup.blocksRaycasts = false;

        if(slotOrigin != null) slotOrigin.GetComponent<BoardSlot>().HasCard = false;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / cardCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        handler.draggingCard = false;
        onEndDrag = true;
        cardCanvasGroup.blocksRaycasts = true;
    }
}
