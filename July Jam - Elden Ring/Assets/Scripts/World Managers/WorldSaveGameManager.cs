using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance;
    public PlayerManager player;

    [Header("SAVE/LOAD")]
    [SerializeField] bool saveGame;
    [SerializeField] bool loadGame;

    [Header("World Scene Index")]
    [SerializeField] int worldSceneIndex = 1;

    [Header("Save Data")]
    private SaveFileDataWriter saveFileDataWriter;

    [Header("Current Character Data")]
    public CharacterSlot currentCharacterSlotBeingUsed;
    public CharacterSaveData currentCharacterData;
    private string saveFileName;

    [Header("Character Slots")]
    public CharacterSaveData characterSlot01;
    public CharacterSaveData characterSlot02;
    public CharacterSaveData characterSlot03;
    public CharacterSaveData characterSlot04;
    public CharacterSaveData characterSlot05;
    public CharacterSaveData characterSlot06;
    public CharacterSaveData characterSlot07;
    public CharacterSaveData characterSlot08;
    public CharacterSaveData characterSlot09;
    public CharacterSaveData characterSlot10;


    private void Awake(){
        //there can only be one instance of this script
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start(){
        DontDestroyOnLoad(gameObject);
        LoadAllCharacterProfiles();
    }

    private void Update() {
        if(saveGame){
            saveGame = false;
            SaveGame();
        }
        if(loadGame){
            loadGame = false;
            LoadGame();
        }
    }
    public string DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot characterSlot){
        
        string fileName = "";

        switch(characterSlot){
            
            case CharacterSlot.CharacterSlot01:
                fileName = "characterSlot01";
                break;
            case CharacterSlot.CharacterSlot02:
                fileName = "characterSlot02";
                break;
            case CharacterSlot.CharacterSlot03:
                fileName = "characterSlot03";
                break;
            case CharacterSlot.CharacterSlot04:
                fileName = "characterSlot04";
                break;
            case CharacterSlot.CharacterSlot05:
                fileName = "characterSlot05";
                break;
            case CharacterSlot.CharacterSlot06:
                fileName = "characterSlot06";
                break;
            case CharacterSlot.CharacterSlot07:
                fileName = "characterSlot07";
                break;  
            case CharacterSlot.CharacterSlot08:
                fileName = "characterSlot08";
                break;
            case CharacterSlot.CharacterSlot09:
                fileName = "characterSlot09";
                break;
            case CharacterSlot.CharacterSlot10:
                fileName = "characterSlot10";
                break;
            default:
                break;
        }

        return fileName;
    }

    public void AttemptToCreateNewGame(){

        saveFileDataWriter = new SaveFileDataWriter();
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

        //CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISTING FILES FIRST)
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot01);

        if(!saveFileDataWriter.CheckToSeeIfFileExists()){

            //IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot01;
            currentCharacterData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        //CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISTING FILES FIRST)
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot02);

        if(!saveFileDataWriter.CheckToSeeIfFileExists()){

            //IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot02;
            currentCharacterData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        //CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISTING FILES FIRST)
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot03);

        if(!saveFileDataWriter.CheckToSeeIfFileExists()){

            //IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot03;
            currentCharacterData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        //CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISTING FILES FIRST)
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot04);

        if(!saveFileDataWriter.CheckToSeeIfFileExists()){

            //IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot04;
            currentCharacterData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        //CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISTING FILES FIRST)
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot05);

        if(!saveFileDataWriter.CheckToSeeIfFileExists()){

            //IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot05;
            currentCharacterData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        //CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISTING FILES FIRST)
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot06);

        if(!saveFileDataWriter.CheckToSeeIfFileExists()){

            //IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot06;
            currentCharacterData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        //CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISTING FILES FIRST)
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot07);

        if(!saveFileDataWriter.CheckToSeeIfFileExists()){

            //IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot07;
            currentCharacterData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        //CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISTING FILES FIRST)
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot08);

        if(!saveFileDataWriter.CheckToSeeIfFileExists()){

            //IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot08;
            currentCharacterData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        //CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISTING FILES FIRST)
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot09);

        if(!saveFileDataWriter.CheckToSeeIfFileExists()){

            //IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot09;
            currentCharacterData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        //CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISTING FILES FIRST)
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot10);

        if(!saveFileDataWriter.CheckToSeeIfFileExists()){

            //IF THIS PROFILE SLOT IS NOT TAKEN, MAKE A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot10;
            currentCharacterData = new CharacterSaveData();
            StartCoroutine(LoadWorldScene());
            return;
        }

        //NOTIFY IF ALL SLOTS ARE FULL
        TitleScreenManager.instance.DisplayNoFreeCharacterSlotsPopUp();      
    }

    public void LoadGame(){

        //LOAD A PREVIOUS FILE, WE A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
        saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

        saveFileDataWriter = new SaveFileDataWriter();
        //GENERALLY WORKS ON MULTIPLE MACHINES TYPES (Application.persistentDataPath)
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveFileDataWriter.saveFileName = saveFileName;
        currentCharacterData = saveFileDataWriter.LoadSaveFile();

        StartCoroutine(LoadWorldScene());
    }

    public void SaveGame(){

        //PASS THE PLAYERS INFO, FROM GAME OBJECT, TO THEIR SAVE FILE
        player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);

        //WRITE THAT INFO ONTO A JSON FILE, SAVED TO THIS MACHINE
        saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);

    }

    public void DeleteGame(CharacterSlot characterSlot){
        //CHOOSE A FILE TO DELETE BASED ON THE NAME
        saveFileDataWriter = new SaveFileDataWriter();
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

        saveFileDataWriter.DeleteSaveFile();
    }

    //LOAD ALL CHARACTER PROFILES ON DEVICE WHEN STARTING GAME
    private void LoadAllCharacterProfiles(){
        saveFileDataWriter = new SaveFileDataWriter();
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot01);
        characterSlot01 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot02);
        characterSlot02 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot03);
        characterSlot03 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot04);
        characterSlot04 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot05);
        characterSlot05 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot06);
        characterSlot06 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot07);
        characterSlot07 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot08);
        characterSlot08 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot09);
        characterSlot09 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot10);
        characterSlot10 = saveFileDataWriter.LoadSaveFile();

    }

    public IEnumerator LoadWorldScene(){
        
        //IF YOU WANT 1 WORLD SCENE USE THIS
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

        //IF YOU WANT TO USE DIFFERENT SCENES FOR LEVELS IN YOUR PROJECT USE THIS
        //AsyncOperation loadOperation = SceneManager.LoadSceneAsync(currentCharacterData.sceneIndex);
        
        player.LoadGameDataFromCurrentCharacterData(ref currentCharacterData);
        yield return null;
    }

    public int GetWorldSceneIndex(){
        return worldSceneIndex;
    }
    
}
