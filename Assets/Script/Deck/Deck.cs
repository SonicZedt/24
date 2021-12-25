using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameHandler gameHandler;

    [Header("Deck")]
    public Transform slotParent;
    public GameObject slotPrefab;
    [Range(0f, 5f)] public float slotSpacing;
    public List<Transform> slots = new List<Transform>();

    void Awake() {
        SetSlotList();
    }

    private void SetSlotList() {
        int operandCount = gameHandler.operandCount;
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