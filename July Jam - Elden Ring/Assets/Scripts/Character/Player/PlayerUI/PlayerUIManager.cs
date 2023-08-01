using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    [Header("NETWORK JOIN")]
    [SerializeField] bool startGameAsClient;

    [SerializeField] public PlayerUIHUDManager playerUIHUDManager;
    [HideInInspector] public PlayerUIPopUpManager playerUIPopUpManager;


    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }

        playerUIHUDManager = GetComponentInChildren<PlayerUIHUDManager>();
        playerUIPopUpManager = GetComponentInChildren<PlayerUIPopUpManager>();
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update(){
        if(startGameAsClient){
            startGameAsClient = false;
            //WE MUST SHUT DOWN AS WE STARTED AS HOST DURING TITLE SCREEN
            NetworkManager.Singleton.Shutdown();
            //WE THEN RESTART AS CLIENT
            NetworkManager.Singleton.StartClient();

        }
    }
}
