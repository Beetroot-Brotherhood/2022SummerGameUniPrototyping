using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryBox : MonoBehaviour
{
    public bool parryReactBool = false;


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyWeapon")
        {
            parryReactBool = true;

            Debug.Log("lungsbungus");
        }
    }

}
