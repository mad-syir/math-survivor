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
        SetupBattle();
    }

    public void SetupBattle()
    {
        GameObject playerObj = Instantiate(playerPrefab, playerBattleStation); //getting reference of this instance
        playerUnit = playerObj.GetComponent<Unit>();

        GameObject enemyObj = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyObj.GetComponent<Unit>();

        dialogueText.text = "A "+enemyUnit.name + " approaches..";

        playerHealth.SetHUD(playerUnit);
        enemyHealth.SetHUD(enemyUnit);

    }
}
