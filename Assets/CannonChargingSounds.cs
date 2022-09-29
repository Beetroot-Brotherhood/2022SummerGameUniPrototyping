using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonChargingSounds : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodEvent;

    [SerializeField]
    [Range(0f, 1f)]
    private float chargeLevel;

    private void OnEnable()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    void Update()
    {
        instance.setParameterByName("cannonChargeLevel", chargeLevel);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, gameObject.transform);
    }

    private void OnDisable()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
