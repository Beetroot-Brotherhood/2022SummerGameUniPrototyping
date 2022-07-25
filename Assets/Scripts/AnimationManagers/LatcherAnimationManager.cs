using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatcherAnimationManager : MonoBehaviour
{
    public Animator animator;
    public float speed;

    public GameObject latcherObject;

    // Update is called once per frame
    void Update()
    {
        bool walking = latcherObject.GetComponent<LatcherController>().walking;

        if (walking == false)
        {
            speed = 0;
        }
        else
        {
            speed = 5;
        }
        animator.SetFloat("Speed", speed);
    }
}
