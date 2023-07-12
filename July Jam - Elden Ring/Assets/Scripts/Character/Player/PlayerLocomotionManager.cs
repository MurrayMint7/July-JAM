using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;

    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float moveAmount;

    [Header("Movement Settings")]
    private Vector3 moveDirection;
    private Vector3 targetRotationDirection;
    [SerializeField] float walkingSpeed = 1.5f;
    [SerializeField] float runningSpeed = 4.5f;
    [SerializeField] float rotationSpeed = 15;

    [Header("Dodge")]
    private Vector3 rollDirection;


    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    protected override void Update()
    {
        base.Update();

        if(player.IsOwner){
            player.characterNetworkManager.verticalMovement.Value = verticalMovement;
            player.characterNetworkManager.horizontalMovement.Value = horizontalMovement;
            player.characterNetworkManager.moveAmount.Value = moveAmount;
        }
        else{
            verticalMovement = player.characterNetworkManager.verticalMovement.Value;
            horizontalMovement = player.characterNetworkManager.horizontalMovement.Value;
            moveAmount = player.characterNetworkManager.moveAmount.Value;

            //IF NOT LOCKED ON, PASS MOVE AMOUNT
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);

            //IF LOCKED ON PASS IN HORIZONTAL MOVEMENT
        }
    }


    public void HandleAllMovement(){
        //GROUNDED MOVEMENT
        HandleGroundedMovement();
        HandleRotation();
        //AERIAL MOVEMENT
        
    }

    private void GetMovementValues(){
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;
        moveAmount = PlayerInputManager.instance.moveAmount;
        //CLAMP THE MOVEMENTS
    }

    private void HandleGroundedMovement(){

        GetMovementValues();

        if(!player.canMove){
            return;
        }

        //OUR MOVE DIRECTION IS BASED ON OUR CAMERAS FACING PERSPECTIVE & MOVEMENT INPUTS
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if(PlayerInputManager.instance.moveAmount > 0.5f){
            //MOVE AT A RUNNING SPEED
            player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);

        }
        else if(PlayerInputManager.instance.moveAmount <= 0.5f){
            //MOVE AT A WALKING SPEED
            player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);

        }
    }

    private void HandleRotation(){

        if(!player.canRotate){
            return;
        }

        targetRotationDirection = Vector3.zero;
        targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
        targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
        targetRotationDirection.Normalize();
        targetRotationDirection.y = 0;

        if(targetRotationDirection == Vector3.zero){
            targetRotationDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }

    public void AttemptToPerformDodge(){

        if(player.isPerformingAction){
            return;
        }
               

        //IF WE ARE MOVING WHEN WE ATTEMPT TO DODGE, WE PERFORM A ROLL
        if(PlayerInputManager.instance.moveAmount > 0){
            rollDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
            rollDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;
            rollDirection.y = 0;
            rollDirection.Normalize();

            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            player.transform.rotation = playerRotation;

            //PERFORM ROLL ANIM
            player.playerAnimatorManager.PlayTargetActionAnimation("Roll", true, true);
        }

        //IF WE ARE STATIONARY, PERFORM A BACKSTEP
        else{
            //PERFORM BACKSTEP ANIM
            player.playerAnimatorManager.PlayTargetActionAnimation("Backflip", true, true);

        }
        
    }
}
