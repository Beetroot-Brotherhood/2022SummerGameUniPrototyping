using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! https://www.youtube.com/watch?v=naSl0DbqACA&ab_channel=OmnisepherGame%26Sound - WORKS!
public class PlayerSounds : MonoBehaviour
{
   
   [SerializeField] private FMODUnity.EventReference _footsteps;
   
   private FMOD.Studio.EventInstance footsteps;

   [SerializeField] [Range(0, 10)]
    public float surfaceType = 0; //* Creating a scroll bar in the inspector window which can be used to test the material values


   private void Awake()
   {
        if (!_footsteps.IsNull)
        {
            footsteps = FMODUnity.RuntimeManager.CreateInstance(_footsteps);
        }
   }

    public void PlayStep()
    {
        if (footsteps.isValid())
        {
            //FMODUnity.RuntimeManager.AttachInstanceToGameObject(footsteps, transform);
            footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            GroundSwitch();
            footsteps.setParameterByName("Footsteps", surfaceType); //! Enable for debugging using surface slider in the inspector
            footsteps.start();
        }

    }


    private void GroundSwitch()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterial;

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();
            if (surfaceRenderer)
            {
                Debug.Log(surfaceRenderer.material.name);
                if (surfaceRenderer.material.name.Contains("Wood"))
                {
                    //footsteps.setParameterByName("Footsteps", 1);
                    surfaceType = 1.0f;    
                }
                else if (surfaceRenderer.material.name.Contains("Stone")) 
                {
                    //footsteps.setParameterByName("Footsteps", 2);
                    surfaceType = 2.0f;
                }
                else if (surfaceRenderer.material.name.Contains("Metal")) 
                {
                    //footsteps.setParameterByName("Footsteps", 3);
                    surfaceType = 3.0f;
                }
                else if (surfaceRenderer.material.name.Contains("Dirt")) 
                {
                    //footsteps.setParameterByName("Footsteps", 4);
                    surfaceType = 4.0f;
                }
                else if (surfaceRenderer.material.name.Contains("Rock")) 
                {
                    //footsteps.setParameterByName("Footsteps", 4);
                    surfaceType = 4.0f;
                }
                else if (surfaceRenderer.material.name.Contains("Mud")) 
                {
                    //footsteps.setParameterByName("Footsteps", 4);
                    surfaceType = 5.0f;
                }
                else if (surfaceRenderer.material.name.Contains("Blood")) 
                {
                    //footsteps.setParameterByName("Footsteps", 4);
                    surfaceType = 5.0f;
                }
                else
                {
                    //footsteps.setParameterByName("Footsteps", 0);
                    surfaceType = 0.0f;
                }
            }
        }
    }



















}
