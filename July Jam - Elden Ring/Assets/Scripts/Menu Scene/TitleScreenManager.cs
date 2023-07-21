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

    [Header("Pop Ups")]
    [SerializeField] GameObject noCharacterSlotsPopUp;
    [SerializeField] Button noCharacterSlotsOkayButton;

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
}
