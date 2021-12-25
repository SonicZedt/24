using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameHandler gameHandler;

    [Header("Deck")]
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] [Range(0f, 5f)] private float slotSpacing;
    private List<Transform> slots = new List<Transform>();

    public List<Transform> Slots { get { return slots; }}

    void Awake() {
        SetSlotList();
    }

    private void SetSlotList() {
        int operandCount = gameHandler.OperandCount;
        float positionModifier = operandCount / 2;

        if(operandCount % 2 == 0) positionModifier -= .5f;
        
        Vector2 Position(int i) {
            float posX = slotSpacing * (i - positionModifier);
            float posY = slotParent.position.y;

            return new Vector2(posX, posY);
        }

        for(int i = 0; i < operandCount; i++) {
            GameObject slot = Instantiate(slotPrefab, Position(i), Quaternion.identity, slotParent);
            slots.Add(slot.transform);
        }
    }
}