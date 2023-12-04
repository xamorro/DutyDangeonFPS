using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.XR;

public class IAEnemicRaycast : MonoBehaviour

{
    [SerializeField] private GameObject enemicgo;
    [SerializeField] private float patrolMaxRange = 15;
    [SerializeField] private float patrolMinRange = 5;
    [SerializeField] private float atackRange = 18;
    [SerializeField] private float armaDaño = 8;
    [SerializeField] private float FollowingRange = 30;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private LayerMask LayerPersonatge;
    [SerializeField] private bool sniper;

    private float vidaSoldat;

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
        ToInitialPosition,
        DistanceHit
    }

    private State state;

    private FieldOfView vision;
    private NavMeshAgent agent;
    private Vector3 initialPosition;
    private Vector3 patrolPosition;
    //private Vector3 armaPosition;
    //private Vector3 armaRotation;

    private Transform target;
    private Transform posicioplayer;

    private bool vistPrimerPic = false;

    private float nextTimeToShoot = 0;
    private float shootRate = 1;

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
        //Raycast dispar fora cap random
        Debug.DrawRay(IniciDispar.transform.position, IniciDispar.transform.forward * 100, Color.red);

        StatsSoldat statssoldat = GetComponent<StatsSoldat>();
        float hpsoldat = statssoldat.VidaSoldat;


        if (hpsoldat > 0)
        {
            if (sniper)
            {

                GetComponent<FieldOfView>().detectionRange = 60;
                agent.isStopped = true;
                Debug.Log(vision.canSeePlayer);
                Debug.Log(vistPrimerPic);
                LookTarget();
                if (vision.canSeePlayer)
                {
                    if (!vistPrimerPic)
                    {
                        StartCoroutine(ExampleCoroutine());
                        vistPrimerPic = true;
                        
                    }
                }
                else
                {
                    vistPrimerPic = false;
                }
                //if (Vector3.Distance(transform.position, target.position) > atackRange)
                //{
                //    agent.isStopped = false;
                //    state = State.Following;
                //}

            }
            else
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

                    case State.DistanceHit:
                        agent.SetDestination(posicioplayer.position);
                        agent.speed = 9;
                        if (agent.speed > 5)
                        {
                            enemicgo.GetComponent<Animator>().SetFloat("run", agent.speed);
                        }
                        if (Vector3.Distance(transform.position, posicioplayer.position) < atackRange)
                        {
                            state = State.AtackTarget;
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
        float randomnumber = Random.Range(1.2f, -1.2f);
        if (Physics.Raycast(IniciDispar.transform.position + new Vector3(0f, randomnumber, 0f), IniciDispar.transform.forward, out RaycastHit hit, Mathf.Infinity, LayerPersonatge))
        {

            if (hit.transform.gameObject.CompareTag("Player"))
            {
                enemicgo.GetComponent<Animator>().SetBool("dispar", true);
                Debug.Log("jugador ferit");
                //Agafam es component des pare de s'objecte impactat
                StatsPlayer vidasoldat = hit.transform.gameObject.GetComponentInParent<StatsPlayer>();
                vidasoldat.DañoRecibido(armaDaño);
            }

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

    public void AttackDistance(Transform player)
    {
        state = State.DistanceHit;
        posicioplayer = player;
        //agent.SetDestination(player.position);
        //Debug.Log(player.position);
    }


    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        
        while (vision.canSeePlayer)
        {
            ShootTimer();
            
            yield return null;
        }
        

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);

    }
}