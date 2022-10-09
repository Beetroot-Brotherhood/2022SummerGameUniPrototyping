using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Krezme;

namespace Latch.Combat {
    public class ZapTrap : Weapon {
        public Transform projectileSpawnPoint;
        public GameObject projectile;

        public ProjectileStatistics projectileStatistics;

        public override bool AttackFire1 (bool isAttacking) {
            if (isAttacking) {
                GameObject projectileInstance = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                projectileInstance.GetComponent<ProjectileInfo>().projectileStatistics = projectileStatistics.DeepClone();
            }
            return isAttacking;
        }
    }
}
