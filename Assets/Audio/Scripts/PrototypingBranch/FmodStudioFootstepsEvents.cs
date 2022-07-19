using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! By Rhys

//! https://www.youtube.com/watch?v=o381sP1G5bA&ab_channel=ScottGameSounds
//! https://www.youtube.com/watch?v=pVzgjleibQo&ab_channel=ScottGameSounds

public class FmodStudioFootstepsEvents : MonoBehaviour
{
    void PlayStep()
    {
        /* FMOD.Studio.EventInstance footstepSound;
        footstepSound = FMODUnity.RuntimeManager.CreateInstance ("event:/FootstepsMultiSurfaceTest");
        footstepSound.start(); */

        FMODUnity.RuntimeManager.PlayOneShot ("event:/FootstepsMultiSurfaceTest", GetComponent<Transform> ().position);
        Debug.Log("Animation Sound Trigger");


    }












}
