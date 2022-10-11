using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Latch.Combat {
    public class Spanner : ProjectileController
    {
        private float currentLifeTimer;

        void Update() {
            currentLifeTimer += Time.deltaTime;
            if (currentLifeTimer >= projectileInfo.projectileStatistics.lifeTime) {
                Destroy(gameObject);
            }
        }

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
            else {
                Destroy(gameObject);
            }
        }        
    }
}