using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Character_Save_Slot : MonoBehaviour
{
    SaveFileDataWriter saveFileWriter;

    [Header("Game Slot")]
    public CharacterSlot characterSlot;

    [Header("Character Info")]
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI timePlayed;

    private void OnEnable(){
        LoadSaveSlot();
    }

    private void LoadSaveSlot(){
        saveFileWriter = new SaveFileDataWriter();
        saveFileWriter.saveDataDirectoryPath = Application.persistentDataPath;


        switch(characterSlot){
            case CharacterSlot.CharacterSlot01:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    //IF THE FILE EXISTS, GET INFORMATION FROM IT
                    if(saveFileWriter.CheckToSeeIfFileExists()){
                        characterName.text = WorldSaveGameManager.instance.characterSlot01.characterName;
                    }

                    //IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                    else{
                        gameObject.SetActive(false);
                    }
                break;
            case CharacterSlot.CharacterSlot02:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    //IF THE FILE EXISTS, GET INFORMATION FROM IT
                    if(saveFileWriter.CheckToSeeIfFileExists()){
                        characterName.text = WorldSaveGameManager.instance.characterSlot02.characterName;
                    }

                    //IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                    else{
                        gameObject.SetActive(false);
                    }
                break;
            case CharacterSlot.CharacterSlot03:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    //IF THE FILE EXISTS, GET INFORMATION FROM IT
                    if(saveFileWriter.CheckToSeeIfFileExists()){
                        characterName.text = WorldSaveGameManager.instance.characterSlot03.characterName;
                    }

                    //IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                    else{
                        gameObject.SetActive(false);
                    }
                break;
            case CharacterSlot.CharacterSlot04:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    //IF THE FILE EXISTS, GET INFORMATION FROM IT
                    if(saveFileWriter.CheckToSeeIfFileExists()){
                        characterName.text = WorldSaveGameManager.instance.characterSlot04.characterName;
                    }

                    //IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                    else{
                        gameObject.SetActive(false);
                    }
                break;
            case CharacterSlot.CharacterSlot05:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    //IF THE FILE EXISTS, GET INFORMATION FROM IT
                    if(saveFileWriter.CheckToSeeIfFileExists()){
                        characterName.text = WorldSaveGameManager.instance.characterSlot05.characterName;
                    }

                    //IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                    else{
                        gameObject.SetActive(false);
                    }
                break;
            case CharacterSlot.CharacterSlot06:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    //IF THE FILE EXISTS, GET INFORMATION FROM IT
                    if(saveFileWriter.CheckToSeeIfFileExists()){
                        characterName.text = WorldSaveGameManager.instance.characterSlot06.characterName;
                    }

                    //IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                    else{
                        gameObject.SetActive(false);
                    }
                break;
            case CharacterSlot.CharacterSlot07:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    //IF THE FILE EXISTS, GET INFORMATION FROM IT
                    if(saveFileWriter.CheckToSeeIfFileExists()){
                        characterName.text = WorldSaveGameManager.instance.characterSlot07.characterName;
                    }

                    //IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                    else{
                        gameObject.SetActive(false);
                    }
                break;
            case CharacterSlot.CharacterSlot08:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    //IF THE FILE EXISTS, GET INFORMATION FROM IT
                    if(saveFileWriter.CheckToSeeIfFileExists()){
                        characterName.text = WorldSaveGameManager.instance.characterSlot08.characterName;
                    }

                    //IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                    else{
                        gameObject.SetActive(false);
                    }
                break;
            case CharacterSlot.CharacterSlot09:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    //IF THE FILE EXISTS, GET INFORMATION FROM IT
                    if(saveFileWriter.CheckToSeeIfFileExists()){
                        characterName.text = WorldSaveGameManager.instance.characterSlot09.characterName;
                    }

                    //IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                    else{
                        gameObject.SetActive(false);
                    }
                break;
            case CharacterSlot.CharacterSlot10:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    //IF THE FILE EXISTS, GET INFORMATION FROM IT
                    if(saveFileWriter.CheckToSeeIfFileExists()){
                        characterName.text = WorldSaveGameManager.instance.characterSlot10.characterName;
                    }

                    //IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                    else{
                        gameObject.SetActive(false);
                    }
                break;
        }
        
    }

    public void LoadGameFromCharacterSlot(){
        WorldSaveGameManager.instance.currentCharacterSlotBeingUsed = characterSlot;
        WorldSaveGameManager.instance.LoadGame();
    }

    public void SelectCurrentSlot(){
        TitleScreenManager.instance.SelectCharacterSlot(characterSlot);
    }
}
