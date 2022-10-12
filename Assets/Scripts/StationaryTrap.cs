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
            currentTickTimer += Time.deltaTime;
            //Debug.Log("Running");
            if (currentTickTimer >= tickRate) {
                //Debug.Log("Running2");
                currentTickTimer -= tickRate;
                currentTickNumbers++;
                for (int i = 0; i < combatManager.Count; i++) {
                    //Debug.Log("Running3");
                    combatManager[i].TakeDamage(Mathf.RoundToInt((((float)projectileInfo.projectileStatistics.damage / (float)tickAmount) / 100f) * 80f), projectileInfo.projectileStatistics.damageType);
                }
            }
            
            if (currentTickNumbers >= tickAmount) {
                Destroy(projectileInfo.gameObject);
            }
        }

        public void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Human") {
                Debug.Log("Added: " + other.gameObject.name);
                for (int i = 0; i < combatManager.Count; i++) {
                    if (combatManager[i].gameObject == other.gameObject) {
                        return;
                    }
                }
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