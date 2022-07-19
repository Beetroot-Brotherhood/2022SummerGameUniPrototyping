using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Latch : MonoBehaviour
{
    [SerializeField]
    private float latchRange = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out hit, latchRange))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.collider.tag == "Enemy")
            {
                Debug.Log("Can Latch");
            }
            
        }
    }
}
