using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantCharacterEffect : ScriptableObject
{
    [Header("Effect Info")]
    public int instantEffectID;

    public virtual void ProcessEffect(CharacterManager character){
        
    }
}
