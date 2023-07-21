using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//SINCE WE WANT TO REFERENCE THIS DATA FOR EVERY SAVE FILE, THIS SCRIPT IS NOT A MONOBEHAVIOUR AND IS INSTEAD SERIALIZABLE
public class CharacterSaveData
{
    [Header("Character Name")]
    public string characterName = "Character";

    [Header("Time Played")]
    public float secondsPlayed;

    //QUESTION: WHY NOT USE A VECTOR3?
    //ANSWER: WE CAN ONLY SAVE DATA FROM BASIC DATA TYPES SUCH AS INT, FLOAT, STRING OR BOOL
    [Header("World Coordinates")]
    public float xPos;
    public float yPos;
    public float zPos;

}
