using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;

public class Formula
{
    private List<int> operands = new List<int>();
    private List<string> operators = new List<string>();
    private string question;
    private int mark, maxModifier, numberTemp, operandCount;
    private bool[] operatorsToggle = new bool[4];

    public int OperandCount {
        get { return operandCount; }
        set { this.operandCount = value; }
        }
    public List<int> Operands { get { return operands; }}
    public List<string> Operators { get { return operators; }}
    public string Question { get { return question; }}

    public Formula(int mark, int maxModifier, int operandCount, bool[] operatorsToggle = null) {
        operatorsToggle ??= EnableAllOperators(operatorsToggle, 4);

        this.operatorsToggle = operatorsToggle;
        this.mark = mark;
        this.maxModifier = maxModifier;
        this.operandCount = operandCount;
        this.numberTemp = mark;
    }

    public bool[] EnableAllOperators(bool[] operatorsToggle, int lenght = 4) {
        operatorsToggle ??= new bool[lenght];

        for(int i = 0; i < operatorsToggle.Length; i++) {
            operatorsToggle[i] = true;
        }

        return operatorsToggle;
    }

    public object Result(string formula = null) {
        DataTable dt = new DataTable();

        formula ??= question;
        return dt.Compute(formula, " ");
    }

    public string GenerateQuestion() {
        int RandomNumber(List<int> list = null) {
            // Get random number on range of 0 - maxModifier if list is null
            // Else get random number from list

            return list == null ? Random.Range(0, maxModifier) : list[(int)Random.Range(0, list.Count - 1)];
        }

        void Build() {
            StringBuilder stringBuilder = new StringBuilder();
            question = null;

            for(int i = 0; i < operands.Count; i++) {
                stringBuilder.Append(operands[i]);
                if(i < operators.Count) stringBuilder.Append(operators[i]);
            }
            
            question = stringBuilder.ToString();
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

                if(number_max <= 0) {
                    factors.Add(1);
                    
                    return factors;
                }

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
            // FIXME: Divider still causes result become float
            List<int> Divisors(int dividend) {
                List<int> divisors = new List<int>();
                float dividendSQRT = Mathf.Sqrt(dividend);
                
                if((dividend <= 0) || (dividend > maxModifier)) {
                    divisors.Add(1);
                    
                    return divisors;
                }

                for(int i = 1; i <= dividendSQRT; i++) {
                    if(dividend % i != 0) continue;

                    divisors.Add(i);
                    if(i != (dividend / i)) divisors.Add(dividend / i);
                }

                return divisors;
            }

            int n = RandomNumber(Divisors(numberTemp));
            Debug.Log($"d: {numberTemp} / {n} = {numberTemp/n}");
            int mul = numberTemp * n;
            numberTemp = mul;

            return n;
        }
        #endregion
        
        int OperatorSelector() { // Return operator index (+, -, *, /)
            int operatorIndex = Random.Range(0, 4);

            while(!operatorsToggle[operatorIndex]) {
                operatorIndex = Random.Range(0, 4);
            }

            return operatorIndex;
        }

        for(int i = 0; i < operandCount - 1; i++) {
            string opr = null;
            int opd = 0;
            int operatorSelector = OperatorSelector();

            switch(operatorSelector) {
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

        // FIXME: numberTemp (first operand) sometimes too big
        //if(numberTemp > maxModifier) numberTemp = maxModifier;

        operands.Add(numberTemp);
        operands.Reverse();
        operators.Reverse();
        Build();

        return question;
    }
}
