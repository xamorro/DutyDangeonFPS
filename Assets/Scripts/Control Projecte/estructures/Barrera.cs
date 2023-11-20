using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Barrera: MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public Transform barrera;

    private void Update()
    {
        float distance = Vector3.Distance(player.position, barrera.position);

        if (distance <= 10 )
        {
            animator.SetTrigger("Aprop");
        }
        else
        {
            animator.SetTrigger("Aprop");
        }
    }

}
