using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private CharacterController characterController;

    #region Jump Variables

    private Vector3 playerVelocity;
    private bool isGrounded;
    [SerializeField]
    private float jumpHeight = 5.0f;
    [SerializeField]
    private bool jumpPressed = false;
    private float gravityValue = -9.81f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementJump();
    }

    /// <summary>
    /// Checks to see if the player can jump
    /// </summary>
    void OnJump()
    {
        Debug.Log("Jump Pressed");

        if (characterController.velocity.y == 0)
        {
            Debug.Log("Can Jump");
            jumpPressed = true;
        }
        else
        {
            Debug.Log("Can't Jump - In the air");
        }
    }

    /// <summary>
    /// Function that lets the player jump
    /// </summary>
    void MovementJump()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded)
        {
            playerVelocity.y = 0.0f;
        }

        if (jumpPressed && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.0f * gravityValue);
            jumpPressed = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
