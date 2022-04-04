using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [HideInInspector] public List<Card> cards = new List<Card>();
    
    [SerializeField] private new CameraController camera;
    [SerializeField] private NotifyResult notifyResult;
    [SerializeField] private Button button_Continue;
    [HideInInspector] [SerializeField] private int operandCount;
    [HideInInspector] [SerializeField] private int modifier, mark, minMark, maxMark, minModifier, maxModifier;
    [HideInInspector] [SerializeField] private bool includeMark, randomMark, randomModifier, naturalNumber;
    [HideInInspector] [SerializeField] private bool[] operatorsToggle = new bool[4];
    private object expectedResult;
    private Input input;
    private List<int> operands = new List<int>();
    private List<string> operators = new List<string>();
    private Answer answer;

    #region Getter & Setter
    public bool IncludeMark {
        get { return includeMark; }
        set { this.includeMark = value; }
        }
    public bool RandomMark {
        get { return randomMark; }
        set { this.randomMark = value; }
        }
    public int Mark {
        get { return mark; }
        set { this.mark = value; }
        }
    public int MinMark { 
        get { return minMark; }
        set { this.minMark = value; }
        }
    public int MaxMark { 
        get { return maxMark == 0 ? 24 : maxMark; }
        set { this.maxMark = value; }
        }
    public int OperandCount {
        get { return operandCount; }
        set { this.operandCount = value; }
        }
    public bool RandomModifier {
        get { return randomModifier; }
        set { this.randomModifier = value; }
        }
    public int Modifier { 
        get { return modifier; }
        set { this.modifier = value; }
        }
    public int MinModifier {
        get { return minModifier; }
        set { this.minModifier = value; }
        }
    public int MaxModifier {
        get { return maxModifier; }
        set { this.maxModifier = value; }
        }
    public bool[] OperatorsToggle {
        get { return operatorsToggle; }
        set { this.operatorsToggle = value; }
        }
    public bool NaturalNumber {
        get { return naturalNumber; }
        set { this.naturalNumber = value; }
        }
    public List<int> Operands { get { return operands; }}
    public List<string> Operators { get { return operators; }}
    public object ExpectedResult { get { return expectedResult; }}
    #endregion

    void Awake() {
        input = GetComponent<Input>();
        answer = GetComponent<Answer>();

        GenerateFormula();
    }

    private void GenerateFormula() {
        Formula formula;

        // Disable continue button
        if(button_Continue.gameObject.activeSelf) button_Continue.gameObject.SetActive(false);

        int RandomMark() {
            return (int)Random.Range(minMark, maxMark);
        }

        if(randomMark) mark = RandomMark();
        
        if(randomModifier) {
            int[] modifierRange = {minModifier, maxModifier};
            formula = new Formula(
                mark,
                modifierRange,
                operandCount,
                operatorsToggle,
                includeMark,
                naturalNumber
            );
            Debug.Log($"Generated question. M{mark}, m{modifierRange}, OD{operandCount}");
        }
        else {
            formula = new Formula(
                mark,
                modifier,
                operandCount,
                operatorsToggle,
                includeMark,
                naturalNumber
            );
            Debug.Log($"Generated question. M{mark}, m{modifier}, OD{operandCount}");
        }

        string question = formula.GenerateQuestion();
        operands = formula.Operands;
        operators = formula.Operators;
        expectedResult = formula.Result();
        
        Debug.Log($"{question} = {expectedResult}");
        Debug.Log("=======================");
    }

    public void Continue() {
        // Continue the game by generate new formula
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void CheckAnswer() {
        DataTable dt = new DataTable();
        object resultGiven = dt.Compute(answer.Get, " ");
        bool result = resultGiven.ToString() == expectedResult.ToString();

        notifyResult.ShowNotification(resultGiven.ToString());
        Debug.Log($"answer: {resultGiven} {result}");

        // Enable continue button if answer is correct
        if(result) button_Continue.gameObject.SetActive(true);
    }

    public void ResetAnswer() {
        // Send all card on board to deck
        foreach(Card card in cards) card.BackToDeck();
        Debug.Log("Reset answer");
    }
}
