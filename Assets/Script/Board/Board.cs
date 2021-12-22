using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameHandler gameHandler;

    [Header("Slot")]
    public Transform slotParent;
    public GameObject slotPrefab;

    private List<GameObject> slots = new List<GameObject>();

    void Awake() {
        SetCardSlot();
    }

    void SetCardSlot() {
        int operandCount = gameHandler.operandCount;
        float positionModifier = operandCount / 2;

        if(operandCount % 2 == 0) positionModifier -= .5f;

        for(int i = 0; i < operandCount; i++) {
            GameObject slot = Instantiate(slotPrefab, Vector3.left * (i - positionModifier), Quaternion.identity, slotParent);
            slots.Add(slot);
        }
    }
}
