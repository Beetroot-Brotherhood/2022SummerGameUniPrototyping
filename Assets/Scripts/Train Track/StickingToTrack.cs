using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickingToTrack : MonoBehaviour
{
    public bool onTrack = false;
    public GameObject dollyCart, playerObject;
    public StarterAssets.FirstPersonController firstPersonController;
    public TrainController trainController;
    

    // Start is called before the first frame update
    void Start()
    {
        trainController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onTrack)
        {
            playerObject.transform.position = dollyCart.transform.position;
            playerObject.transform.rotation = dollyCart.transform.rotation;
            playerObject.transform.parent = dollyCart.transform;
            firstPersonController.enabled = false;
            trainController.enabled = true;

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
