using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Krezme;

namespace Latch.Combat {
    public class ZapTrap : Weapon {
        public Transform projectileSpawnPoint;
        public GameObject projectile;
        public GameObject owner;

        public ProjectileStatistics projectileStatistics;

        private float currentFireTimer;
        private bool canFire;

        void Update () {
            if (currentFireTimer >= projectileStatistics.fireRate && !canFire) {  
                currentFireTimer = 0;
                canFire = true;
            }
            else if (!canFire) {
                currentFireTimer += Time.deltaTime;
            }
        }

        public override bool AttackFire1 (bool isAttacking) {
            if (canFire) {
                canFire = false;
                Ray ray = QualityOfLife.GetCameraCentrePointAsRay(Camera.main);
                if (Physics.Raycast(ray, out RaycastHit hit, 1000f)) {
                    GameObject projectileInstance = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                    projectileStatistics = CalculateProjectileStatistics(projectileStatistics, Vector3.Distance(hit.point, projectileInstance.transform.position), projectileInstance.transform, hit.point);
                    projectileInstance.GetComponent<ProjectileInfo>().projectileStatistics = projectileStatistics.DeepClone();
                    projectileInstance.GetComponent<ProjectileController>().projectileOwner = owner;
                }
                else {
                    Debug.Log("Raycast didn't hit anything");
                }
            }
            return isAttacking;
        }

        public ProjectileStatistics CalculateProjectileStatistics (ProjectileStatistics projectileStatistics, float distance, Transform projectile, Vector3 target) {
            projectileStatistics.distance = distance;
            projectileStatistics.angleBetweenObjects = QualityOfLife.GetAngleBetweenObjects(transform.forward, projectile.position, target, projectile.forward);
            projectileStatistics.speed = QualityOfLife.GetInitialSpeedToArc(projectileStatistics.angle, Physics.gravity.magnitude, projectile.position, target);
            return projectileStatistics;
        }
    }
}
