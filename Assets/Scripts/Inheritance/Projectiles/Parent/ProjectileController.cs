using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Krezme;


namespace Latch.Combat {
    [RequireComponent(typeof(Rigidbody), typeof(ProjectileInfo))]
    public abstract class ProjectileController : MonoBehaviour
    {

        public ProjectileInfo projectileInfo;
        public Rigidbody projectileRigidbody;
        [HideInInspector]
        public GameObject projectileOwner;
        
        void Start()
        {
            LaunchProjectile();
        }

        [ContextMenu("LaunchProjectile")]
        public void LaunchProjectile() {
            //projectileRigidbody.mass = projectileInfo.projectileStatistics.mass;
            try {
                projectileRigidbody.velocity = Quaternion.AngleAxis(projectileInfo.projectileStatistics.angleBetweenObjects, Vector3.up) * QualityOfLife.GetVector3VelocityToArc(projectileInfo.projectileStatistics.speed, QualityOfLife.GetRadians(projectileInfo.projectileStatistics.angle));
            }
            catch (System.Exception) {
                projectileRigidbody.velocity = transform.forward * projectileInfo.projectileStatistics.maxSpeed;
            }
        }

        //create a function OnTriggerEnter
        void OnTriggerEnter(Collider other)
        {
            OnHit(other);
        }

        public abstract void OnHit(Collider other);
    }
}
