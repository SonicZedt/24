using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;

public class Formula
{
    public int result;
    public List<int> operands = new List<int>();
    public List<string> operators = new List<string>();
    public string question;

    private int number_temp;

    public Formula(int result = 24) {
        this.result = result;
        this.number_temp = this.result;
    }

    public void SetResult(int result) {
        this.result = result;
        this.number_temp = this.result;
    }

    private int RandomNumber() {
        return Random.Range(0, number_temp);
    }

    public int Compute(string formula) {
        // Return result of formula
        DataTable dt = new DataTable();
        return (int)dt.Compute(formula, " ");
    }

    public void GenerateQuestion(int operandCount = 4) {
        List<int> operands_temp = new List<int>();
        List<string> operators_temp = new List<string>();
        
        #region Operator
        int Adder() {
            int n = RandomNumber();
            int diff = number_temp - n;
            number_temp = diff;

            return n;
        }

        int Subtractor() {
            int n = RandomNumber();
            int sum = number_temp + n;
            number_temp = sum;

            return n;
        }
        #endregion

        void Reverse() {
            operands.Add(number_temp);
            for(int i = operands_temp.Count - 1; i >= 0; i--) {
                operands.Add(operands_temp[i]);
                operators.Add(operators_temp[i]);
            }
        }

        void Build() {
            StringBuilder stringBuilder = new StringBuilder();

            for(int i = 0; i < operands.Count; i++) {
                stringBuilder.Append(operands[i]);
                if(i < operators.Count) stringBuilder.Append(operators[i]);
            }
            
            question = stringBuilder.ToString();
        }

        for(int i = 0; i < operandCount - 1; i++) {
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
                    break;
                case 3:
                    opr = "/";
                    break;
                */
            }

            operands_temp.Add(opd);
            operators_temp.Add(opr);
        }

        Reverse();
        Build();
    }
}
