using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Latch : MonoBehaviour
{
    [SerializeField]
    private float latchRange = 10.0f;
    public bool canLatch = false;

    public GameObject latcherObject, humanObject;
    public GameObject latcherCameraRoot;

    public StarterAssets.ThirdPersonController latcherController;

    public Cinemachine.CinemachineVirtualCamera cameraManager;

    private PlayerInput latchInput;

    private HumanComponents currentHumanComponenets;


    // Start is called before the first frame update
    void Start()
    {
        //latcherController.enabled = true;
        cameraManager.Follow = latcherCameraRoot.transform;
        cameraManager.LookAt = latcherCameraRoot.transform;

        latchInput = latcherObject.GetComponent<PlayerInput>();

        latchInput.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, latchRange))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.collider.tag == "Human")
            {
                humanObject = hit.transform.gameObject;
                humanObject.TryGetComponent<HumanComponents>(out currentHumanComponenets);
                //humanInput = humanObject.GetComponent<PlayerInput>();
                //humanController = humanObject.GetComponent<StarterAssets.ThirdPersonController>();
                //humanCameraRoot = humanObject.transform.GetChild(0).gameObject;
                //humanLatchSpot = humanObject.transform;

                Debug.Log("Can Latch");
                canLatch = true;

            }
            else
            {
                canLatch = false;
            }


        }
    }

    void OnLatch() ///The latch action in new InputSystem
    {
        if (canLatch) /// checks to see if the player can latch (Raycast check in OnUpdate)
        {
            Debug.Log("Latched");
            latcherController.enabled = false;
            currentHumanComponenets.thirdPersonController.enabled = true;

            cameraManager.Follow = currentHumanComponenets.cameraRoot.transform;
            cameraManager.LookAt = currentHumanComponenets.cameraRoot.transform;
            latcherObject.transform.parent = currentHumanComponenets.latchSpot;

            latchInput.enabled = false;
            currentHumanComponenets.playerInput.enabled = true;

            SkinnedMeshRenderer latchermesh = latcherObject.transform.GetComponentInChildren(typeof (SkinnedMeshRenderer)) as SkinnedMeshRenderer;
            latchermesh.enabled = false;

            currentHumanComponenets.latcherOnBackMesh.SetActive(true);


        }
    }
}
