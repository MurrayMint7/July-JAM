using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : CharacterEquipmentManager
{
    PlayerManager player;
    public WeaponModelInsantiationSlot rightHandSlot;
    public WeaponModelInsantiationSlot leftHandSlot;

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

    public void LoadWeaponOnRightHand(){
        if(player.playerInventoryManager.currentRightHandWeapon != null){
            rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
            rightHandSlot.LoadWeapon(rightHandWeaponModel);
        }
    }

    public void LoadWeaponOnLeftHand(){
        if(player.playerInventoryManager.currentLeftHandWeapon != null){
            leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);
            leftHandSlot.LoadWeapon(leftHandWeaponModel);
        }
    }
}
