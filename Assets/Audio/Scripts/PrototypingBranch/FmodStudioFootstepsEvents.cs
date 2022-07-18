using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! https://www.youtube.com/watch?v=o381sP1G5bA&ab_channel=ScottGameSounds
public class FmodStudioFootstepsEvents : MonoBehaviour
{
[Header("Written by Rhys")]

    [FMODUnity.EventRef]
    public string inputsound;
    
    void PlayStep()
    {
        FMODUnity.RuntimeManager.PlayOneShot (inputsound);
    }












}
