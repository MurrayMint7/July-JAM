using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHUDManager : MonoBehaviour
{
    [SerializeField] UI_StatBar healthBar;
    [SerializeField] UI_StatBar staminaBar;

    public void SetNewHealthValue(float oldValue, float newValue){
        healthBar.SetStat(Mathf.RoundToInt(newValue));
    }

    public void SetMaxHealthValue(int maxHealth){
        healthBar.SetMaxStat(maxHealth);
    }

    public void SetNewStaminaValue(float oldValue, float newValue){
        staminaBar.SetStat(Mathf.RoundToInt(newValue));
    }

    public void SetMaxStaminaValue(int maxStamina){
        staminaBar.SetMaxStat(maxStamina);
    }

    
}
