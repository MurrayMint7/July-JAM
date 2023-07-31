using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Damage")]
public class TakeDamageEffect : InstantCharacterEffect
{
    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage; //If the damage is caused by another characters attack it will be stored here

    [Header("Character Causing Damage")]  
    public float physicalDamage = 0; //IN THE FUTURE, WILL BE SPLIT INTO "STANDARD", "STRIKE", "PIERCE", "SLASH" ETC
    public float magicDamage = 0;
    public float fireDamage = 0;
    public float lightningDamage = 0;
    public float holyDamage = 0;

    [Header("Final Damage")]
    public int finalDamageDealt = 0; //THE DMG THE CHARACTER TAKES AFTER ALL CALCULATIONS HAVE BEEN MADE

    [Header("Poise")]
    public float poiseDamage = 0;
    public bool poiseIsBroken = false; //IF THE POISE IS BROKEN, THE CHARACTER WILL BE STUNNED

    //BUILD UPS (TODO: IMPLEMENT THESE)
    //
    
    [Header("Aninmation")]
    public bool playDamageAnimation = true;
    public bool manuallySelectDamageAnimation = false;
    public string damageAnimation;

    [Header("Sound FX")]
    public bool willPlayDamageSFX = true;
    public AudioClip elementalDamageSFX; //USED ONTOP OF REGULAR SFX IF ELEMENTAL DMG IS PRESENT

    [Header("Direction Damage Taken From")]
    public float angleHitFrom; //DETERMINE WHAT DAMAGE ANIMATION TO PLAY BASED ON THIS
    public Vector3 contactPoint; //USED TO DETERMINE WHERE THE BOOLD FX WILL SPAWN
    
    public override void ProcessEffect(CharacterManager character){
        base.ProcessEffect(character);

        //IF THE CHARACTER IS DEAD, NO ADDITIONAL DAMAGE EFFECTS SHOULD BE PROCESSED
        if(character.isDead.Value){
            return;
        }

        //CHECK FOR INVULNERABILITY

        //CALCULATE DAMAGE
        CalculateDamage(character);
        //CHECK WHICH DIRECTION THE DMG CAME FROM
        //PLAY THE DAMAGE ANIMATION
        //CHECK FOR BUILD UPS (POISON, BLEED, FROST, ETC)
        //PLAY DMG SFX
        //SPAWN BLOOD FX

        //IF CHARACTER IS AIM CHECK FOR NEW TARGET IF CHARACTER CAUSING DAMAGE IS PRESENT
    }

    private void CalculateDamage(CharacterManager character){

        if(!character.IsOwner){
            return;
        }
        if(characterCausingDamage != null){
            //CHECK FOR DMG MODIFIERS AND MODIFY BASE DMG
        }

        //CHECK CHARACTER FOR FLAT DEFENCES AND SUBTRACT THEM FROM DAMAGE

        //CHECK FOR CHARACTER ARMOR ABSORBTION AND SUBTRACT THE PERCENTAGE FROM DAMAGE

        //ADD ALL DAMAGE TYPES TOGETHER TO GET FINAL DAMAGE
        finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + fireDamage + lightningDamage + holyDamage);

        if(finalDamageDealt <= 0){
            finalDamageDealt = 1;
        }

        character.characterNetworkManager.currentHealth.Value -= finalDamageDealt;

        //CALCULATE POISE DAMAGE TO DETERMINE IF THE CHARACTER WILL BE STUNNED
    }
}
