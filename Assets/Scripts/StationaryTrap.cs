using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Latch.Combat {
    public class StationaryTrap : MonoBehaviour
    {
        public ProjectileInfo projectileInfo;
        [HideInInspector]
        public List<CombatManager> combatManager;

        public float tickRate;
        public int tickAmount;

        private float currentTickTimer;
        private int currentTickNumbers;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            currentTickTimer = Time.deltaTime;

            if (currentTickTimer >= tickRate) {
                currentTickTimer -= tickRate;
                currentTickNumbers++;
                for (int i = 0; i < combatManager.Count; i++) {
                    combatManager[i].TakeDamage((((projectileInfo.projectileStatistics.damage / tickAmount) / 100) * 80), projectileInfo.projectileStatistics.damageType);
                }
            }
            
            if (currentTickNumbers >= tickAmount) {
                Debug.Log("Is this the cause of the bug?");
                Destroy(projectileInfo.gameObject);
            }
        }

        public void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Human") {
                combatManager.Add(other.gameObject.GetComponent<CombatManager>());
            }
        }

        public void OnTriggerExit(Collider other) {
            if (other.gameObject.tag == "Human") {
                combatManager.Remove(other.gameObject.GetComponent<CombatManager>());
            }
        }
    }
}