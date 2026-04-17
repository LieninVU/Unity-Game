using UnityEngine;
using UnityEngine.AI;
using Utils;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private float chaseRange = 15f; // Дистанция, при которой начинается погоня

    private NavMeshAgent navMeshAgent;
    private Transform player;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        // Поиск игрока по тегу
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Не найден объект с тегом 'Player'!");
        }
    }

    private void Update()
    {
        // Если игрок найден и в зоне видимости - преследовать его
        if (player != null && Vector3.Distance(transform.position, player.position) <= chaseRange)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector3 targetPosition = player.position;
        navMeshAgent.SetDestination(targetPosition);
        FlipX(transform.position, targetPosition);
    }

    private void FlipX(Vector3 positionNow, Vector3 positionGoal)
    {
        if ((positionNow.x - positionGoal.x) < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Смотрит направо
        }
        else if ((positionNow.x - positionGoal.x) > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Смотрит налево
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}