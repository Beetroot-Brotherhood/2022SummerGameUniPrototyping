using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Krezme;
using StarterAssets;

namespace Latch.Combat {
    public class CombatManager : MonoBehaviour
    {
        public Statistics defaultStats;
        public Statistics currentStats;
        public Statistics maxStats;

        [Header("Attack")]
        public Weapon primaryAttack;

        public StarterAssetsInputs starterAssetsInputs;
        public ThirdPersonController thirdPersonController;

        // Start is called before the first frame update
        void Start()
        {
            ResetStatistics();
        }

        // Update is called once per frame
        void Update()
        {
            primaryAttack.AttackFunc(starterAssetsInputs.onFire1, out starterAssetsInputs.onFire1);
        }

        public void ResetStatistics() {
            currentStats = defaultStats.DeepClone();
        }

        public void TakeDamage(int damage, DamageType damageType) {
            bool damageTaken = false;
            for (int j = 0; j < CombatData.damageShieldMaps.Length; j++) {
                for (int i = 0; i < currentStats.armours.Length; i++) {
                    if (CombatData.damageShieldMaps[j][damageType] == currentStats.armours[i].shieldTypes) {
                        if (currentStats.armours[i].health > 0) {
                            if (CombatData.damageEffectivenessDividerMap[j] > 0) {
                                currentStats.armours[i].health -= damage / CombatData.damageEffectivenessDividerMap[j];
                                damageTaken = true;
                                break;
                            }
                        }
                    }
                }
                if (damageTaken) {
                    break;
                }
            }
            
            for (int i = 0; i < currentStats.armours.Length; i++) {
                if (currentStats.armours[i].health > 0) {
                    return;
                }
            }

            thirdPersonController.StartDowned();
        }
    }
}