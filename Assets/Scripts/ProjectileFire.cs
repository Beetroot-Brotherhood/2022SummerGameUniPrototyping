using UnityEngine;
using System.Collections;
using Krezme;
 
public class ProjectileFire : MonoBehaviour {
 
    [SerializeField]
    Transform target;
 
    [SerializeField]
    float initialAngle;

    public Transform projectile;
 
    [ContextMenu("Fire")]
    void StartASD () {
        projectile.position = transform.position;

        projectile.rotation = Quaternion.LookRotation(target.position - projectile.position);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
 
        // Positions of this object and the target on the same plane
         
        float initialSpeed = QualityOfLife.GetInitialSpeedToArc(initialAngle, Physics.gravity.magnitude, projectile.position, target.position);
 
        Vector3 velocity = QualityOfLife.GetVector3VelocityToArc(initialSpeed, QualityOfLife.GetRadians(initialAngle));

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = QualityOfLife.GetAngleBetweenObjects(Vector3.forward, projectile.position, target.position, projectile.forward);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // Fire!
        rb.velocity = finalVelocity;
    }
}