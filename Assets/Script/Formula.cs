using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;

public class Formula
{
    private List<int> operands = new List<int>();
    private List<string> operators = new List<string>();
    private string question;
    private int mark, modifier, operandCount;
    private bool randomModifier, includeMark;
    private int[] modifierRange = new int[2];
    private bool[] operatorsToggle = new bool[4];

    public int OperandCount {
        get { return operandCount; }
        set { this.operandCount = value; }
        }
    public List<int> Operands { get { return operands; }}
    public List<string> Operators { get { return operators; }}
    public string Question { get { return question; }}

    public Formula(int mark, int modifier, int operandCount, bool[] operatorsToggle = null, bool includeMark = false) {
        // Constant modifier
        operatorsToggle ??= EnableAllOperators(operatorsToggle, 4);

        this.includeMark = includeMark;
        this.randomModifier = false;
        this.operatorsToggle = operatorsToggle;
        this.mark = mark;
        this.modifier = modifier;
        this.operandCount = operandCount;
    }

    public Formula(int mark, int[] modifierRange, int operandCount, bool[] operatorsToggle = null, bool includeMark = false) {
        // Random modifier
        operatorsToggle ??= EnableAllOperators(operatorsToggle, 4);

        this.includeMark = includeMark;
        this.randomModifier = true;
        this.operatorsToggle = operatorsToggle;
        this.mark = mark;
        this.modifierRange = modifierRange;
        this.operandCount = operandCount;
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
        // FIXME: Sequentialy generated is bad approach except for addition and substraction
        
        int RandomModifier() {
            return randomModifier ? Random.Range(modifierRange[0], modifierRange[1]++) : modifier;
        }

        int RandomNumber(List<int> list = null) {
            // Return random number on range of 0 - maxModifier if list is null
            // Else return random number from list

            return list == null ? Random.Range(0, RandomModifier()) : list[(int)Random.Range(0, list.Count - 1)];
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
        void CheckNegative(int n, string loc) {
            if(n < 0) Debug.Log($"Negative exist ({mark}) because (n = {n}) in {loc}");
        }

        int Adder() {
            int n = RandomNumber();
            int diff = mark - n;
            mark = diff;
            
            return n;
        }

        int Subtractor() {
            int n = RandomNumber();
            int sum = mark + n;
            mark = sum;
    
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

            int factorMark = includeMark ? mark : RandomModifier();
            int n = RandomNumber(Factors(factorMark));
            int div = mark / n;
            mark = div;

            return n;
        }

        int Divider() {
            List<int> Divisors(int dividend) {
                List<int> divisors = new List<int>();
                float dividendSQRT = Mathf.Sqrt(dividend);
                
                if((dividend <= 0) || (dividend > modifier)) {
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

            int divisorMark = includeMark ? mark : RandomModifier();
            int n = RandomNumber(Divisors(divisorMark));
            int mul = mark * n;
            mark = mul;

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

        int firstOperand = includeMark ? mark : RandomNumber();
        
        // Assign remaining value of mark as first operand if enabled
        operands.Add(firstOperand);
        operands.Reverse();
        operators.Reverse();
        Build();

        return question;
    }
}
