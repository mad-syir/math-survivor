using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    //make a simple math questions
    int num1, num2, num3, sum;
    public int Num1
    {
        get { return num1; }
        set { num1 = value; }
    }
    public int Num2
    {
        get { return num2; }
        set { num2 = value; }
    }
    public int Sum
    {
        get { return sum; }
        set { sum = value; }
    }

    private void Start()
    {
        //Debug.Log("sum is: " + RandomAddition());
    }

    /* public int GetNum3()
     {
         return num3;
     }*/
    public int RandomAddition()
    {

        Num1 = Random.Range(0, 10);
        Num2 = Random.Range(0, 10);
        //int num3 = Random.Range(0, 10);

        sum = Num1 + Num2;
        return sum;
    }
    public int RandomSubtraction()
    {

        Num1 = Random.Range(0, 10);
        Num2 = Random.Range(0, 10);
        //int num3 = Random.Range(0, 10);

        sum = Num1 - Num2;
        if (sum >= 0)
        {
            return sum;
        }
        else
        {
            return RandomSubtraction(); //recursion if negative num
        }
    }


}
