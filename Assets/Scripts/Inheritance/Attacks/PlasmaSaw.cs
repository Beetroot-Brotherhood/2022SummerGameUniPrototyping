using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

namespace Latch.Combat {
    public class PlasmaSaw : Weapon {
        public Animator animator;
        public ThirdPersonController thirdPersonController;
        public AttackStatistics attackStats;
        public override bool AttackFire1 (bool isAttacking) {
            thirdPersonController.StartAttack();
            return isAttacking;
        }

        public override void Shoot (Vector3 target) {
            thirdPersonController.Attacking();
        }
    }
}
