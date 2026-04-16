using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputSystem_Actions inputSystemActions;

    private void Awake()
    {
        Instance = this;
        inputSystemActions = new InputSystem_Actions();
        inputSystemActions.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = inputSystemActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}
