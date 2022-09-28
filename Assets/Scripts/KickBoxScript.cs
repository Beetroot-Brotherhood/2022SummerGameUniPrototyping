using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBoxScript : MonoBehaviour
{
    public float kickForce;
    public float kickSize;

    public void OnTriggerEnter(Collider other)
    {
        KickKnockback();



    }

    public void KickKnockback()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, kickSize);

        foreach(Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(kickForce, transform.position, kickSize);
                FMODUnity.RuntimeManager.PlayOneShot("event:/--- Code Slicer ---/Player/Kick/KickHit");
            }

        }

    }





}
