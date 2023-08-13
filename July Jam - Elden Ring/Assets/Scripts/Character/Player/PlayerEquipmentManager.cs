using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : CharacterEquipmentManager
{
    PlayerManager player;
    public WeaponModelInsantiationSlot rightHandSlot;
    public WeaponModelInsantiationSlot leftHandSlot;

    [SerializeField] WeaponManager rightHandWeaponManager;
    [SerializeField] WeaponManager leftHandWeaponManager;

    public GameObject rightHandWeaponModel;
    public GameObject leftHandWeaponModel;

    protected override void Awake(){
        base.Awake();

        player = GetComponent<PlayerManager>();
        //GET OUR SLOTS

        InitializeWeaponSlots();

    }

    protected override void Start(){
        base.Start();

        LoadWeaponsOnBothHands();}

    private void InitializeWeaponSlots(){
        //LOAD OUR WEAPON MODELS INTO OUR SLOTS

        WeaponModelInsantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInsantiationSlot>();

        foreach(var weaponSlot in weaponSlots){
            if(weaponSlot.weaponSlot == WeaponModelSlot.RightHand){
                rightHandSlot = weaponSlot;
            }
            else if(weaponSlot.weaponSlot == WeaponModelSlot.LeftHand){
                leftHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponsOnBothHands(){
        LoadWeaponOnLeftHand();
        LoadWeaponOnRightHand();
    }

    //RIGHT WEAPON
    public void LoadWeaponOnRightHand(){
        if(player.playerInventoryManager.currentRightHandWeapon != null){
            rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
            rightHandSlot.LoadWeapon(rightHandWeaponModel);
            rightHandWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
            rightHandWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            //ASSIGN WEAPONS DAMAGE TO OUR DAMAGE COLLIDER
        }
    }


    //LEFT WEAPON
    public void LoadWeaponOnLeftHand(){
        if(player.playerInventoryManager.currentLeftHandWeapon != null){
            leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);
            leftHandSlot.LoadWeapon(leftHandWeaponModel);
            leftHandWeaponManager = leftHandWeaponModel.GetComponent<WeaponManager>();
            leftHandWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
            //ASSIGN WEAPONS DAMAGE TO OUR DAMAGE COLLIDER

        }
    }
}
