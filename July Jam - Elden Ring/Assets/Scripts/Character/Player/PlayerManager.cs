
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    protected override void Awake()
    {
        base.Awake();

        //DO MORE STUFF, ONLY FOR PLAYER

        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
    }

    protected override void Update() {
        base.Update();

        //IF WE DO NOT OWN THIS GAMEOBJECT, WE FO NOT CONTROL OR EDIT IT
        if(!IsOwner){
            return;
        }
        //HANDLE ALL OF OUR CHARACTER MOVEMENT
        playerLocomotionManager.HandleAllMovement();
    }

    protected override void LateUpdate()
    {
        if(!IsOwner){
            return;
        }
        base.LateUpdate(); 

        PlayerCamera.instance.HandleAllCameraActions();
    }

    public override void OnNetworkSpawn(){
        base.OnNetworkSpawn();

        //IF THIS IS THE PLAYER OBJECT OWNED BY THIS CLIENT
        if(IsOwner){
            PlayerCamera.instance.player = this;
            PlayerInputManager.instance.player = this;
        }
    }
}
