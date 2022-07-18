using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    #region Movement Variables

    [SerializeField]
    private float speed = 5.0f;

    private Vector2 playerMovementInput;

    #endregion

    private void Start()
    {
        Debug.Log("Player created");
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(playerMovementInput.x, 0.0f, playerMovementInput.y);
        characterController.SimpleMove(movement * speed);
    }

    /// <summary>
    /// Gets the value from the InputSystem
    /// </summary>
    /// <param name="iv"></param>
    void OnMovement(InputValue iv)
    {
        Debug.Log("Movement pressed");
        playerMovementInput = iv.Get<Vector2>();
    }


}
