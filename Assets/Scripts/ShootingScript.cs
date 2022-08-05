using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    public float force;
    public GameObject bulletPrefab;
    public Transform leftGunEnd, rightGunEnd;
    private Vector3 aim;

    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;

        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos += Camera.main.transform.forward * 10f; // Make sure to add some "depth" to the screen point
        aim = Camera.main.ScreenToWorldPoint(mousePos);

    }

    void OnFire()
    {
        leftGunEnd.LookAt(aim);
        rightGunEnd.LookAt(aim);

        GameObject lBullet = Instantiate(bulletPrefab, leftGunEnd.position, Quaternion.identity);
        GameObject rBullet = Instantiate(bulletPrefab, rightGunEnd.position, Quaternion.identity);

        lBullet.transform.LookAt(aim);
        rBullet.transform.LookAt(aim);


        Rigidbody lb = lBullet.GetComponent<Rigidbody>();
        lb.AddRelativeForce(transform.forward * force);

        Rigidbody rb = rBullet.GetComponent<Rigidbody>();
        rb.AddRelativeForce(transform.forward * force);
    }
}
