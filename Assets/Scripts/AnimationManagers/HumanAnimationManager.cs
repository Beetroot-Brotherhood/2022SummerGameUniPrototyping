using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimationManager : MonoBehaviour
{
    public Animator animator;
    public float speed;

    public GameObject humanObject;

    // Update is called once per frame
    void Update()
    {
        bool walking = humanObject.GetComponent<HumanController>().walking;

        if (walking == false)
        {
            speed = 0;
        }
        else
        {
            speed = 4;
        }
        animator.SetFloat("Speed", speed);
    }
}
