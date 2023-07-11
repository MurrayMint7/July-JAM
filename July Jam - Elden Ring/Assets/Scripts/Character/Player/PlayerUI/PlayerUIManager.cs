using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;
    [Header("NETWORK JOIN")]
    [SerializeField] bool startGameAsClient;

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
