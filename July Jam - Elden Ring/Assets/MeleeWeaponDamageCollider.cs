using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponDamageCollider : DamageCollider
{
    [Header("Attacking Character")]
    public CharacterManager characterCausingDamage; //WHEN CALCULATING DAMAGE THIS IS USED TO CHECK FOR ATTACKERS DAMAGE MODIFIERS, EFFEFCTS ETC
    
}
