using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemics : MonoBehaviour
{
    public float Vida = 50f;
    public void DañoRecibido(float cantidad)
    {
         Vida -= cantidad;
        if (Vida <= 0f)
        {
            //Abatido();
            Die();
            
        }

    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Abatido()
    {
        Debug.Log("hola");
    }

}
