using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public static PlayerVisual Instance { get; private set; }

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private const string IS_RUN = "IsRun";
    private const string ATTACK = "Attack";

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Player.Instance != null)
        {
            animator.SetBool(IS_RUN, Player.Instance.IsRunning());
            spriteRenderer.flipX = Player.Instance.IsFlipX();
        }
    }

    public void PlayAttack()
    {
        animator.SetTrigger(ATTACK);
    }
}