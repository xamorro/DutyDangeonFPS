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
    //[SerializeField] private GameObject bulletPrefab;

    [SerializeField] private LayerMask LayerPersonatge;
    [SerializeField] private bool sniper, sniperMuro;
    [SerializeField] private GameObject player;

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

    private Animator animator;

    private float waitTimer = 0;

    private bool dispar = false;

    private void Awake()
    {
        vision = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        animator = enemicgo.GetComponent<Animator>();
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
        dispar = false;
        //Raycast dispar fora cap random
        Debug.DrawRay(IniciDispar.transform.position, IniciDispar.transform.forward * 100, Color.red);

        StatsSoldat statssoldat = GetComponent<StatsSoldat>();
        float hpsoldat = statssoldat.VidaSoldat;


        if (hpsoldat > 0)
        {
            if (sniper || sniperMuro)
            {
                if (sniperMuro)
                {
                    visorPoint.LookAt(player.transform);
                } 
                    GetComponent<FieldOfView>().detectionRange = 60;
                agent.isStopped = true;
                //Debug.Log(vision.canSeePlayer);
                //Debug.Log(vistPrimerPic);
                LookTarget();
                if (vision.canSeePlayer)
                {
                    waitTimer += Time.deltaTime;
                    if (waitTimer >= 0.5f && sniper)
                    {
                        ShootTimer();
                    }else if (waitTimer >= 0.5f && sniperMuro)
                    {
                        arma.LookAt(player.transform);
                        IniciDispar.LookAt(player.transform);
                        ShootTimer();
                    }
                }
                else
                {
                    waitTimer = 0;
                }

                //if (Vector3.Distance(transform.position, target.position) > atackRange)
                //{
                //    agent.isStopped = false;
                //    state = State.Following;
                //}

            }
            else
            {
                animator.SetFloat("run", agent.velocity.magnitude);
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

                       

                        FindTarget();
                        break;

                    case State.ObserverTarget:
                        agent.isStopped = true;

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

                        agent.speed = 9;
                        agent.SetDestination(target.position);
                        if (agent.speed > 5)
                        {
                            //animator.SetFloat("run", agent.speed);
                        }
                        if (!vision.canSeePlayer)
                        {
                            state = State.ToInitialPosition;
                        }
                        if (Vector3.Distance(transform.position, target.position) <= atackRange)
                        {
                            state = State.AtackTarget;
                        }

                        //if (Vector3.Distance(transform.position, target.position) > FollowingRange)
                        //{
                        //    state = State.ObserverTarget;
                        //}

                        break;

                    case State.AtackTarget:
                        agent.isStopped = true;
                        LookTarget();
                        if (vision.canSeePlayer)
                        {
                            waitTimer += Time.deltaTime;
                            if (waitTimer >= 0.5f)
                            {
                                ShootTimer();
                            }
                            if (Vector3.Distance(transform.position, target.position) > atackRange)
                            {
                                agent.isStopped = false;
                                state = State.Following;
                            }
                        }
                        else
                        {
                            waitTimer = 0;
                        
                            vistPrimerPic = false;
                            
                            
                            //ShootTimer();
                            //enemicgo.GetComponent<Animator>().Play("DisparAturat");
                        }

                        break;

                    case State.DistanceHit:
                        agent.SetDestination(posicioplayer.position);
                        agent.speed = 9;
                        if (agent.speed > 5)
                        {
                            animator.SetFloat("run", agent.velocity.magnitude);
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

        animator.SetBool("dispar", dispar);
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
        dispar = true;
        if (Time.time > nextTimeToShoot)
        {
            PerformShoot();
            nextTimeToShoot = Time.time + 1 / shootRate;
            //Debug.DrawRay(visorPoint.transform.position, visorPoint.transform.forward, Color.green);
        }


    }

    private void PerformShoot()
    {
        AudioManager.I.PlaySound(SoundName.AkShot, transform.position);
        float randomnumber = Random.Range(0.5f, -0.5f);
        float randomnumberMuro = Random.Range(2.8f, -2.8f);
        if (sniperMuro && !sniper)
        {
            // POS AQUEST IF JA QUE PER FER ENEMIC MURO, HE EMPLEAT UN ALTRE TARGET EL CUAL AMB EL RANDOM NORMAL ME FER TOT ESTENS. Amb aquest random ja no pasa constant
            if (Physics.Raycast(IniciDispar.transform.position + new Vector3(randomnumberMuro, randomnumberMuro, 0f), IniciDispar.transform.forward, out RaycastHit hit, Mathf.Infinity, LayerPersonatge))
            {
                //SENSE AQUESTES 2 LINEES ME DISPARA INCLUS ESTANT MORT. 
                StatsSoldat statssoldat = GetComponent<StatsSoldat>();
                float hpsoldat = statssoldat.VidaSoldat;
                //--------------------------------------

                if (hit.transform.gameObject.CompareTag("Player") && hpsoldat > 0)
                {


                    //Debug.Log("jugador ferit");
                    //Agafam es component des pare de s'objecte impactat
                    StatsPlayer vidasoldat = hit.transform.gameObject.GetComponentInParent<StatsPlayer>();
                    vidasoldat.DañoRecibido(armaDaño);
                }

            }
        }else if (!sniperMuro && sniper)
        {
            if (Physics.Raycast(IniciDispar.transform.position + new Vector3(randomnumber, randomnumber, 0f), IniciDispar.transform.forward, out RaycastHit hit, Mathf.Infinity, LayerPersonatge))
            {
                //SENSE AQUESTES 2 LINEES ME DISPARA INCLUS ESTANT MORT. 
                StatsSoldat statssoldat = GetComponent<StatsSoldat>();
                float hpsoldat = statssoldat.VidaSoldat;
                //--------------------------------------

                if (hit.transform.gameObject.CompareTag("Player") && hpsoldat > 0)
                {


                    //Debug.Log("jugador ferit");
                    //Agafam es component des pare de s'objecte impactat
                    StatsPlayer vidasoldat = hit.transform.gameObject.GetComponentInParent<StatsPlayer>();
                    vidasoldat.DañoRecibido(armaDaño);
                }

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


    
}