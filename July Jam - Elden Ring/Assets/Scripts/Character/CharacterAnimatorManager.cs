using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;

    float vertical;
    float horizontal;

    protected virtual void Awake(){
        character = GetComponent<CharacterManager>();
    }
    
    public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue){
        character.animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
        character.animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);
    }

    public virtual void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false){
        character.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f);

        //CAN BE USED TO STOP CHARACTER FROM ATTEMPTING NEW ACTIONS
        //IF YOU GET DAMAGED AND BEGIN PERFORMING DAMAGE ANIMATION
        //THIS FLAG WILL TURN TRUE IF YOU ARE STUNNED
        //WE CAN THEN CHECK FOR THIS BEFORE ATTEMPTING NEW ACTIONS
        character.isPerformingAction = isPerformingAction;
        character.canRotate = canRotate;
        character.canMove = canMove;

        //TELL THE SERVER/HOST THAT WE PLAYED AN ANIMATION, AND TO PLAY THE ANIM FOR EVERYONE ELSE
        character.characterNetworkManager.NotifyTheServerOfActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
    }
}
