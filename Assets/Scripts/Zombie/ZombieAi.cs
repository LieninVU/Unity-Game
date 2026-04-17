using UnityEngine;
using UnityEngine.AI;
using Utils;

public class ZombieAi : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 10f;
    [SerializeField] private float roamingDistanceMin = 0.0f;
    [SerializeField] private float roamingTimerMax = 2f;
    private NavMeshAgent navMeshAgent;
    private State stage;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startPosition;

    private enum State
    {
        Idle,
        Roaming
    }

    private void Start()
    {
        startPosition = transform.position; 
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        stage = startingState;
    }

    private void Update()
    {
        switch (stage)
        {
            default:
            case State.Idle:
                break;
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                break;
        }
    }


    private void Roaming()
    {
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition);
    }


    private Vector3 GetRoamingPosition()
    {
        return startPosition + Instruments.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }


}
