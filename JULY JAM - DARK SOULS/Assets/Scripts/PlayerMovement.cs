using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform cameraObject;
    InputHandler inputHandler;
    Vector3 moveDir;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public AnimatorHandler animHandler;

    public new Rigidbody rigidbody;
    public GameObject normalCamera;

    [Header("Stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float rotationSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        animHandler = GetComponentInChildren<AnimatorHandler>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
        animHandler.Initialise();
    }

    public void Update(){
        float delta = Time.deltaTime;

        inputHandler.TickInput(delta);
        HandleMovement(delta);
        HandleRollingAndSprinting(delta);
    }

    #region Movement
    Vector3 normalVector;
    Vector3 targetPos;

    private void HandleRotation(float delta){
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputHandler.moveAmount;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if(targetDir == Vector3.zero){
            targetDir = myTransform.forward;
        }
        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;
    }
    
    public void HandleMovement(float delta){
        moveDir = cameraObject.forward * inputHandler.vertical;
        moveDir += cameraObject.right * inputHandler.horizontal;
        moveDir.Normalize();
        moveDir.y = 0;

        float speed = movementSpeed;
        moveDir *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDir, normalVector);
        rigidbody.velocity = projectedVelocity;

        animHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

        if(animHandler.canRotate){
            HandleRotation(delta);
        }
    }

    public void HandleRollingAndSprinting(float delta){
        if(animHandler.anim.GetBool("isInteracting")){
            return;
        }
        if(inputHandler.rollFlag){
            moveDir = cameraObject.forward * inputHandler.vertical;
            moveDir += cameraObject.right * inputHandler.horizontal;

            if(inputHandler.moveAmount > 0){
                animHandler.PlayerTargetAnimation("Roll", true);
                moveDir.y = 0;
                Quaternion rollRot = Quaternion.LookRotation(moveDir);
                myTransform.rotation = rollRot;
            }
            else{
                animHandler.PlayerTargetAnimation("Backflip", true);
            }
        }
    }

    #endregion

}

