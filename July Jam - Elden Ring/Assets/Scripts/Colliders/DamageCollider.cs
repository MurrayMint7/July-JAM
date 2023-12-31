using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [Header("Collider")]
    protected Collider damageCollider;

    [Header("Damage")]
    public float physicalDamage = 0; //IN THE FUTURE, WILL BE SPLIT INTO "STANDARD", "STRIKE", "PIERCE", "SLASH" ETC
    public float magicDamage = 0;
    public float fireDamage = 0;
    public float lightningDamage = 0;
    public float holyDamage = 0;

    [Header("Contact Point")]
    protected Vector3 contactPoint;

    [Header("Characters Damaged")]
    public List<CharacterManager> charactersDamaged = new List<CharacterManager>();

    private void OnTriggerEnter(Collider other) {
        CharacterManager damageTarget = other.GetComponent<CharacterManager>();

        if(damageTarget != null){
            contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

            //CHECK IF WE CAN DMG THIS TARGET BASED IN FRIENDLY FIRE

            //CHECK IF TARGET IS BLOCKING

            //CHECK IF TARGET IS INVULNERABLE

            //DAMAGE TARGET
            DamageTarget(damageTarget);
        }
    }

    protected virtual void DamageTarget(CharacterManager damageTarget){
        //WE DONT WANT TO DMG THE SAME TARGET MORE THAN ONCE IN A SINGLE ATTACK
        //SO WE ADD THEM TO A LIST THAT CHECKS BEFORE APPLYING DMG

        if(charactersDamaged.Contains(damageTarget)){
            return;
        }

        charactersDamaged.Add(damageTarget);

        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
        damageEffect.physicalDamage = physicalDamage;
        damageEffect.magicDamage = magicDamage;
        damageEffect.fireDamage = fireDamage;
        damageEffect.lightningDamage = lightningDamage;
        damageEffect.holyDamage = holyDamage;
        damageEffect.contactPoint = contactPoint;

        damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
    }

    public virtual void EnableDamageCollider(){
        damageCollider.enabled = true;
    }

    public virtual void DisableDamageCollider(){
        damageCollider.enabled = false;
        charactersDamaged.Clear(); //CLEAR THE LIST OF CHARACTERS DAMAGED SO WE CAN DMG THEM AGAIN
    }
}
