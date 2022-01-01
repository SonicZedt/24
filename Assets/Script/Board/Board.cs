using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameHandler gameHandler;

    [Header("Configuration")]
    [SerializeField] [Range(0f, 5f)] private float spacing;

    [Header("Slot")]
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    private List<GameObject> slots = new List<GameObject>();

    [Header("Operator")]
    [SerializeField] private Transform operatorParent;
    [SerializeField] private GameObject operatorPrefab;

    void Start() {
        SetCardSlot();
        SetOperator();
    }

    Vector3 SpawnPosition(int index, float modifier) {
        return Vector3.left * spacing * (index - modifier);
    }

    void SetCardSlot() {
        int operandCount = gameHandler.OperandCount;
        float positionModifier = operandCount / 2;

        if(operandCount % 2 == 0) positionModifier -= .5f;

        for(int i = 0; i < operandCount; i++) {
            GameObject slot = Instantiate(slotPrefab, SpawnPosition(i, positionModifier), Quaternion.identity, slotParent);
            slots.Add(slot);
        }
    }

    void SetOperator() {
        List<string> operators = gameHandler.Operators;
        float positionModifier = operators.Count / 2;

        if(operators.Count % 2 == 0) positionModifier -= .5f;

        for(int i = 0; i < operators.Count; i++) {
            GameObject opr = Instantiate(operatorPrefab, SpawnPosition(i, positionModifier), Quaternion.identity, operatorParent);
            opr.GetComponent<Operator>().Type = operators[i];
        }
    }
}
