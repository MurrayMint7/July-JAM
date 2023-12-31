using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public Animator anim;
    public InputHandler inputHandler;
    public PlayerMovement playerMovement;
    int horizontal;
    int vertical;
    public bool canRotate;

    public void Initialise(){
        anim = GetComponent<Animator>();
        inputHandler = GetComponentInParent<InputHandler>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement){
        #region Vertical
        float v = 0; 
        if(verticalMovement > 0 && verticalMovement < 0.55f){
            v = 0.5f;
        }
        else if(verticalMovement > 0.55f){
            v = 1;
        }
        else if(verticalMovement < 0 && verticalMovement > -0.55f){
            v = -0.5f;
        }
        else if (verticalMovement < -0.55f){
            v = -1;
        }
        else{
            v = 0;
        }
        #endregion

        #region Horizontal
        float h = 0; 
        if(horizontalMovement > 0 && horizontalMovement < 0.55f){
            h = 0.5f;
        }
        else if(horizontalMovement > 0.55f){
            h = 1;
        }
        else if(horizontalMovement < 0 && horizontalMovement > -0.55f){
            h = -0.5f;
        }
        else if (horizontalMovement < -0.55f){
            h = -1;
        }
        else{
            h = 0;
        }
        #endregion

        anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);

    }

    public void PlayerTargetAnimation(string targetAnim, bool isInteracting){
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    public void CanRotate(){
        canRotate = true;
    }

    public void StopRotation(){
        canRotate = false;
    }

    private void OnAnimatorMove(){
        if(inputHandler.isInteracting == false){
            return;
        }
        float delta = Time.deltaTime;
        playerMovement.rigidbody.drag = 0;
        Vector3 deltaPos = anim.deltaPosition;
        deltaPos.y = 0;
        Vector3 velocity = deltaPos / delta;
        playerMovement.rigidbody.velocity = velocity;
    }
}
