using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ybotSwordCollisionDetection : MonoBehaviour
{
    public bool staggerCollision = false;

    public YbotTestController2 ybat;

    public GameObject metalSparksEffect;

    public Vector3 sparkPoint;
  

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blocker")
        {

            staggerCollision = true;

            Debug.Log("stagger colission");

            sparkPoint = new Vector3(this.gameObject.transform.position.x + (other.gameObject.transform.position.x - this.gameObject.transform.position.x ) / 2, 
                this.gameObject.transform.position.y + (other.gameObject.transform.position.y - this.gameObject.transform.position.y ) / 2,
                this.gameObject.transform.position.z + (other.gameObject.transform.position.z - this.gameObject.transform.position.z  ) / 2);

            Instantiate(metalSparksEffect, sparkPoint, Quaternion.identity);

        }






    }




}
