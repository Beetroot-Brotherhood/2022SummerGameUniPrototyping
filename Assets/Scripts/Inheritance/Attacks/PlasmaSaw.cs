using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Latch.Combat {
    public class PlasmaSaw : Weapon {
        public override bool AttackFire1 (bool isAttacking) {
            return isAttacking;
        }
    }
}
