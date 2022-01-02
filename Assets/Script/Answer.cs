using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private Button button_checkAnswer;
    private GameHandler gameHandler;
    private List<GameObject> availableSlot = new List<GameObject>();

    void Awake() {
        gameHandler = GetComponent<GameHandler>();
    }

    void Start() {
        availableSlot = board.Slots;
    }

    void Update() {
        ButtonInteraction();
    }

    private void ButtonInteraction() {
        button_checkAnswer.interactable = AnswerSet() ? true : false;
    }

    private bool AnswerSet() {
        for(int i = 0; i < availableSlot.Count; i++) {
            BoardSlot slot = availableSlot[i].GetComponent<BoardSlot>();

            if(!slot.HasCard) return false;
        }

        return true;
    }

    private string BuildAnswer() {
        StringBuilder answerBuilder = new StringBuilder();
        List<string> operators = gameHandler.Operators;
        
        for(int i = 0; i < availableSlot.Count; i++) {
            BoardSlot slot = availableSlot[i].GetComponent<BoardSlot>();

            answerBuilder.Append(slot.Value);
            if(i < operators.Count) answerBuilder.Append(operators[i]);
        }

        return answerBuilder.ToString();
    }

    public void Check() {
        string answer = BuildAnswer();

        Debug.Log(answer);
    }
}
