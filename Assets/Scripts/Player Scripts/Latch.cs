using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Latch : MonoBehaviour
{
    [SerializeField]
    private float latchRange = 10.0f;
    private bool canLatch = false;

    public Transform latchSpot;
    public GameObject latcherObject, humanObject;

    public PlayerController latcherController, humanController;

    public Cinemachine.CinemachineFreeLook cameraManager;

    private PlayerInput latchInput;
    private PlayerInput humanInput;

    public Text latchText;


    // Start is called before the first frame update
    void Start()
    {
        latcherController.enabled = true;
        cameraManager.Follow = latcherObject.transform;
        cameraManager.LookAt = latcherObject.transform;

        latchInput = latcherObject.GetComponent<PlayerInput>();
        humanInput = humanObject.GetComponent<PlayerInput>();

        latchInput.enabled = true;
        latchText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out hit, latchRange))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.collider.tag == "Enemy")
            {
                Debug.Log("Can Latch");
                canLatch = true;
                latchText.enabled = true;
            }
            else
            {
                canLatch = false;
                latchText.enabled = false;
            }
            
        }
    }

    void OnLatch() ///The latch action in new InputSystem
    {
        if (canLatch) /// checks to see if the player can latch (Raycast check in OnUpdate)
        {
            transform.position = latchSpot.position;
            transform.rotation = latchSpot.rotation;
            transform.parent = humanObject.transform;
            latcherController.enabled = false;
            humanController.enabled = true;

            cameraManager.Follow = humanObject.transform;
            cameraManager.LookAt = humanObject.transform;

            latchInput.enabled = false;
            humanInput.enabled = true;
        }
    }
}
