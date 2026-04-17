using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField] private int damage = 25;
    private Rigidbody2D rb;
    [SerializeField] private float Speed = 10f;
    private float minSpeed = 0.1f;
    private PolygonCollider2D colider;
    private bool isRun = false;
    private bool flipX = false;


    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        colider = GetComponent<PolygonCollider2D>();
        GameInput.Instance.OnAttackStarted += HandleAttack;
    }

    private void Start()
    {
        ColliderOff();
    }

    private void HandleAttack()
    {
        ColliderOn();
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
        
        Vector2[] points = colider.points;

        for(int i = 0; i < points.Length; i++)
        {
            points[i].x = Mathf.Abs(points[i].x) * (flipX ? -1 : 1); 
        }
        colider.points = points;
        
    }

    public bool IsRunning() => isRun;
    public bool IsFlipX() => flipX;


    public void ColliderOff()
    {
        colider.enabled = false;
    }

    private void ColliderOn()
    {
        colider.enabled = true;
    }

    private void OnDestroy()
    {
        if (GameInput.Instance != null)
            GameInput.Instance.OnAttackStarted -= HandleAttack;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamage(damage);
        }
    }
}