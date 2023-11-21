using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricPlayerController : MonoBehaviour, Controls.IPlayerActions
{
    CharacterController controller;

    public float moveSpeed = 5f;
    Vector3 forward;
    Vector3 right;

    // Variable to store ongoing movement input
    private Vector2 movementInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        forward = (GameObject.Find("CameraTarget").GetComponentInChildren<Camera>()).transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
    void Update()
    {
        // Calculate the movement direction based on camera orientation
        Vector3 moveDirection = (forward * movementInput.y + right * movementInput.x).normalized;

        // Move the player in the calculated direction
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        // Get the movement values from the input context
        movementInput = context.ReadValue<Vector2>();
    }
}
    


