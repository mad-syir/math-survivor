using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    private Unit playerUnit;
    private Unit enemyUnit;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;//finding the object

    public HealthSystem enemyHealth;
    public HealthSystem playerHealth;

    public Questions question;

    public Transform playerBattleStation;//location of the object
    public Transform enemyBattleStation; //for good practice, make another gameobject that is a parent to the character to setup battlestation

    public Text questionText;
    public Text dialogueText;
    public Text playerHealthText;
    public Text enemyHealthText;
    public bool isAttacking = false;
    void Start()
    {
        question = new Questions();
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        //Debug.Log("Player deals " + playerUnit.damage + " damage!");
    }

    IEnumerator SetupBattle()
    {
        GameObject playerObj = Instantiate(playerPrefab, playerBattleStation); //getting reference of this instance
        playerUnit = playerObj.GetComponent<Unit>();

        GameObject enemyObj = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyObj.GetComponent<Unit>();

        dialogueText.text = "A "+enemyUnit.name + " approaches..";
        playerHealthText.text = playerUnit.currentHP.ToString();
        playerHealth.SetHUD(playerUnit);
        enemyHealth.SetHUD(enemyUnit);
        //maybe give 1-2 sec delay for turn to start
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.Damage(playerUnit.damage); //player deals 5 damage (refer to the inspector)
        dialogueText.text = "you deal " + playerUnit.damage + " damage";
        enemyHealth.SetHealth(enemyUnit.currentHP);
        //todo
        if (isDead)
        {
            state = BattleState.WON;
            //add winning screen or proceeds to the next levels
            yield return new WaitForSeconds(2f);
            EndBattle();
        }
        else
        {
            
            dialogueText.text = "you deal " + playerUnit.damage + " damage";

            state = BattleState.ENEMYTURN;
            

            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
            //game over or fight until death
        }
    }
    IEnumerator EnemyTurn()
    {
        //we can put logic to the enemy ai here
        dialogueText.text = enemyUnit.name + " attacks!";

        yield return new WaitForSeconds(1f);
        bool isDead = playerUnit.Damage(enemyUnit.damage); //checking the status of the player
        playerHealth.SetHealth(playerUnit.currentHP);
        playerHealthText.text = playerUnit.currentHP.ToString();
        yield return new WaitForSeconds(1f);
        if (isDead)
        {
            state = BattleState.LOST;
            //add lost screen or return to main menu
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            //isAttacking = true;
            PlayerTurn();
            
            //game over or fight until death
        }
    }
    public void PlayerTurn()
    {
        dialogueText.text = "Choose your action...";
        question.RandomAddition();
        questionText.text = question.Num1 + " + " + question.Num2 + " + " + question.Sum + "(answer)";
    }

    public void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You WON!!";
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "You LOST!!!!";
        }
        //make it so that it travels to the next scene/level
    }
    public void OnAttackButton()
    {
        //a selection of choices only 1 yields true and damages the enemy
        //make player unable to attack after execute it once

        if(state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerAttack());

    }
}
