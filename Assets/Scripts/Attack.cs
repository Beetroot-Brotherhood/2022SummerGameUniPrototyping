using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Latch.Combat {
    public abstract class Attack : MonoBehaviour
    {
        public virtual void AttackFunc (bool isAttacking, out bool isAttackingReturn) {
            if (isAttacking) {
                isAttackingReturn = AttackFire1(isAttacking);
            }
            else{
                isAttackingReturn = isAttacking;
            }
        }

        public abstract bool AttackFire1 (bool isAttacking);


    }
}