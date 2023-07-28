using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    CharacterManager character;

    protected virtual void Awake(){
        character = GetComponent<CharacterManager>();
    }

    //PROCESSING INSTANT EFFECTS (TAKE DMG, HEAL)

    public virtual void ProcessInstantEffect(InstantCharacterEffect effect){
        effect.ProcessEffect(character);
    }

    //PROCESS TIMED EFFECTS (POISON, BLEED, FROST, ETC.)

    //PROCESS STATIC EFFECTS (ADDING/REMOVING BUFFS/DEBUFFS)
}
