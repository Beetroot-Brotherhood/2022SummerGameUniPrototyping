using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LatchFunction : MonoBehaviour
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

    private bool hasLatched;

    public StarterAssets.StarterAssetsInputs latcherInputs;

    public LayerMask layerMask;

    [SerializeField] private PlayerSounds playerSounds; //* This provides a reference point for the script which allows you to call the various sound functions which will be written into the 'PlayerSounds' script

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

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));

        if(!hasLatched)
        {
            currentHumanComponenets = null;
            if (Physics.Raycast(ray, out hit, latchRange, layerMask, QueryTriggerInteraction.UseGlobal)) {
                
                if (hit.collider.TryGetComponent<HumanComponents>(out currentHumanComponenets))
                {
                    canLatch = true;

                }

            }
            else {
                hit = new RaycastHit();
                canLatch = false;
                //ResetComponents();
            }

            /* if (Physics.Raycast(transform.position, transform.forward, out hit, latchRange))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * colliders[0].transform, Color.yellow);

                if (hit.collider.TryGetComponent<HumanComponents>(out currentHumanComponenets))
                {

                    Debug.Log("Can Latch");
                    canLatch = true;

                }
                
            } */
        }
        else
        {
            hit = new RaycastHit();
            canLatch = false;
        }

       

        if (latcherInputs.onLatch && !hasLatched && currentHumanComponenets != null)
        {
            hasLatched = true;
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

            playerSounds.PlayLatchedReaction();
        }
        else if (hasLatched && currentHumanComponenets != null)
        {
            if (currentHumanComponenets.starterAssetsInputs.onUnLatch)
            {
                hasLatched = false;
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
        else {
            latcherInputs.onLatch = false;
        }

        
        
    }

    void ResetComponents()
    {
        currentHumanComponenets = null;
    }
}
