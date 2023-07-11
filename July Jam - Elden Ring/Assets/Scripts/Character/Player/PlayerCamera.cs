using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public PlayerManager player;
    public Camera cameraObject;
    [SerializeField] Transform cameraPivotTransform;


    //CHANGE THESE TO TWEAK CAMERA PERFORMANCE
    [Header("Camera Settings")]
    private float cameraSmoothSpeed = 1; //THE BIGGER THIS NUMBER, THE LONGER THE CAMERA WILL TAKE TO REACH ITS POSITION
    [SerializeField] float leftAndRightRotationSpeed = 220;
    [SerializeField] float upAndDownRotationSpeed = 220;
    [SerializeField] float minimumPivot = -30; //LOWEST POINT YOU ARE ABLE TO LOOK DOWN
    [SerializeField] float maximumPivot = 60; //HIGHEST POINT YOURE ABLE TO LOOK UP
    [SerializeField] float cameraCollisionRadius = 0.2f; //HIGHEST POINT YOURE ABLE TO LOOK UP
    [SerializeField] LayerMask collideWithLayers; //HIGHEST POINT YOURE ABLE TO LOOK UP

    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition; //USED FOR CAMERA COLLISIONS (MOVES CAMERA TO THIS POSITION UPON COLLIDING)
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;
    private float cameraZPosition; //VALUES USED FOR CAMERA COLLSIONS
    private float targetCameraZPosition; //VALUES USED FOR CAMERA COLLISION

    private void Awake() {
        if(instance == null){

            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);
        cameraZPosition = cameraObject.transform.localPosition.z;
    }

    public void HandleAllCameraActions(){
        if(player != null){
            //FOLLOW THE PLAYER
            HandleFollowTarget();
            //ROTATE AROUND THE PLAYER
            HandleRotation();
            //COLLIDE WITH THE ENVIRNOMENT
            HandleCollisions();
        }
       
    }

    private void HandleFollowTarget(){
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    private void HandleRotation(){
        //IF LOCKED ON FORCE ROTATION TOWARDS TARGET
        //ELSE ROTATE REGULARLY

        //NORMAL ROTATIONS
        //ROTATE LEFT AND RIGHT BASED ON HORIZONTAL MOVEMENT ON THE RIGHT JOYSTICK
        leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;

        //ROTATE UP AND DOWN BASED ON VERTICAL MOVEMENT ON THE RIGHT JOYSTICK
        upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;

        //CLAMP THE UP AND DOWN LOOK ANGLE BETWEEN A MIN AND MAX VALUE
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;

        //ROTATE THIS GAMEOBJECT LEFT AND RIGHT
        cameraRotation.y = leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        //ROTATE THE PIVOT OBJECT UP AND DOWN
        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }

    private void HandleCollisions(){
        targetCameraZPosition = cameraZPosition;

        RaycastHit hit;
        //DIRECTION FOR COLLISION CHECK
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize();

        //WE CHECK IF THERE IS AN OBJECT INFRONT OF OUR DESIRED DIRECTION
        if(Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers)){
            
            //IF THERE IS, WE GET OUR DISTANCE FROM IT
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);

            //WE THENEQUATE OUR TARGET Z POSITION TO THE FOLLOWING
            targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
        }

        //IF OUR TARGET POSITION IS LESS THAN OUR COLLISION RADIUS, WE SUBTRACT OUR COLLISION RADIUS (MAKING IT SNAP BACK)
        if(Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius){
            targetCameraZPosition = -cameraCollisionRadius;
        }

        //WE THEN APPLY OUR FINAL POSITIONUSING A LERP OVER A TIME OF 0.2F
        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }

}