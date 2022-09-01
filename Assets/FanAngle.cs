using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanAngle : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodEvent;

    [SerializeField]
    [Range(0f, 1f)]
    private float fanAngle;

    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Mech/mech_Fan");
        instance.start();
    }

    void Update()
    {
        instance.setParameterByName("fanAngle", fanAngle);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, gameObject.transform);
    }
}