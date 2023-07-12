using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    public PlayerManager player;
    //1. find a way to read input
    //2. move character based on those values

    PlayerControls playerControls;

    [Header("MOVEMENT INPUT")]
    [SerializeField] Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("CAMERA INPUT")]
    [SerializeField] Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    [Header("PLAYER ACTIONS")]
    [SerializeField] bool dodgeInput = false;

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

        //WHEN THE SCENE CHANGES, RUN THIS LOGIC
        SceneManager.activeSceneChanged += OnSceneChange;

        instance.enabled = false;
    }

    private void OnSceneChange(Scene oldScene, Scene newScene){

        //IF WE ARE LOADING INTO OUR WORLD SCENE, DIABLE OUR PLAYER CONTROLS
        if(newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex()){
            instance.enabled = true;
        }

        //OTHERWISE WE MUST BE AT THE MAIN MENU, DISABLE OUR PLAYER CONTROLS
        //THIS IS SO OUR PLAYER CANNOT MOVE AROUND IF WE ENTER THINGS DURING CHARACTER CREATION
        else{
            instance.enabled = false;
        }
    }

    private void OnEnable() {

        if(playerControls == null){
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
        }

        playerControls.Enable();
    }
    
    private void OnDestroy(){
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    //IF WE MINIMISE THE WINDOW, STOP ADJUSTING INPUTS
    private void OnApplicationFocus(bool focus) {
        if(enabled){
            if(focus){
                playerControls.Enable();
            }
            else{
                playerControls.Disable();
            }
        }
    }

    private void Update() {
        HandleAllInput();
    }

    private void HandleAllInput(){
        HandlePlayerMovementInput();
        HandleCameraMovementInput();
        HandleDodgeInput();
    }
    
    //MOVEMENT

    private void HandlePlayerMovementInput(){
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        if(moveAmount <= 0.5 && moveAmount > 0){
            moveAmount = 0.5f;
        }
        else if(moveAmount > 0.5 && moveAmount <= 1){
            moveAmount = 1;
        }

        //WE PASS 0 ON THE HORIZONTAL AS WE ONLY WANT TO STRAFE WHEN WE ARE LOCKED ONTO AN EMEMY
        player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);

        if(player == null){
            return;
        }
        //IF WE ARE LOCKED ON PASS THE HORIZONTAL MOVEMENT AS WELL
    }

    private void HandleCameraMovementInput(){
        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;
    }

    //ACTION

    private void HandleDodgeInput(){
        if(dodgeInput){
            dodgeInput = false;

            //RETURN IF MENU OR UI IS OPEN
            //PERFORM DODGE

            player.playerLocomotionManager.AttemptToPerformDodge();
        }
    }
}
