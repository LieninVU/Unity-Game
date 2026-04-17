using UnityEngine;
using UnityEngine.InputSystem; 

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private InputSystem_Actions inputSystemActions;

    public event System.Action OnAttackStarted;

    private void Awake()
    {
        Instance = this;
        inputSystemActions = new InputSystem_Actions();
        inputSystemActions.Enable();

        inputSystemActions.Player.Attack.started += OnAttackInput;
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        OnAttackStarted?.Invoke();
    }

    public Vector2 GetMovementVector()
    {
        return inputSystemActions.Player.Move.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        if (inputSystemActions != null)
        {
            inputSystemActions.Player.Attack.started -= OnAttackInput;
            inputSystemActions.Disable();
        }
    }
}