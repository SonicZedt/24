using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;

public class Formula
{
    public List<int> operands = new List<int>();
    public List<string> operators = new List<string>();
    public string question;
    
    private int mark, maxModifier, numberTemp, operandCount;

    public int MaxModifier { 
        get { return maxModifier; }
        set { this.maxModifier = (value >= mark) | (value == 0) ? mark : value; }
        }
    public int OperandCount {
        get { return operandCount; }
        set { this.operandCount = value; }
        }

    public Formula(int mark, int maxModifier, int operandCount) {
        this.mark = mark;
        MaxModifier = maxModifier;
        OperandCount = operandCount;
        this.numberTemp = mark;
    }

    public int Result(string formula = null) {
        DataTable dt = new DataTable();

        formula ??= question;
        return (int)dt.Compute(formula, " ");
    }

    public string GenerateQuestion() {
        int RandomNumber(List<int> list = null) {
            // Get random number from list if list isn't null
            return list == null ? Random.Range(1, maxModifier) : list[Random.Range(0, list.Count - 1)];
        }

        void Fix() {
            int gap = mark - numberTemp;
            Debug.Log($"gap: {gap} | opr[0] {operands[0]}");
            
            // FIXME: * as first operator causes result mismatch.

            switch (operators[0]) {
                case "*":
                    operands[0] = 1;
                    Debug.Log("Fix action: B");
                    break;
                default:
                    if(operands[0] < mark) operands[0] += gap;
                    else operands[0] -= gap;
                    Debug.Log("Fix action: C");
                    break;
            }
            
            Build();
        }

        void Build() {
            StringBuilder stringBuilder = new StringBuilder();
            question = null;

            for(int i = 0; i < operands.Count; i++) {
                stringBuilder.Append(operands[i]);
                
                if(i >= operators.Count) break;
                stringBuilder.Append(operators[i]);
            }
            
            question = stringBuilder.ToString();
            //Debug.Log("Q: " + question);
            //if(Result() != mark) Fix();
        }

        #region Operator
        int Adder() {
            int n = RandomNumber();
            int diff = numberTemp - n;
            numberTemp = diff;

            return n;
        }

        int Subtractor() {
            int n = RandomNumber();
            int sum = numberTemp + n;
            numberTemp = sum;

            return n;
        }

        int Multiplicator() {
            List<int> Factors(int number) {
                List<int> factors = new List<int>();
                int number_max = (int)Mathf.Sqrt(number);

                for(int i = 1; i <= number_max; ++i) {
                    if(number % i == 0) {
                        factors.Add(i);
                        if(i == number/i) continue;
                        factors.Add(number/i);
                    }
                }

                return factors;
            }

            int n = RandomNumber(Factors(numberTemp));
            int div = numberTemp / n;
            numberTemp = div;

            return n;
        }

        int Divider() {
            return 1;
        }
        #endregion

        for(int i = 0; i < OperandCount - 1; i++) {
            string opr = null;
            int opd = 0;
            int oprSelector = Random.Range(0, 3);

            switch(oprSelector) {
                case 0:
                    opr = "+";
                    opd = Adder();
                    break;
                case 1:
                    opr = "-";
                    opd = Subtractor();
                    break;
                case 2:
                    opr = "*";
                    opd = Multiplicator();
                    break;
                case 3:
                    opr = "/";
                    opd = Divider();
                    break;
            }

            operands.Add(opd);
            operators.Add(opr);
        }

        operands.Add(numberTemp);
        operands.Reverse();
        operators.Reverse();
        Build();

        return question;
    }
}
