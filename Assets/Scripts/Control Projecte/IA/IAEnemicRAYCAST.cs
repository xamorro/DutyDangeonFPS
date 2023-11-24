using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemicRaycast: MonoBehaviour

{
    [SerializeField] private GameObject enemicgo;
    [SerializeField] private float patrolMaxRange = 15;
    [SerializeField] private float patrolMinRange = 5;
    [SerializeField] private float atackRange = 18;
    [SerializeField] private float FollowingRange = 30;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private LayerMask LayerPersonatge;


    [SerializeField] private Transform prepoint;
    [SerializeField] private Transform point;
    [SerializeField] private Transform visorPoint;
    [SerializeField] private Transform arma;
    [SerializeField] private Transform IniciDispar;

    private enum State
    {
        Patroling,
        ObserverTarget,
        Following,
        AtackTarget,
        ToInitialPosition
    }

    private State state;

    private FieldOfView vision;
    private NavMeshAgent agent;
    private Vector3 initialPosition;
    private Vector3 patrolPosition;
    //private Vector3 armaPosition;
    //private Vector3 armaRotation;

    private Transform target;

    private float nextTimeToShoot = 0;
    private float shootRate = 2;

    private void Awake()
    {
        vision = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        state = State.Patroling;
        target = vision.target;
        initialPosition = transform.position;
        patrolPosition = GetPatrolPosition();
        
        //armaPosition = arma.localPosition;
        //armaRotation = arma.localEulerAngles;
    }

    private void Update()
    {
        //Debug.DrawLine(IniciDispar.transform.position, IniciDispar.transform.position + IniciDispar.transform.forward * 100, Color.red);

        Debug.DrawRay(IniciDispar.transform.position, IniciDispar.transform.forward * 100, Color.red);

        StatsSoldat statssoldat = GetComponent<StatsSoldat>();
        float hpsoldat = statssoldat.VidaSoldat;

        if (hpsoldat > 0)
        {
            switch (state)
            {
                default:
                case State.Patroling:
                    agent.SetDestination(patrolPosition);
                    agent.speed = 2;
                    //arma.localPosition = armaPosition;
                    //arma.localEulerAngles = armaRotation;

                    if (agent.remainingDistance < 1f)
                        patrolPosition = GetPatrolPosition();

                    if (agent.speed < 5)
                    {
                        enemicgo.GetComponent<Animator>().SetFloat("run", agent.speed);
                    }

                    FindTarget();
                    break;

                case State.ObserverTarget:
                    agent.isStopped = true;
                    enemicgo.GetComponent<Animator>().Play("RifleIdle");

                    LookTarget();
                    if (Vector3.Distance(transform.position, target.position) <= FollowingRange)
                    {
                        agent.isStopped = false;
                        state = State.Following;
                    }

                    if (Vector3.Distance(transform.position, target.position) > vision.detectionRange)
                    {
                        agent.isStopped = false;
                        state = State.ToInitialPosition;
                    }

                    break;

                case State.Following:
                    agent.SetDestination(target.position);
                    agent.speed = 9;
                    if (agent.speed > 5)
                    {
                        enemicgo.GetComponent<Animator>().SetFloat("run", agent.speed);
                    }
                    if (!vision.canSeePlayer)
                    {
                        state = State.ToInitialPosition;
                    }
                    if (Vector3.Distance(transform.position, target.position) <= atackRange)
                    {
                        state = State.AtackTarget;
                    }

                    if (Vector3.Distance(transform.position, target.position) > FollowingRange)
                    {
                        state = State.ObserverTarget;
                    }

                    break;

                case State.AtackTarget:
                    agent.isStopped = true;
                    LookTarget();
                    if (vision.canSeePlayer)
                    {
                        ShootTimer();
                        enemicgo.GetComponent<Animator>().Play("DisparAturat");
                    }
                    if (Vector3.Distance(transform.position, target.position) > atackRange)
                    {
                        agent.isStopped = false;
                        state = State.Following;
                    }
                    break;

                case State.ToInitialPosition:
                    agent.SetDestination(initialPosition);
                    if (agent.remainingDistance < 1f)
                        state = State.Patroling;
                    break;
            }
        }
    }

    private Vector3 GetPatrolPosition()
    {
        Vector3 pointToPatrol = initialPosition;
        float randomRange = Random.Range(patrolMinRange, patrolMaxRange);
        Vector3 proposedPoint = initialPosition + Random.insideUnitSphere * randomRange;

        prepoint.position = proposedPoint;

        if (NavMesh.SamplePosition(proposedPoint, out NavMeshHit navMeshHit, 5f, NavMesh.AllAreas))
        {

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(navMeshHit.position, path);
            bool canReachPoint = path.status == NavMeshPathStatus.PathComplete;

            if (canReachPoint)
                pointToPatrol = navMeshHit.position;
        }

        point.position = pointToPatrol;
        return pointToPatrol;
    }

    private void FindTarget()
    {
        if (target == null)
            return;

        if (vision.canSeePlayer)
        {
            state = State.ObserverTarget;
        }



    }

    private void LookTarget()
    {
        Vector3 lookDirection = target.position - transform.position;
        lookDirection.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 90 * Time.deltaTime);
    }

    private void ShootTimer()
    {
        if (Time.time > nextTimeToShoot)
        {
            Debug.Log("dispara");
            PerformShoot();
            nextTimeToShoot = Time.time + 1 / shootRate;
            //Debug.DrawRay(visorPoint.transform.position, visorPoint.transform.forward, Color.green);
        }


    }

    private void PerformShoot()
    {
        
        if (Physics.Raycast(IniciDispar.transform.position, IniciDispar.transform.forward, out RaycastHit hit, Mathf.Infinity, LayerPersonatge))
        {
            Debug.Log("jugador ferit");
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;
        //Gizmos.DrawWireSphere(initialPosition, patrolMaxRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, FollowingRange);
    }

    public void IsStop(bool stop)
    {
        agent.isStopped = stop;
    }


}