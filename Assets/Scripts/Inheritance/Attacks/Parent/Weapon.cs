using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Latch.Combat {
    public abstract class Weapon : MonoBehaviour
    {
        public virtual void AttackFunc (bool isAttacking, out bool isAttackingReturn) {
            if (isAttacking) {
                isAttackingReturn = AttackFire1(isAttacking);
            }
            else{
                isAttackingReturn = isAttacking;
            }
        }

        /// <summary>
        /// The specific attack for the weapon
        /// </summary>
        /// <param name="isAttacking">Used if the weapon is requires continues kicking</param>
        /// <returns>if it is attacking</returns>
        public abstract bool AttackFire1 (bool isAttacking);

        public abstract void Shoot (Vector3 target);
    }
}