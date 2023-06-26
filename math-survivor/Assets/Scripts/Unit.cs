using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Animator animator;
    
    public new string name;
    public int damage;
    public int maxHP;
    public int currentHP;

    
    public bool Damage(int damage)
    {
        currentHP -= damage;

        if(currentHP <= 0)
        {
            currentHP = 0;
            return true; //checking if the enemy is dead
        }
        else
        {
            return false;
        }
    }
    public void CharacterSlash(bool damaged)
    {
        if(damaged){
            animator.SetBool("attack", true);
            return;
        }
        else
        {
            animator.SetBool("attack", false);
        }
        
        //animator.SetBool("attack", false);

    }
    public void CharacterDied()
    {
        animator.SetBool("Dead", true);
        

        //animator.SetBool("attack", false);

    }
}
