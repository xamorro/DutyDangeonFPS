using System;
using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // El radi actuar� com a dist�ncia de detecci�
    public float detectionRange;

    // L'angle del con de visi� des del punt de vista del forward del gameObject
    [Range(0, 360)]
    public float coneAngle;

    public bool canSeePlayer;

    // Necessitem la refer�ncia p�blica al gameobject objectiu per a l'script de l'editor
    public Transform target;

    //LayerMask del player o target
    public LayerMask targetMask;

    //LayerMask del gameObjects que poden obstru�r la visi�
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private Transform startVision;

    private void Start()
    {
        // llancem una corutina que s'executar� cada .2 segons ja que no cal fer
        // les comprovacions de detecci� cada frame.
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        while (true)
        {
            // a cada iteraic� de la corutina cridem aquest m�tode privat que revisa
            // si el target est� dins el camp de visi� especificat pels par�metra de dalt
            FieldOfViewCheck();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void FieldOfViewCheck()
    {
        // feim un OverlapSphere que revisa si dins una esfera especificada hi ha colisions amb
        // colliders que pertanyin a la LayerMask passada per par�metre
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, detectionRange, targetMask);

        if (rangeChecks.Length > 0)
        {
            // si hi ha alguna colisi� agafarem la primera (nom�s tenim un target que seria el jugador)
            // i n'obtenim la direcci�
            Transform target = rangeChecks[0].transform;
            // Si es troba dins l'angle de visi� hem d'averiguar si dins la dist�ncia 
            // especificada la l�nia de visi� colisiona amb objectes especificats com a obstacles
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget > detectionRange)
                return;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // un cop tenim la direcci� comprovem que entre la direcci� especificada i 
            // nostre vector de forward no es suspera la meitat de l'angle de detecci�
            if (Vector3.Angle(transform.forward, directionToTarget) < coneAngle / 2)
            {

                
                canSeePlayer = !Physics.Raycast(startVision.position, directionToTarget, distanceToTarget, obstructionMask);
                    
            }
            else
                canSeePlayer = false;
        }
        else
        {
            canSeePlayer = false;
        }
    }


}
