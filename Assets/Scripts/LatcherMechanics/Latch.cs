using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Latch : MonoBehaviour
{
    [SerializeField]
    private float latchRange = 10.0f;
    public bool canLatch = false;
    public float sphereRadius = 1.0f;

    public GameObject latcherObject;
    public GameObject latcherCameraRoot;

    public StarterAssets.ThirdPersonController latcherController;

    public Cinemachine.CinemachineVirtualCamera cameraManager;

    private PlayerInput latchInput;

    private HumanComponents currentHumanComponenets;

    public StarterAssets.StarterAssetsInputs latcherInputs;

    public Collider[] colliders;

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

        if(currentHumanComponenets == null)
        {

            if (Physics.Raycast(transform.position, transform.forward, out hit, latchRange))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * colliders[0].transform, Color.yellow);

                if (hit.collider.TryGetComponent<HumanComponents>(out currentHumanComponenets))
                {

                    Debug.Log("Can Latch");
                    canLatch = true;

                }
                
            }
        }
        else
        {
            canLatch = false;
        }

       

        if (latcherInputs.onLatch && currentHumanComponenets != null)
        {
            latcherInputs.onLatch = false;
            latcherController.enabled = false;
            currentHumanComponenets.thirdPersonController.enabled = true;

            cameraManager.Follow = currentHumanComponenets.cameraRoot.transform;
            cameraManager.LookAt = currentHumanComponenets.cameraRoot.transform;
            latcherObject.transform.parent = currentHumanComponenets.latchSpot;

            latchInput.enabled = false;
            currentHumanComponenets.playerInput.enabled = true;

            SkinnedMeshRenderer latchermesh = latcherObject.transform.GetComponentInChildren(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
            latchermesh.enabled = false;

            currentHumanComponenets.latcherOnBackMesh.SetActive(true);
        }

        if (currentHumanComponenets != null)
        {
            if (currentHumanComponenets.starterAssetsInputs.onUnLatch)
            {
                currentHumanComponenets.starterAssetsInputs.onUnLatch = false;
                currentHumanComponenets.thirdPersonController.enabled = false;
                latcherController.enabled = true;
                
                cameraManager.Follow = latcherCameraRoot.transform;
                cameraManager.LookAt = latcherCameraRoot.transform;
                latcherObject.transform.parent = null;

                currentHumanComponenets.playerInput.enabled = false;
                latchInput.enabled = true;
                
                SkinnedMeshRenderer latchermesh = latcherObject.transform.GetComponentInChildren(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
                latchermesh.enabled = true;

                currentHumanComponenets.latcherOnBackMesh.SetActive(false);

                ResetComponents();
            }
        }
        
    }

    void ResetComponents()
    {
        currentHumanComponenets = null;
    }
}
