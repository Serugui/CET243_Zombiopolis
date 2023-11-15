using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricPlayerController : MonoBehaviour, Controls.IPlayerActions
{
    CharacterController controller;

    private Vector2 direction;
    public float moveSpeed = 5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        controller.Move(direction * moveSpeed * Time.deltaTime);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 readVector = context.ReadValue<Vector2>();
        Vector3 toConvert = new Vector3(readVector.x, 0, readVector.y);
        direction = context.ReadValue<Vector2>();
    }

    private Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rotation = Quaternion.Euler(0, 45.0f, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }
}
