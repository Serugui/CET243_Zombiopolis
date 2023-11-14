using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricPlayerController : MonoBehaviour, Controls.IPlayerActions
{

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 readVector = context.ReadValue<Vector2>();
        Vector3 toConvert = new Vector3(readVector.x, 0, readVector.y);
        direction = context.ReadValue<Vector2>();
    }

    private Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rotation = Quaternion.Euler
    }
    
}
