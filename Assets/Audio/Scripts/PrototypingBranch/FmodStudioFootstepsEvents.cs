using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! By Rhys

//! https://www.youtube.com/watch?v=o381sP1G5bA&ab_channel=ScottGameSounds
//! https://www.youtube.com/watch?v=pVzgjleibQo&ab_channel=ScottGameSounds

public class FmodStudioFootstepsEvents : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;

    [SerializeField] [Range(0, 10)]
    public int surfaceType = 0;

    void start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    void FixedUpdate()
    {
        Debug.Log("Surface #: " +surfaceType);
        instance.setParameterByName("SurfaceType", surfaceType);
    }


//! https://www.youtube.com/watch?v=xC8ssCCiRUQ&ab_channel=AlessandroFam%C3%A0 - Continue watching / Similar


    void PlayStep()
    {
        /* FMOD.Studio.EventInstance footstepSound;
        footstepSound = FMODUnity.RuntimeManager.CreateInstance ("event:/FootstepsMultiSurfaceTest");
        footstepSound.start(); */

        FMODUnity.RuntimeManager.PlayOneShot ("event:/FootstepsMultiSurfaceTest", GetComponent<Transform> ().position);
        Debug.Log("Animation Sound Trigger");

    }










}
