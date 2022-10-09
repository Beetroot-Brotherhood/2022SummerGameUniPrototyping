using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PathCreation;
using PathCreation.Examples;

public class StickingToTrack : MonoBehaviour
{
    public static StickingToTrack instance;
    void Awake()
    {
        if (instance != null)
        {
           Debug.LogError("More than one StickingToTrack in scene!");
        }
        else
        {
             instance = this;
        }
    }
    public bool onTrack = false;
    public GameObject dollyCart, playerObject;
    public MechController mechController;
    public TrainController trainController;

    private OnPlayerInput _input;
    private PlayerInput _playerInput;

    public PathCreator _pathcreator;
    public PathFollower _pathfollower;

    // Start is called before the first frame update
    void Start()
    {
        trainController.enabled = false;
        _input = GetComponent<OnPlayerInput>();

    }

    private bool hasAttached = false;

    // Update is called once per frame
    void Update()
    {
        if(!onTrack)
        {
            _pathfollower.distanceTravelled = _pathcreator.path.GetClosestDistanceAlongPath(dollyCart.transform.position);
        }
        
        if (onTrack && !hasAttached)
        {
            
            playerObject.transform.position = dollyCart.transform.position;
            playerObject.transform.rotation = dollyCart.transform.rotation;
            playerObject.transform.parent = dollyCart.transform;
            mechController.enabled = false;
            trainController.enabled = true;
            hasAttached = true;
        }
        if(onTrack && _input.onBoard == true)
        {
            playerObject.transform.parent = null;
            mechController.enabled = true;
            trainController.enabled = false;
            onTrack = false;
            hasAttached = false;
            _input.onBoard = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Track")
        {
            onTrack = true;
        }
    }
}
