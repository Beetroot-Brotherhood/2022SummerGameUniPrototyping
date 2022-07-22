using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatcherAnimationManager : MonoBehaviour
{
    public Animator animator;
    public float speed;

    public GameObject latcherObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool walking = latcherObject.GetComponent<PlayerController>().walking;

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
