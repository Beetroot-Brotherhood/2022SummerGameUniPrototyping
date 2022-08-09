using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ybotSwordCollisionDetection : MonoBehaviour
{
    public bool staggerCollision = false;



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blocker")
        {

            staggerCollision = true;


            Debug.Log("lungsbungus");
        }

     





    }




}
