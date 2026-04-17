using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D rb;
    [SerializeField] private float Speed = 10f;
    private float minSpeed = 0.1f;

    private bool isRun = false;
    private bool flipX = false;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();

        GameInput.Instance.OnAttackStarted += HandleAttack;
    }

    private void HandleAttack()
    {
        if (PlayerVisual.Instance != null)
            PlayerVisual.Instance.PlayAttack();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        inputVector = inputVector.normalized;

        rb.MovePosition(rb.position + inputVector * (Speed * Time.fixedDeltaTime));

        isRun = Mathf.Abs(inputVector.x) > minSpeed || Mathf.Abs(inputVector.y) > minSpeed;

        if (Mathf.Abs(inputVector.x) > minSpeed)
        {
            flipX = inputVector.x < 0;
        }
    }

    public bool IsRunning() => isRun;
    public bool IsFlipX() => flipX;

    private void OnDestroy()
    {
        if (GameInput.Instance != null)
            GameInput.Instance.OnAttackStarted -= HandleAttack;
    }
}