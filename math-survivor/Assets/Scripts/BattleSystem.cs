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
    public Button[] buttons;

    public HealthSystem enemyHealth;
    public HealthSystem playerHealth;

    public Questions question;

    public Transform playerBattleStation;//location of the object
    public Transform enemyBattleStation; //for good practice, make another gameobject that is a parent to the character to setup battlestation

    //public Animator animator;

    public Text questionText;
    public Text dialogueText;
    public Text playerHealthText;
    public Text enemyHealthText;
    public bool isAttacking = false;

    [SerializeField]
    public int selectedValue;
    
    void Start()
    {

        //question = new Questions();
        question = gameObject.AddComponent<Questions>();
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        //Debug.Log("Player deals " + playerUnit.damage + " damage!");
    }
    //calls once per frame
    private void Update()
    {
        
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

        enemyHealthText.text = enemyUnit.currentHP.ToString();
        enemyHealth.SetHUD(enemyUnit);
        //maybe give 1-2 sec delay for turn to start
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool attack = true;
        bool isDead = enemyUnit.Damage(playerUnit.damage); //player deals 5 damage (refer to the inspector)
            dialogueText.text = "Correct! you deal " + playerUnit.damage + " damage";
            enemyHealth.SetHealth(enemyUnit.currentHP);
            enemyHealthText.text = enemyUnit.currentHP.ToString();

        playerUnit.CharacterAnimate(attack);

        if (isDead)
        {
            state = BattleState.WON;
            //add winning screen or proceeds to the next levels
            yield return new WaitForSeconds(2f);
            EndBattle();
        }
        else
        {

           

            state = BattleState.ENEMYTURN;
            

            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
            //game over or fight until death
        }
    }

    IEnumerator WrongAnswers()
    {
        dialogueText.text = "Wrong answer! Your attack missed.";
        state = BattleState.ENEMYTURN;
        yield return new WaitForSeconds(2f);
        
        StartCoroutine(EnemyTurn());
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
        foreach (Button button in buttons)
        {
            
            //button.interactable = false; //all buttons is started with false values
            button.GetComponentInChildren<Text>().text = question.FalseAnswers().ToString();
        }

        
        questionText.text = question.Num1 + " + " + question.Num2 + " = " + "???";

        int index = Random.Range(0, buttons.Length);

        //debug
        //buttons[index].interactable = true; //only 1 is correct
        buttons[index].GetComponentInChildren<Text>().text = question.Sum.ToString();
        //checking values
        foreach (Button button in buttons)
        {
            Text buttonText = button.GetComponentInChildren<Text>();
            Debug.Log(button.name + ": " + buttonText.text);
        }
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

        if (state != BattleState.PLAYERTURN)
            return;

        Button selectedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        string buttonText = selectedButton.GetComponentInChildren<Text>().text;

        
        if (int.TryParse(buttonText, out selectedValue))
        {
            if (selectedValue == question.Sum)
            {
                StartCoroutine(PlayerAttack());
            }
            else
            {
                
                StartCoroutine(WrongAnswers());
            }
        }
    }

    
}
