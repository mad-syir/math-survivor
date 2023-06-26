using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Questions : MonoBehaviour
{
    //make a simple math questions
    int num1, num2, num3, sum, falseSum;
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
    public int FalseSum
    {
        get { return falseSum; }
        set { falseSum = value; }
    }

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

        Num1 = Random.Range(0, 10); //you can change difficulty by changing the values of 10 to something else
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

    public int FalseAnswers()
    {
        int number1 = Random.Range(0, 50);
        int number2 = Random.Range(0, 10);
        FalseSum = number1 + number2;
        if (FalseSum == Sum)
            FalseAnswers(); //recursion
        return FalseSum;
    }

}
