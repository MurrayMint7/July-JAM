using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Stamina Damage")]
public class TakeStaminaDamageEffect : InstantCharacterEffect
{
    public float staminaDamage;
    
    public override void ProcessEffect(CharacterManager character){
        CalculateStaminaDamage(character);
    }

    private void CalculateStaminaDamage(CharacterManager character){
        //COMPARED THE BASE STAMINA DAMAGE AGAINST OTHER PLAYER EFFECTS/MODIFIERS
        //CHANGE THE VALUE BEFORE SUBTRACTING/ADDING IT
        //PLAY SOUND FX OR VISUAL FX DURING EFFECT

        if(character.IsOwner)
        {
            Debug.Log("Stamina Damage: " + staminaDamage);
            character.characterNetworkManager.currentStamina.Value -= staminaDamage;
        }

    }
}
