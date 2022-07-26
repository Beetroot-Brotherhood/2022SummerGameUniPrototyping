using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioState : MonoBehaviour
{

    [SerializeField]
    //[EventRef]
    private FMODUnity.EventReference _radio;
    public HumanController humanController;

    private bool RadioOn = true;

    private FMOD.Studio.EventInstance radio;




   private void Awake()
   {
        if (!_radio.IsNull)
        {
            radio = FMODUnity.RuntimeManager.CreateInstance(_radio);
        }
   }


    void start()
    {
        radio.start();
    }
    

    void FixedUpdate()
    {

        if (RadioOn == true)
        {
            radio.setParameterByName("Footsteps", 0);

        }
        else 
        {
            radio.setParameterByName("Footsteps", 1);
        }

    }

}
