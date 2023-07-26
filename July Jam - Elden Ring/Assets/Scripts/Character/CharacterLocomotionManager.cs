using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Ground & Jumping")]
    [SerializeField] float gravityForce = -5.55f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckSphereRadius = 1;
    [SerializeField] protected Vector3 yVelocity; // THE FORCE AT WHICH THE CHRACTER IS PULLED UP OR DOWN
    [SerializeField] protected float groundedYVelocity = -20; //THE FORCE AT WHICH THE CHARACTER IS PULLED DOWN WHEN GROUNDED
    [SerializeField] protected float fallStartYVelocity = -5; //THE FORCE AT WHICH THE CHARACTER IS PULLED DOWN WHEN FALLING
    protected bool fallingVelocityHasBeenSet = false; 
    protected float inAirTimer = 0;

    protected virtual void Awake(){
        character = GetComponent<CharacterManager>();
    }

    protected virtual void Update(){
        HandleGroundCheck();

        if(character.isGrounded){
            
            //IF WE ARE NOT ATTEMPTING TO JUMP OR MOVE UPWARD
            if(yVelocity.y < 0){
                inAirTimer = 0;
                fallingVelocityHasBeenSet = false;
                yVelocity.y = groundedYVelocity;
            }    
        }
        else{
            //IF WE ARE NOT JUMPING, AND OUR FALLING VELOCUTY HAS NOT BEEN SET
            if(character.isJumping && !fallingVelocityHasBeenSet){
                fallingVelocityHasBeenSet = true;
                yVelocity.y = fallStartYVelocity;
            }

            inAirTimer += Time.deltaTime;
            character.animator.SetFloat("inAirTimer", inAirTimer);

            yVelocity.y += gravityForce * Time.deltaTime;
        }

        //THERE SHOULD ALWAYS BE SOME FORCE APPLIED TO THE Y VEL 
        character.characterController.Move(yVelocity * Time.deltaTime);

    }

    protected void HandleGroundCheck(){
        character.isGrounded = Physics.CheckSphere(character.transform.position,groundCheckSphereRadius , groundLayer);
    }

    //DRAWS A GROUND CHECK SPHERE IN THE EDITOR
    protected void OnDrawGizmosSelected() {
        Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
    }
}
