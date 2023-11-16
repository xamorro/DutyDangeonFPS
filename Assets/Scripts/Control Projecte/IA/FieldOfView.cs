using System;
using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // El radi actuarà com a distància de detecció
    public float detectionRange;

    // L'angle del con de visió des del punt de vista del forward del gameObject
    [Range(0, 360)]
    public float coneAngle;

    public bool canSeePlayer;

    // Necessitem la referència pública al gameobject objectiu per a l'script de l'editor
    public Transform target;

    //LayerMask del player o target
    public LayerMask targetMask;

    //LayerMask del gameObjects que poden obstruïr la visió
    [SerializeField] private LayerMask obstructionMask;

    private void Start()
    {
        // llancem una corutina que s'executarà cada .2 segons ja que no cal fer
        // les comprovacions de detecció cada frame.
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        while (true)
        {
            // a cada iteraicó de la corutina cridem aquest mètode privat que revisa
            // si el target està dins el camp de visió especificat pels paràmetra de dalt
            FieldOfViewCheck();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void FieldOfViewCheck()
    {
        // feim un OverlapSphere que revisa si dins una esfera especificada hi ha colisions amb
        // colliders que pertanyin a la LayerMask passada per paràmetre
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, detectionRange, targetMask);

        if (rangeChecks.Length > 0)
        {
            // si hi ha alguna colisió agafarem la primera (només tenim un target que seria el jugador)
            // i n'obtenim la direcció
            Transform target = rangeChecks[0].transform;
            // Si es troba dins l'angle de visió hem d'averiguar si dins la distància 
            // especificada la línia de visió colisiona amb objectes especificats com a obstacles
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget > detectionRange)
                return;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // un cop tenim la direcció comprovem que entre la direcció especificada i 
            // nostre vector de forward no es suspera la meitat de l'angle de detecció
            if (Vector3.Angle(transform.forward, directionToTarget) < coneAngle / 2)
            {
                

                canSeePlayer = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask);
                    
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
