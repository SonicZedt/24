using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;

public class Formula
{
    public List<int> operands = new List<int>();
    public List<string> operators = new List<string>();
    public string question;
    
    private int result, maxModifier, numberTemp, operandCount;

    public int MaxModifier { 
        get { return maxModifier; }
        set {
        if(value >= result) maxModifier = this.result / 2;
        else maxModifier = value;
        }
    }

    public int OperandCount {
        get { return operandCount; }
        set { this.operandCount = value; }
    }

    public Formula(int result, int maxModifier, int operandCount) {
        this.result = result;
        MaxModifier = maxModifier;
        OperandCount = operandCount;
        this.numberTemp = result;
    }

    public int Result(string formula = null) {
        DataTable dt = new DataTable();

        if(formula == null) formula = question;
        return (int)dt.Compute(formula, " ");
    }

    public void GenerateQuestion() {
        int RandomNumber(List<int> list = null) {
            int number = 0;

            if(list != null) {
                int index = Random.Range(0, list.Count - 1);
                number = list[index];
            }

            if(maxModifier == 0) number = 0;
            else number = Random.Range(1, maxModifier);
            return number;
        }

        void Fix() {
            int gap = result - numberTemp;
            Debug.Log($"gap: {gap} | opr[0] {operands[0]}");
            
            // FIXME: * as first operator causes result mismatch.

            switch (operators[0]) {
                case "*":
                    operands[0] = 1;
                    Debug.Log("Fix action: B");
                    break;
                default:
                    if(operands[0] < result) operands[0] += gap;
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
            Debug.Log("Q: " + question);
            if(Result() != result) Fix();
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

        int Multificator() {
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
        #endregion

        for(int i = 0; i < OperandCount - 1; i++) {
            string opr = null;
            int opd = 0;
            int oprSelector = Random.Range(0, 2);

            switch(oprSelector) {
                case 0:
                    opr = "+";
                    opd = Adder();
                    break;
                case 1:
                    opr = "-";
                    opd = Subtractor();
                    break;
                /* TODO: complete arithmatic
                case 2:
                    opr = "*";
                    opd = Multificator();
                    break;
                case 3:
                    opr = "/";
                    break;
                */
            }

            operands.Add(opd);
            operators.Add(opr);
        }

        operands.Add(numberTemp);
        operands.Reverse();
        operators.Reverse();
        Build();
    }
}
