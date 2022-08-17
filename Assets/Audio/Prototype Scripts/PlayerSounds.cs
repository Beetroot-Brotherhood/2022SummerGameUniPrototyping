using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! https://www.youtube.com/watch?v=naSl0DbqACA&ab_channel=OmnisepherGame%26Sound - WORKS!
public class PlayerSounds : MonoBehaviour
{
    [Header("Rhys' PlayerSounds Script - Called via the player controller")] 
    [Space]
    [Header("Footstep banks")] 
    [Space]
    [SerializeField] private FMODUnity.EventReference _footsteps;
    [SerializeField] private FMODUnity.EventReference _cloth;
    [SerializeField] private FMODUnity.EventReference _jingle;

    private FMOD.Studio.EventInstance footsteps;
    private FMOD.Studio.EventInstance cloth;
    private FMOD.Studio.EventInstance jingle;

    public Transform leftFoot;

    public Transform rightFoot;

    [SerializeField] [Range(0, 10)]
    public float surfaceType = 0; //* Creating a scroll bar in the inspector window which can be used to test the material values

    [Space] 
    [Header("Weapon banks")]
    [Space]
   
    [SerializeField] private FMODUnity.EventReference _swing;
    [SerializeField] private FMODUnity.EventReference _swingHit;

    private FMOD.Studio.EventInstance swing;
    private FMOD.Studio.EventInstance swingHit;



   


    private void Awake()
    {
        if (!_footsteps.IsNull)
        {
            footsteps = FMODUnity.RuntimeManager.CreateInstance(_footsteps);
        }

        if (!_cloth.IsNull)
        {
            cloth = FMODUnity.RuntimeManager.CreateInstance(_cloth);
        }

        if (!_jingle.IsNull)
        {
            jingle = FMODUnity.RuntimeManager.CreateInstance(_jingle);
        }

        if (!_swing.IsNull)
        {
            swing = FMODUnity.RuntimeManager.CreateInstance(_swing);

        }
    }

    public void PlayStepLeft()
    {
        if (footsteps.isValid())
        {
            //FMODUnity.RuntimeManager.AttachInstanceToGameObject(footsteps, transform);
            footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            GroundSwitchLeft();
            footsteps.setParameterByName("Footsteps", surfaceType); //! Enable for debugging using surface slider in the inspector
            footsteps.start();


            cloth.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            cloth.start();

            jingle.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            jingle.start();

        }

    }

    public void PlayStepRight()
    {
        if (footsteps.isValid())
        {
            //FMODUnity.RuntimeManager.AttachInstanceToGameObject(footsteps, transform);
            footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            GroundSwitchRight();
            footsteps.setParameterByName("Footsteps", surfaceType); //! Enable for debugging using surface slider in the inspector
            footsteps.start();

            cloth.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            cloth.start();
            
            jingle.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            jingle.start();

        }

    }


    private void GroundSwitchLeft()
    {
        RaycastHit hit;
        Ray ray = new Ray(leftFoot.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterialLeft;

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();
            if (surfaceRenderer)
            {
                if (surfaceRenderer.gameObject.TryGetComponent<FMODMaterialSoundOverride>(out FMODMaterialSoundOverride matOverride))
                {
                    surfaceType = (int)matOverride.materials;
                }
                else
                {

                    Debug.Log(surfaceRenderer.material.name);
                    if (surfaceRenderer.material.name.Contains("Wood") || surfaceRenderer.material.name.Contains("wood"))
                    {
                        //footsteps.setParameterByName("Footsteps", 1);
                        surfaceType = 1.0f;    
                    }
                    else if (surfaceRenderer.material.name.Contains("Stone") || surfaceRenderer.material.name.Contains("stone") || (surfaceRenderer.material.name.Contains("Rock") || surfaceRenderer.material.name.Contains("rock")))
                    {
                        //footsteps.setParameterByName("Footsteps", 2);
                        surfaceType = 2.0f;
                    }
                    else if (surfaceRenderer.material.name.Contains("Metal") || surfaceRenderer.material.name.Contains("metal")) 
                    {
                        //footsteps.setParameterByName("Footsteps", 3);
                        surfaceType = 3.0f;
                    }
                    else if (surfaceRenderer.material.name.Contains("Dirt") || surfaceRenderer.material.name.Contains("dirt")) 
                    {
                        //footsteps.setParameterByName("Footsteps", 4);
                        surfaceType = 4.0f;
                    }
                    else if (surfaceRenderer.material.name.Contains("Mud") || surfaceRenderer.material.name.Contains("mud")) 
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


 private void GroundSwitchRight()
    {
        RaycastHit hit;
        Ray ray = new Ray(rightFoot.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterialRight;

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();
            if (surfaceRenderer)
            {
                if (surfaceRenderer.gameObject.TryGetComponent<FMODMaterialSoundOverride>(out FMODMaterialSoundOverride matOverride))
                {
                    surfaceType = (int)matOverride.materials;
                }
                else
                {

                    Debug.Log(surfaceRenderer.material.name);
                    if (surfaceRenderer.material.name.Contains("Wood") || surfaceRenderer.material.name.Contains("wood"))
                    {
                        //footsteps.setParameterByName("Footsteps", 1);
                        surfaceType = 1.0f;    
                    }
                    else if (surfaceRenderer.material.name.Contains("Stone") || surfaceRenderer.material.name.Contains("stone") || (surfaceRenderer.material.name.Contains("Rock") || surfaceRenderer.material.name.Contains("rock")))
                    {
                        //footsteps.setParameterByName("Footsteps", 2);
                        surfaceType = 2.0f;
                    }
                    else if (surfaceRenderer.material.name.Contains("Metal") || surfaceRenderer.material.name.Contains("metal")) 
                    {
                        //footsteps.setParameterByName("Footsteps", 3);
                        surfaceType = 3.0f;
                    }
                    else if (surfaceRenderer.material.name.Contains("Dirt") || surfaceRenderer.material.name.Contains("dirt")) 
                    {
                        //footsteps.setParameterByName("Footsteps", 4);
                        surfaceType = 4.0f;
                    }
                    else if (surfaceRenderer.material.name.Contains("Mud") || surfaceRenderer.material.name.Contains("mud")) 
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

    public void playerSwing()
    {
        swing.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        swing.start();
    }


    public void playerSwingHit()
    {
        swingHit.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        swingHit.start();
    }











}
