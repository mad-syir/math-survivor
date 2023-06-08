using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Slider slider;
    public void SetHUD(Unit unit)
    {
        slider.maxValue = unit.maxHP;
        slider.value = unit.currentHP;
    }
    public void SetMaxHealth(int health) //feels redundant might discard later
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    

}
