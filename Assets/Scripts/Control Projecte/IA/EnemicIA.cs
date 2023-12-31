using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemicIA : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent navMeshEnemic;


    void Start()
    {
        navMeshEnemic = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            Move(Player.transform.position);
            float velocity = navMeshEnemic.velocity.magnitude / navMeshEnemic.speed;
            if (velocity > 0)
            {
                GetComponent<Animator>().SetFloat("run", velocity);
            }
        }

    }

    private void Move(Vector3 position)
    {
        navMeshEnemic.SetDestination(position);
    }

    public void IsStop(bool stop)
    {
        navMeshEnemic.isStopped = stop;
    }
        
}