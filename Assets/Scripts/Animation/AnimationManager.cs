using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [HideInInspector] public PlayerInputs _input;
    public Animator mechAnim;


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

        if (_input.readyLazer)
        {
            mechAnim.SetBool("ReadyLazer", true);
        }
        else
        {
            mechAnim.SetBool("ReadyLazer", false);
        }

    }
}
