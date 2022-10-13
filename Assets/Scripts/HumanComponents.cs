using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using TheKiwiCoder;

public class HumanComponents : MonoBehaviour
{
    public PlayerInput playerInput;
    public StarterAssets.ThirdPersonController thirdPersonController;
    public GameObject cameraRoot;
    [HideInInspector]
    public Transform latchSpot;
    public GameObject latcherOnBackMesh;
    public StarterAssets.StarterAssetsInputs starterAssetsInputs;
    public NavMeshAgent navMeshAgent;
    public FieldOfView fieldOfView;
    public BehaviourTreeRunner behaviourTreeRunner;


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
