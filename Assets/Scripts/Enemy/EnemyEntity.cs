using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        if (currentHealth <= 0) 
        {
            // Уведомляем GameManager об убийстве врага
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EnemyKilled();
            }
            Destroy(gameObject);
        }
    }
}
