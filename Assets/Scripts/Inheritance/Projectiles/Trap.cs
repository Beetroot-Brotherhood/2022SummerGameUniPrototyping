using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Latch.Combat {
    public class Trap : ProjectileController {

        public Collider trapCollider;
        public GameObject stationaryTrapGO;

        public override void OnHit (Collider other) {
            if (other.gameObject == projectileOwner) {
                return;
            }
            //if the object that the projectile hits has a tag of "Enemy"
            if (other.gameObject.tag == "Human")
            {
                other.gameObject.GetComponent<CombatManager>().TakeDamage(projectileInfo.projectileStatistics.damage, projectileInfo.projectileStatistics.damageType);
                Destroy(gameObject);
            }
            else if (other.gameObject.tag == "Projectile") {}
            else if (other.gameObject.tag == "Untagged") {
                projectileRigidbody.velocity = Vector3.zero;
                projectileRigidbody.useGravity = false;
                projectileRigidbody.isKinematic = false;
                trapCollider.isTrigger = false;
                stationaryTrapGO.SetActive(true);
            }
        }
    }
}
