using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Latch : MonoBehaviour
{
    [SerializeField]
    private float latchRange = 10.0f;
    public bool canLatch = false;

    private bool terryDetected = false;

    private bool trentonDetected = false;

    public Transform terryLatchSpot;

    public Transform trentonLatchSpot;
    public GameObject latcherObject, terryObject, trentonObject;
    public GameObject latcherCameraRoot, terryCameraRoot, trentonCameraRoot;

    public StarterAssets.ThirdPersonController latcherController, terryController, trentonController;

    public Cinemachine.CinemachineVirtualCamera cameraManager;

    private PlayerInput latchInput;
    private PlayerInput terryInput;

    private PlayerInput trentonInput;

    public GameObject trenton, terry;




    // Start is called before the first frame update
    void Start()
    {
        //latcherController.enabled = true;
        cameraManager.Follow = latcherCameraRoot.transform;
        cameraManager.LookAt = latcherCameraRoot.transform;

        latchInput = latcherObject.GetComponent<PlayerInput>();
        terryInput = terryObject.GetComponent<PlayerInput>();

        trentonInput = trentonObject.GetComponent<PlayerInput>();

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
                Debug.Log("Can Latch");
                canLatch = true;

                if (hit.collider.gameObject == trenton)
                {
                    trentonDetected = true;
                    terryDetected = false;
                }
                else if (hit.collider.gameObject == terry)
                {
                    terryDetected = true;
                    trentonDetected = false;
                }
            }
            else
            {
                canLatch = false;
                terryDetected = false;
                trentonDetected = false;
            }


        }
    }

    void OnLatch() ///The latch action in new InputSystem
    {
        if (canLatch) /// checks to see if the player can latch (Raycast check in OnUpdate)
        {
            

            if (trentonDetected)
            {
                transform.position = trentonLatchSpot.position;
                transform.rotation = trentonLatchSpot.rotation;
                transform.parent = trentonObject.transform;
                latcherController.enabled = false;
                trentonController.enabled = true;

                cameraManager.Follow = trentonCameraRoot.transform;
                cameraManager.LookAt = trentonCameraRoot.transform;

                latchInput.enabled = false;
                trentonInput.enabled = true;

            }
            else if (terryDetected)
            {
                transform.position = terryLatchSpot.position;
                transform.rotation = terryLatchSpot.rotation;
                transform.parent = terryObject.transform;
                latcherController.enabled = false;
                terryController.enabled = true;

                cameraManager.Follow = terryCameraRoot.transform;
                cameraManager.LookAt = terryCameraRoot.transform;

                latchInput.enabled = false;
                terryInput.enabled = true;

            }
        }
    }
}
