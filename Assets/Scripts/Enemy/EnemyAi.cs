using UnityEngine;
using UnityEngine.AI;
using Utils;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private State startingState = State.Patrol;
    [SerializeField] private float roamingDistanceMax = 8f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 3f;
    [SerializeField] private float chaseRange = 15f; // Дистанция, при которой начинается погоня

    private NavMeshAgent navMeshAgent;
    private State currentState;
    private Transform player;
    private float roamTimer;
    private Vector3 roamPosition;
    private Vector3 startPosition;

    private enum State
    {
        Idle,
        Patrol,
        Chasing
    }

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

        currentState = startingState;
        roamTimer = roamingTimerMax;
        startPosition = transform.position;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                break;

            case State.Patrol:
                roamTimer -= Time.deltaTime;
                if (roamTimer <= 0f)
                {
                    Roam();
                    roamTimer = roamingTimerMax;
                }

                // Если игрок в зоне видимости — начать погоню
                if (player != null && Vector3.Distance(transform.position, player.position) <= chaseRange)
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                if (player == null)
                {
                    currentState = State.Patrol;
                    navMeshAgent.ResetPath();
                    break;
                }

                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (distanceToPlayer > chaseRange)
                {
                    // Игрок ушёл слишком далеко — вернуться к патрулированию
                    currentState = State.Patrol;
                    navMeshAgent.SetDestination(roamPosition); // Продолжить путь патруля
                }
                else
                {
                    ChasePlayer();
                }
                break;
        }
    }

    private void Roam()
    {
        startPosition = transform.position;
        roamPosition = GetRoamingPosition();

        if (NavMesh.SamplePosition(roamPosition, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
            FlipX(startPosition, hit.position);
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startPosition + Instruments.GetRandomDir() * Random.Range(roamingDistanceMin, roamingDistanceMax);
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
        if (player != null && currentState == State.Chasing)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }

        if (currentState == State.Patrol && roamPosition != default)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(roamPosition, 0.5f);
        }
    }
}