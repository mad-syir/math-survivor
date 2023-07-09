using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    bool gameIsOver = false;

    
    public void DeathScreen()
    {
        if (gameIsOver == false)
        {
            gameIsOver = true;
            Debug.Log("Game Over");
            //restart
        
        }
    }
    
}
