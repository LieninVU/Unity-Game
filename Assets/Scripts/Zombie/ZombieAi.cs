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
    private float roamingTime = 0f;
    private Vector3 roamPosition;
    private Vector3 startPosition;


    private enum State
    {
        Idle,
        Roaming
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        stage = startingState;
        roamingTime = roamingTimerMax;
    }

    private void Update()
    {
        switch (stage)
        {
            default:
            case State.Idle:
                break;
            case State.Roaming:
                // Initialize roaming timer if this is the first time entering Roaming state
                if (roamingTime == 0f)
                {
                    roamingTime = roamingTimerMax;
                }
                
                roamingTime -= Time.deltaTime;
                if (roamingTime <= 0f)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                break;
        }
    }


    private void Roaming()
    {
        startPosition = transform.position;
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition);
        FlipX(startPosition, roamPosition);
    }


    private Vector3 GetRoamingPosition()
    {
        return startPosition + Instruments.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }



    private void FlipX(Vector3 positionNow, Vector3 positionGoal)
    {
        if ((positionNow.x - positionGoal.x) < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if ((positionNow.x - positionGoal.x) > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }


}
