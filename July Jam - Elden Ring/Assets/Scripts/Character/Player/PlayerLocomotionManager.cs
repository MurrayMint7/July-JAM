using System;
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
    [SerializeField] float runningSpeed = 2.5f;
    [SerializeField] float sprintingSpeed = 4.5f;
    [SerializeField] float rotationSpeed = 15;
    [SerializeField] float sprintingStaminaCost = 2;

    [Header("Jumping")]
    [SerializeField] float jumpHeight = 4;
    [SerializeField] float jumpStaminaCost = 10;
    [SerializeField] float jumpForwardSpeed = 2.5f;
    [SerializeField] float freeFallSpeed = 1.5f;
    private Vector3 jumpDirection;


    [Header("Dodge")]
    private Vector3 rollDirection;
    [SerializeField] float dodgeStaminaCost = 10;


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
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);

            //IF LOCKED ON PASS IN HORIZONTAL MOVEMENT
        }
    }


    public void HandleAllMovement(){
        //GROUNDED MOVEMENT
        HandleGroundedMovement();
        HandleRotation();
        //AERIAL MOVEMENT
        HandleAerialMovement();
        HandleFreeFallMovement();
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

        if(player.playerNetworkManager.isSprinting.Value){
            player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
        }
        else{

            if(PlayerInputManager.instance.moveAmount > 0.5f){
                //MOVE AT A RUNNING SPEED
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if(PlayerInputManager.instance.moveAmount <= 0.5f){
                //MOVE AT A WALKING SPEED
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);

            }
        }       
    }

    private void HandleAerialMovement(){
        if(player.isJumping){
            player.characterController.Move(jumpDirection * jumpForwardSpeed * Time.deltaTime);
        }
    }

    private void HandleFreeFallMovement(){
        if(!player.isGrounded){
            Vector3 freeFallDirection;
            
            freeFallDirection = PlayerCamera.instance.transform.forward * PlayerInputManager.instance.verticalInput;
            freeFallDirection = freeFallDirection + PlayerCamera.instance.transform.right * PlayerInputManager.instance.horizontalInput;
            freeFallDirection.y = 0;

            player.characterController.Move(freeFallDirection * freeFallSpeed * Time.deltaTime);
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

    public void HandleSprinting(){
        if(player.isPerformingAction){
            //SET SPRINTING TO FALSE
            player.playerNetworkManager.isSprinting.Value = false;
        }

        //IF WE ARE OUT OF STAMINA, SET SPRINTING TO FALSE
        if(player.playerNetworkManager.currentStamina.Value <= 0){
            player.playerNetworkManager.isSprinting.Value = false;
            return;
        }

        //IF WE ARE MOVING SET SPRINTING TO TRUE
        if(moveAmount >= 0.5){
            player.playerNetworkManager.isSprinting.Value = true;
        }
        //IF WE ARE STATIONARY/MOVING SLOWLY SET SPRINTING TO FALSE
        else{
            player.playerNetworkManager.isSprinting.Value = false;
        }

        if(player.playerNetworkManager.isSprinting.Value){
            player.playerNetworkManager.currentStamina.Value -= sprintingStaminaCost * Time.deltaTime;
        }
        
    }

    public void AttemptToPerformDodge(){

        if(player.isPerformingAction){
            return;
        }

        if (player.playerNetworkManager.currentStamina.Value <= 0) {
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
            player.playerAnimatorManager.PlayTargetActionAnimation("Backstep", true, true);

        }

        player.playerNetworkManager.currentStamina.Value -= dodgeStaminaCost;
        
    }
    
    public void AttemptToPerformJump(){

        //IF WE ARE PERFORMING AN ACTION, WE DONT WANT TO BE ABLE TO JUMP (WILL CHANGE WHEN COMBAT IS ADDED)
        if(player.isPerformingAction){
            return;
        }

        //IF WE ARE OUT OF STAMINA, WE DO NOT WANT TO JUMP
        if (player.playerNetworkManager.currentStamina.Value <= 0) {
            return;
        }

        //IF WE ARE ALREADY IN A JUMP, WE DO NOT WANT TO JUMP
        if(player.isJumping){
            return;
        }

        //IF WE ARE NOT GROUNDED, WE DO NOT WANT TO ALLOW A JUMP
        if(!player.isGrounded){
            return;
        }

        //IF WE ARE TWO HANDING OUR WEAPON, PLAY TWO HANDED JUMP ANIM, OTHERWISE PLAY ONE HANDED ANIM (TO-DO)
        player.playerAnimatorManager.PlayTargetActionAnimation("Main_Jump_Start", false);

        player.isJumping = true;
        player.playerNetworkManager.currentStamina.Value -= jumpStaminaCost;  

        jumpDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
        jumpDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;

        jumpDirection.y = 0;

        if(jumpDirection != Vector3.zero){
            //IF WE ARE SPRINTING, JUMP DIRECTION IS AT FULL DISTANCE
            if(player.playerNetworkManager.isSprinting.Value){
                jumpDirection *= 1;
            }         

            //IF WE ARE RUNNING, JUMP AT HALF DISTANCE
            else if(PlayerInputManager.instance.moveAmount > 0.5){
                jumpDirection *= 0.5f;
            }

            //IF WE ARE WALKING JUMP AT QUARTER DISTANCE
            else if(PlayerInputManager.instance.moveAmount <= 0.5){
                jumpDirection *= 0.25f;
            }
        }
        
    }

    public void ApplyJumpingVelocity(){
        //APPLY AN UPWARD VELOCITY
        yVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce);
    }

}
