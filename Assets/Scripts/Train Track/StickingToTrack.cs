using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StickingToTrack : MonoBehaviour
{
    public bool onTrack = false;
    public GameObject dollyCart, playerObject;
    public MechController mechController;
    public TrainController trainController;

    private PlayerInputs _input;
    

    // Start is called before the first frame update
    void Start()
    {
        trainController.enabled = false;
        _input = GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onTrack)
        {
            playerObject.transform.position = dollyCart.transform.position;
            playerObject.transform.rotation = dollyCart.transform.rotation;
            playerObject.transform.parent = dollyCart.transform;
            mechController.enabled = false;
            trainController.enabled = true;

        }
        if(onTrack && _input.unboard)
        {
            playerObject.transform.parent = null;
            mechController.enabled = true;
            trainController.enabled = false;
            onTrack = false;
            _input.unboard = false;
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
