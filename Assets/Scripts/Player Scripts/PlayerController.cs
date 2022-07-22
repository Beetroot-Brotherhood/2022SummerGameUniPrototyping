using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    #region Movement Variables

    [HideInInspector]
    public Vector2 playerMovementInput;

    public float speed = 5f;
    [SerializeField]
    private float rotationSpeed = 20.0f;

    public bool walking = false;

    #endregion

    #region Camera variables

    private Transform cameraTransform;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    /// <summary>
    /// Function that lets the player move
    /// </summary>
    void MovePlayer()
    {
        Vector3 movement = new Vector3(playerMovementInput.x, 0.0f, playerMovementInput.y);

        movement = cameraTransform.forward * movement.z + cameraTransform.right * movement.x; /// moves the player in the direction the camera is looking
        movement.y = 0.0f;
        Debug.Log(movement);

        characterController.Move(movement * Time.deltaTime * speed); /// lets the player move around
        characterController.Move(Physics.gravity * Time.deltaTime); /// adds gravity to the object - lets the player fall

        if(movement.x == 0 & movement.z == 0)
        {
            walking = false;
        }
        else
        {
            walking = true;
        }
    }

    void RotatePlayer()
    {
        float playerTargetAngle = Mathf.Atan2(playerMovementInput.x, playerMovementInput.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y; /// caculates the angle the camera is facing
        Quaternion playerRotation = Quaternion.Euler(0.0f, playerTargetAngle, 0.0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, Time.deltaTime * rotationSpeed); /// turns the player in the direction the camera is looking
    }

    /// <summary>
    /// Gets the value from the InputSystem
    /// </summary>
    /// <param name="iv"></param>
    void OnMovement(InputValue iv)
    {
        playerMovementInput = iv.Get<Vector2>();
    }
}
