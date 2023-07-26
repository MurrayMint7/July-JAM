using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TitleScreenManager : MonoBehaviour
{
    public static TitleScreenManager instance;

    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject loadMenu;

    [Header("Buttons")]
    [SerializeField] Button loadMenuReturnButton;
    [SerializeField] Button mainMenuLoadGameButton;
    [SerializeField] Button mainMenuNewGameButton;
    [SerializeField] Button deleteCharacterPopUpConfirmButton;
    [SerializeField] Button noCharacterSlotsOkayButton;


    [Header("Pop Ups")]
    [SerializeField] GameObject noCharacterSlotsPopUp;
    [SerializeField] GameObject deleteCharacterSlotPopUp;


    [Header("Character Slots")]
    public CharacterSlot currentSelectedSlot = CharacterSlot.NO_SLOT;

    


    private void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void StartNetworkAsHost(){
        NetworkManager.Singleton.StartHost();
    }

    public void StartNewGame(){
        WorldSaveGameManager.instance.AttemptToCreateNewGame();
    }

 
    public void OpenLoadGameMenu(){
        mainMenu.SetActive(false);
        loadMenu.SetActive(true);

        loadMenuReturnButton.Select();

    }

    public void CloseLoadGameMenu(){
        loadMenu.SetActive(false);
        mainMenu.SetActive(true);
        
        mainMenuLoadGameButton.Select();
    }

    public void DisplayNoFreeCharacterSlotsPopUp(){
        noCharacterSlotsPopUp.SetActive(true);
        noCharacterSlotsOkayButton.Select();
    }

    public void CloseNoFreeCharacterSlotsPopUp(){
        noCharacterSlotsPopUp.SetActive(false);
        mainMenuNewGameButton.Select();
    }

    //CHARACTER SLOTS

    public void SelectCharacterSlot(CharacterSlot characterSlot){
        currentSelectedSlot = characterSlot;
    }

    public void SelectNoSlot(){
        currentSelectedSlot = CharacterSlot.NO_SLOT;
    }

    public void AttemptToDeleteCharacterSlot(){
        if(currentSelectedSlot != CharacterSlot.NO_SLOT){
            deleteCharacterSlotPopUp.SetActive(true);
            deleteCharacterPopUpConfirmButton.Select();
        }
    }

    public void DeleteCharacterSlot(){
        deleteCharacterSlotPopUp.SetActive(false);
        WorldSaveGameManager.instance.DeleteGame(currentSelectedSlot);
        
        //DISABLE THEN ENABLE THE LOAD MENU TO REFRESH THE SLOTS AFTER DELETING ONE
        loadMenu.SetActive(false);
        loadMenu.SetActive(true);
        
        loadMenuReturnButton.Select();  
    }

    public void CloseDeleteCharacterPopUp(){
        deleteCharacterSlotPopUp.SetActive(false);
        loadMenuReturnButton.Select();
    }
}
