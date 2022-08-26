using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanComponents : MonoBehaviour
{
    public PlayerInput playerInput;
    public StarterAssets.ThirdPersonController thirdPersonController;
    public GameObject cameraRoot;
    [HideInInspector]
    public Transform latchSpot;
    public GameObject latcherOnBackMesh;
    public StarterAssets.StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
        latchSpot = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
