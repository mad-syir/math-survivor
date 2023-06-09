using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string name;
    public int damage;
    public int maxHP;
    public int currentHP;

    public bool Damage(int damage)
    {
        currentHP -= damage;

        if(currentHP <= 0)
        {
            /*currentHP = 0;*/
            return true; //checking if the enemy is dead
        }
        else
        {
            return false;
        }
    }
}
