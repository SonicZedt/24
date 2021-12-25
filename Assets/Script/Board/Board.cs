using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameHandler gameHandler;

    [Header("Slot")]
    public Transform slotParent;
    public GameObject slotPrefab;
    [Range(0f, 5f)] public float slotSpacing;

    private List<GameObject> slots = new List<GameObject>();

    void Start() {
        SetCardSlot();
    }

    void SetCardSlot() {
        int operandCount = gameHandler.operandCount;
        float positionModifier = operandCount / 2;

        if(operandCount % 2 == 0) positionModifier -= .5f;
        
        Vector3 Position(int i) {
            return Vector3.left * slotSpacing * (i - positionModifier);
        }

        for(int i = 0; i < operandCount; i++) {
            GameObject slot = Instantiate(slotPrefab, Position(i), Quaternion.identity, slotParent);
            slots.Add(slot);
        }
    }
}
