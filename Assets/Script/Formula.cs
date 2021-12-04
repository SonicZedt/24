using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formula : MonoBehaviour
{
    public int result;

    private int number_temp;
    private string track;

    void Start() {
        number_temp = result;
        int s = Subtractor();
        int a = Adder();
        Debug.Log($"{number_temp} - {s} + {a} = {number_temp - s + a}");
    }

    private int RandomNumber() {
        return Random.Range(0, number_temp);
    }

    private void Track(string sign, int number) {
        string newtrack = $"{number} {sign}";
        track += newtrack;
    }

    #region Operator
    private int Adder() {
        int n = RandomNumber();
        int diff = number_temp - n;
        number_temp = diff;

        return n;
    }

    private int Subtractor() {
        int n = RandomNumber();
        int sum = number_temp + n;
        number_temp = sum;
        
        return n;
    }
    #endregion
}
