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

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
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
    public void CharacterAnimate(bool damaged)
    {
        animator.SetBool("attack", true);
        
    }
}
