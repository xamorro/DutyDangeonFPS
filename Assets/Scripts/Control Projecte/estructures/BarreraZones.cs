using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreraZones : MonoBehaviour
{
    private float contadorenemics;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject clauCanvas;

    private enum Zona
    {
        portazona1,
        portazona2,
        portazona3
    }

    [SerializeField] private Zona zonaEnemics;


    private void Update()
    {
        switch (zonaEnemics)
        {
            default:
            case Zona.portazona1:
                contadorEnemigoZ1();
                if (contadorenemics == 0)
                {
                    animator.SetBool("Obrir", true);
                    GetComponent<BarreraZones>().enabled = false;
                }
                break;

            case Zona.portazona2:
                contadorEnemigoZ2();
                if (contadorenemics == 0)
                {
                    animator.SetBool("Obrir", true);
                    GetComponent<BarreraZones>().enabled = false;
                }
                break;
        }
    }

    private void contadorEnemigoZ1()
    {
        //Agafa objectes que tenen ficat un script EnemigoZ1 i que t'hes igual s'ordre de retorn.
        EnemigoZ1[] enemics = FindObjectsByType<EnemigoZ1>(FindObjectsSortMode.None);

        //.length me diu quants resultats hi ha
        //Debug.Log(enemics.Length);

        contadorenemics = enemics.Length;
    }

    private void contadorEnemigoZ2()
    {
        EnemigoZ2[] enemics = FindObjectsByType<EnemigoZ2>(FindObjectsSortMode.None);
        //Debug.Log(enemics.Length);
        contadorenemics = enemics.Length;
    }


}
