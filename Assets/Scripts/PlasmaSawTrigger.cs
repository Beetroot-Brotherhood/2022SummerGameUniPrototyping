using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Krezme;

namespace Latch.Combat {
    public class PlasmaSawTrigger : MonoBehaviour
    {
        public PlasmaSaw plasmaSaw;

        //create OnTriggerEnter function
        void OnTriggerEnter(Collider other)
        {
            //check if the object that entered the trigger is the player
            if (other.gameObject.tag == "Human")
            {
                other.gameObject.GetComponent<CombatManager>().TakeDamage(plasmaSaw.attackStats.damage, plasmaSaw.attackStats.damageType);
            }
        }
    }
}