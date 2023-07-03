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

        moveDir = cameraObject.forward * inputHandler.vertical;
        moveDir += cameraObject.right * inputHandler.horizontal;
        moveDir.Normalize();

        float speed = movementSpeed;
        moveDir *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDir, normalVector);
        rigidbody.velocity = projectedVelocity;

        if(animHandler.canRotate){
            HandleRotation(delta);
        }
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

    #endregion

}

