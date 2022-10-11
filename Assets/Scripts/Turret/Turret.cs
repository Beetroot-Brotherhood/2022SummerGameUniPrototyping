using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Krezme;

public class Turret : MonoBehaviour
{
    public Transform player;
    public Transform turretHead;
    public Transform bulletSpawnPoint;
    public float bulletRange = 100f;
    public float force = 1000f;

    public GameObject bulletPrefab;
    [HideInInspector] public GameObject bullet;

    public bool hasFired = false;
    public float lifespan = 5f;

    [HideInInspector]
    public float timeremaining;

    // Start is called before the first frame update
    void Start()
    {
        timeremaining = lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFired)
        {

            timeremaining -= Time.deltaTime;

            if (timeremaining <= 0)
            {
                Destroy(bullet);
                timeremaining = lifespan;
                hasFired = false;
            }
        }
        QualityOfLife.LookAtPlayer(turretHead, player);
        QualityOfLife.LookAtPlayer(bulletSpawnPoint.transform, player);

        RaycastHit hit;

        if (Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out hit, bulletRange))
        {
            if (hit.transform.tag == "Player")
            {
                Debug.Log("Player is in range");

                if(!hasFired)
                {
                    bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    Rigidbody lb = bullet.GetComponent<Rigidbody>();
                    lb.AddRelativeForce(transform.forward * force);
                    hasFired = true;
                }
               
            }
        }
    }
}
