using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioState : MonoBehaviour
{

    [SerializeField] private FMODUnity.EventReference _radio;

    private FMOD.Studio.EventInstance radio;


    public HumanController humanController;


    [SerializeField] [Range(0, 1)]
    private float radioState = 1.0f;

   private void Awake()
   {
        if (!_radio.IsNull)
        {
            radio = FMODUnity.RuntimeManager.CreateInstance(_radio);
        }
   }


    void FixedUpdate()
    {
        radio.setParameterByName("RadioState", radioState);
        //radio.setParameterByName("RadioState", radioState);

        if (radioState == 1.0f)
        {
            radio.setParameterByName("RadioState", 0.0f);
            Debug.Log("Radio On");
        }
        else 
        {
            radio.setParameterByName("RadioState", 1.0f);
            Debug.Log("Radio Off");
        }


    }
}