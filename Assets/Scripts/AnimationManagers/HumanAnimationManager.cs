using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimationManager : MonoBehaviour
{
    public Animator animator;
    public float speed;

    public GameObject humanObject;

    // Start is called before the first frame update
    void Start()
    {
        speed = humanObject.GetComponent<PlayerController>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", speed);
    }
}
