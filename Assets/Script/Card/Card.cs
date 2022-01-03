using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private int value;
    private bool droppedOnBoard, onEndDrag;
    private TextMeshProUGUI valueText;
    private RectTransform rectTransform;
    private Canvas cardCanvas;
    private CanvasGroup cardCanvasGroup;
    private Vector3 defaultPosition;

    public int Value {
        get { return value; }
        set { this.value = value; }
    }

    public bool DroppedOnBoard {
        get { return droppedOnBoard; }
        set { this.droppedOnBoard = value; }
    }

    void Awake() {
        valueText = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        cardCanvas = gameObject.transform.root.GetComponent<Canvas>();
        cardCanvasGroup = GetComponent<CanvasGroup>();
    }

    void Start() {
        valueText.text = value.ToString();
        defaultPosition = rectTransform.localPosition;
    }

    void LateUpdate() {
        if(!droppedOnBoard && onEndDrag) rectTransform.localPosition = defaultPosition;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        onEndDrag = false;
        droppedOnBoard = false;
        cardCanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / cardCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        onEndDrag = true;
        cardCanvasGroup.blocksRaycasts = true;
    }
}
