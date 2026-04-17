using UnityEngine;

public class Zombie : EnemyEntity
{
    public override void Death()
    {
        base.Death();
        
        // Уведомляем GameManager об убийстве зомби
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ZombieKilled();
        }
    }
}