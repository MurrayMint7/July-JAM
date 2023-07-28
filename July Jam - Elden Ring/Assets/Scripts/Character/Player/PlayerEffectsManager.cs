using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : CharacterEffectsManager
{
    [Header("Debug Delete Later")]
    [SerializeField] InstantCharacterEffect testEffect;
    [SerializeField] bool processEffect = false;

    private void Update(){
        if(processEffect){
            processEffect = false;       
            InstantCharacterEffect effect = Instantiate(testEffect);
            ProcessInstantEffect(testEffect);
        }       
    }
}
