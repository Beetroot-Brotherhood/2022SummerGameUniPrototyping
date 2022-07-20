using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! By Rhys

//! https://www.youtube.com/watch?v=o381sP1G5bA&ab_channel=ScottGameSounds
//! https://www.youtube.com/watch?v=pVzgjleibQo&ab_channel=ScottGameSounds

public class FmodStudioFootstepsEvents : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodEvent;

    
    [SerializeField] [Range(0, 10)]
    public int surfaceType = 0; //* Creating a scroll bar in the inspector window which can be used to test the material values



    private RaycastHit hit;

    [SerializeField] private float RayDistance = 1.2f;

    public string[] MaterialTypes;
    [HideInInspector] public int DefaultMaterialValue;


    void start()
    {
        //!stepController = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps/FootstepsMultiSurfaceTest");
        instance = FMODUnity.RuntimeManager.CreateInstance("fmodEvent");
        instance.start();


        //instance.getParameterByName("SurfaceType", out surfaceType);
        //instance.start();
    }


    void FixedUpdate()
    {

        Debug.DrawRay(transform.position, Vector3.down * RayDistance, Color.blue);

        Debug.Log("Surface #: " +surfaceType);
    

        if (surfaceType == 0)
        {
            instance.setParameterByName("SurfaceType", surfaceType);
        }

        if (surfaceType == 1)
        {
            instance.setParameterByName("SurfaceType", surfaceType);
        }

        if (surfaceType == 2)
        {
            instance.setParameterByName("SurfaceType", surfaceType);
        }

        if (surfaceType == 3)
        {
            instance.setParameterByName("SurfaceType", surfaceType);
        }

        if (surfaceType == 4)
        {
            instance.setParameterByName("SurfaceType", surfaceType);
        }

    }



//! https://www.youtube.com/watch?v=xC8ssCCiRUQ&ab_channel=AlessandroFam%C3%A0 - Continue watching / Similar
//! https://alessandrofama.com/tutorials/fmod/unity/parameters


    private void PlayStep()
    {
        /* FMOD.Studio.EventInstance footstepSound;
        footstepSound = FMODUnity.RuntimeManager.CreateInstance ("event:/FootstepsMultiSurfaceTest");
        footstepSound.start(); */

        FMODUnity.RuntimeManager.PlayOneShot ("event:/Footsteps/FootStepsMultiSurfaceTest", GetComponent<Transform> ().position); //* Plays footstep sound bank

        FMODUnity.RuntimeManager.PlayOneShot ("event:/Footsteps/FootstepCloth", GetComponent<Transform> ().position); //* Plays footstep cloth layer sound bank

        FMODUnity.RuntimeManager.PlayOneShot ("event:/Footsteps/FootstepJingle", GetComponent<Transform> ().position); //* Plays footstep jingle layer sound bank

        //* Together, these create a random combination of sound layers which should provide really detailed, random footsteps

    }




}