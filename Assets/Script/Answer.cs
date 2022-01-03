using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private Deck deck;
    [SerializeField] private Button button_Answer;
    private GameHandler gameHandler;
    private List<GameObject> availableSlot = new List<GameObject>();
    private List<Transform> deckSlot = new List<Transform>();
    private string answer;

    public string Get { get { return answer; }}

    void Awake() {
        gameHandler = GetComponent<GameHandler>();
    }

    void Start() {
        availableSlot = board.Slots;
        deckSlot = deck.Slots;
        
        SetAnswerButton(button_Answer);
    }

    void Update() {
        ButtonInteraction();
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

    private bool AnswerSet() {
        for(int i = 0; i < availableSlot.Count; i++) {
            BoardSlot slot = availableSlot[i].GetComponent<BoardSlot>();

            if(!slot.HasCard) return false;
        }

        answer = BuildAnswer();
        return true;
    }

    private void SetAnswerButton(Button button) {
        button.gameObject.SetActive(true);

        // Set button position
        Vector3 buttonPosition = deckSlot[deckSlot.Count - 1].position;

        buttonPosition.x += deck.Spacing;
        button.transform.position = buttonPosition;
    }

    private void ButtonInteraction() {
        button_Answer.interactable = AnswerSet();
    }
}
