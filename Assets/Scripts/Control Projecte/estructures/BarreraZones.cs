using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreraZones : MonoBehaviour
{
    private float contadorenemics;
    [SerializeField] private Animator animator;

    private enum Zona
    {
        zona1,
        zona2,
        zona3
    }

    [SerializeField] private Zona zona;

    private void OnTriggerStay(Collider other)
    {
        contador();
        if (contadorenemics == 0)
        {
            animator.SetTrigger("Aprop");
        }
    }

    private void contador()
    {

        EnemigoZ1[] enemics = FindObjectsByType<EnemigoZ1>(FindObjectsSortMode.None);

        Debug.Log(enemics.Length);

        contadorenemics = enemics.Length;
    }
}
