using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! https://www.youtube.com/watch?v=naSl0DbqACA&ab_channel=OmnisepherGame%26Sound - WORKS!
public class PlayerSounds : MonoBehaviour
{
   
    [SerializeField] private FMODUnity.EventReference _footsteps;
    [SerializeField] private FMODUnity.EventReference _cloth;
    [SerializeField] private FMODUnity.EventReference _jingle;
    [SerializeField] private FMODUnity.EventReference _weapon;
    
    [SerializeField] private FMODUnity.EventReference _latchedReaction;
    
    private FMOD.Studio.EventInstance footsteps;
    private FMOD.Studio.EventInstance cloth;
    private FMOD.Studio.EventInstance jingle;
    private FMOD.Studio.EventInstance weapon;
    private FMOD.Studio.EventInstance latchedReaction;


    public Transform spanner; 

    public Transform leftFoot;

    public Transform rightFoot;

    [SerializeField] [Range(0, 10)]
    public float surfaceType = 0; //* Creating a scroll bar in the inspector window which can be used to test the material values




    //! Ported from CheckTerrainTexture script
    public Transform playerTransform;
    public Terrain t;
    
    public int posX;
    public int posZ;
    public float[] textureValues;


    void Start () 
    {
        t = Terrain.activeTerrain;
        playerTransform = gameObject.transform;
    }



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

        if (!_weapon.IsNull)
        {
            weapon = FMODUnity.RuntimeManager.CreateInstance(_weapon);
        }

        if (!_latchedReaction.IsNull)
        {
            latchedReaction = FMODUnity.RuntimeManager.CreateInstance(_latchedReaction);
        }
   }

    public void PlayLatchedReaction()
    {
        latchedReaction.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        latchedReaction.start();
    } 

    public void PlayWeapon()
    {
         weapon.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(spanner.transform.position));
         weapon.start();
    }

    public void PlayStepLeft()
    {
        try {
            if (footsteps.isValid())
            {
                //GetTerrainTexture();
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
        catch (System.Exception){}
    }

    public void PlayStepRight()
    {
        try {
            if (footsteps.isValid())
            {
                //GetTerrainTexture();
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
        catch (System.Exception){}
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




    //! Ported from CheckTerrainTexture script
    public void GetTerrainTexture()
    {
        ConvertPosition(playerTransform.position);
        CheckTexture();
    }

    void ConvertPosition(Vector3 playerPosition)
    {
        Vector3 terrainPosition = playerPosition - t.transform.position;
    
        Vector3 mapPosition = new Vector3
        (terrainPosition.x / t.terrainData.size.x, 0,
        terrainPosition.z / t.terrainData.size.z);
    
        float xCoord = mapPosition.x * t.terrainData.alphamapWidth;
        float zCoord = mapPosition.z * t.terrainData.alphamapHeight;
    
        posX = (int)xCoord;
        posZ = (int)zCoord;
    }

    void CheckTexture()
  {
    float[,,] aMap = t.terrainData.GetAlphamaps (posX, posZ, 1, 1);
    textureValues[0] = aMap[0,0,0];
    textureValues[1] = aMap[0,0,1];
    textureValues[2] = aMap[0,0,2];
    textureValues[3] = aMap[0,0,3];
  }












}
