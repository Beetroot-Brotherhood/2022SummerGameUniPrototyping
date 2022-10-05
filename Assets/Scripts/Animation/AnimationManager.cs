using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [HideInInspector] public PlayerInputs _input;
    public Animator mechAnim, lazerAnim, wiperAnim, monitorAnim, throttleAnim, stickAnim;
    public bool lazer = true, wiper = true, monitor = true, throttle = true, stick = true;


    // Start is called before the first frame update
    void Start()
    {
       _input.GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        mechAnim.SetFloat("Horizontal", _input.move.x);
        mechAnim.SetFloat("Vertical", _input.move.y);

        if(lazer == true)
        {
            lazerAnim.enabled = true;
        }
        else
        {
            lazerAnim.enabled = false;
        }
        
        if(wiper == true)
        {
            wiperAnim.enabled = true;
        }
        else
        {
            wiperAnim.enabled = false;
        }

        if(monitor == true)
        {
            monitorAnim.enabled = true;
        }
        else
        {
            monitorAnim.enabled = false;
        }

        if(throttle == true)
        {
            throttleAnim.enabled = true;
        }
        else
        {
            throttleAnim.enabled = false;
        }

        if(stick == true)
        {
            stickAnim.enabled = true;
        }
        else
        {
            stickAnim.enabled = false;
        }

    }
}
