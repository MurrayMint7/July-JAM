using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : CharacterStatsManager
{   
    PlayerManager player;

    protected override void Awake(){
        base.Awake();

        player = GetComponent<PlayerManager>();       
    }

    protected override void Start(){
        base.Start();

        //WHY CALCULATE VALUES HERE?
        //WHEN WE MAKE A CHARACTER CREATION MENU, AND THE STATS DEPEND ON THE PLAYER'S CLASS, WE WILL NEED TO CALCULATE THE VALUES HERE
        //UNTIL THEN HOWEVER, STATES ARE NEVER CALCULATED, SO WE DO IT ON START, IF A SAVE FILE EXISTS THEY WILL BE OVERWRITTEN WHEN LOADING INTO A SCENE
        CalculateHealthBasedOnVitalityLevel(player.playerNetworkManager.vitality.Value);
        CalculateStaminaBasedOnEnduranceLevel(player.playerNetworkManager.endurance.Value);
    }
}
