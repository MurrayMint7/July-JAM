using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    [HideInInspector] public PlayerNetworkManager playerNetworkManager;
    [HideInInspector] public PlayerStatsManager playerStatsManager;

    protected override void Awake()
    {
        base.Awake();

        //DO MORE STUFF, ONLY FOR PLAYER

        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerNetworkManager = GetComponent<PlayerNetworkManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    protected override void Update(){
        base.Update();

        //IF WE DO NOT OWN THIS GAMEOBJECT, WE FO NOT CONTROL OR EDIT IT
        if(!IsOwner){
            return;
        }
        //HANDLE ALL OF OUR CHARACTER MOVEMENT
        playerLocomotionManager.HandleAllMovement();

        //REGEN STAMINA
        playerStatsManager.RegenerateStamina();
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
            WorldSaveGameManager.instance.player = this;

            //UPDATE THE TOTAL AMOUNT OF HEALTH WHEN THE STATE LINKED TO EITHER CHANGES
            playerNetworkManager.vitality.OnValueChanged += playerNetworkManager.SetNewMaxHealthValue;
            playerNetworkManager.endurance.OnValueChanged += playerNetworkManager.SetNewMaxStaminaValue;

            //UPDATES OUR UI STAT BARS WHEN A STATE CHANGES 
            playerNetworkManager.currentHealth.OnValueChanged += PlayerUIManager.instance.playerUIHUDManager.SetNewHealthValue;
            playerNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.instance.playerUIHUDManager.SetNewStaminaValue;
            playerNetworkManager.currentStamina.OnValueChanged += playerStatsManager.ResetStaminaRegenTimer;
            
        }
    }

    public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData){
        currentCharacterData.sceneIndex = SceneManager.GetActiveScene().buildIndex;

        currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
        currentCharacterData.xPos = transform.position.x;
        currentCharacterData.yPos = transform.position.y;
        currentCharacterData.zPos = transform.position.z;

        currentCharacterData.currentStamina = playerNetworkManager.currentStamina.Value;
        currentCharacterData.currentHealth = playerNetworkManager.currentHealth.Value;

        currentCharacterData.vitality = PlayerNetworkManager.vitality.Value;
        currentCharacterData.endurance = PlayerNetworkManager.endurance.Value;
    }

    public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData){
        playerNetworkManager.characterName.Value = currentCharacterData.characterName;
        Vector3 myPos = new Vector3(currentCharacterData.xPos, currentCharacterData.yPos, currentCharacterData.zPos);
        transform.position = myPos;

        playerNetworkManager.vitality.Value = currentCharacterData.vitality;
        playerNetworkManager.endurance.Value = currentCharacterData.endurance;

        //THIS WILL BE MOVED WHEN SAVING AND LOADING IS ADDED
        playerNetworkManager.maxHealth.Value = playerStatsManager.CalculateHealthBasedOnLevel(playerNetworkManager.vitality.Value);
        playerNetworkManager.maxStamina.Value = playerStatsManager.CalculateStaminaBasedOnLevel(playerNetworkManager.endurance.Value);
        playerNetworkManager.currentStamina.Value = currentCharacterData.currentStamina;
        playerNetworkManager.currentHealth.Value = currentCharacterData.currentHealth;
        PlayerUIManager.instance.playerUIHUDManager.SetMaxStaminaValue(playerNetworkManager.maxStamina.Value);

    }
}
