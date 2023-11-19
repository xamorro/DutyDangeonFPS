using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreraZones : MonoBehaviour
{
    private float contadorenemics;
    [SerializeField] private Animator animator;

    private void OnTriggerStay(Collider other)
    {
        contador();
        if (contadorenemics == 0)
        {
            animator.SetBool("Aprop", true);
        }
        else
        {
            animator.SetBool("Aprop", false);
        }
    }

    private void contador()
    {
        contadorenemics = 0;

        GameObject[] enemics = GameObject.FindGameObjectsWithTag("EnemigoZ1");

        foreach (GameObject e in enemics)
        {
            contadorenemics = contadorenemics + 1;
        }

        Debug.Log(contadorenemics);
    }
}
