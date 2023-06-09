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

    public Transform playerBattleStation;//location of the object
    public Transform enemyBattleStation; //for good practice, make another gameobject that is a parent to the character to setup battlestation

    public Text dialogueText;
    void Start()
    {
        
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
        
        if (isDead)
        {
            state = BattleState.WON;
            //add winning screen or proceeds to the next levels
            yield return new WaitForSeconds(2f);
            //EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(2f);
            //coroutine enemyturn()
            //game over or fight until death
        }
    }
    public void PlayerTurn()
    {
        dialogueText.text = "Choose your action...";
        
    }
}
