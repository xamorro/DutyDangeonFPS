using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemics : MonoBehaviour
{
    public float Vida = 50f;
    public void DaņoRecibido(float cantidad)
    {
         Vida -= cantidad;
        if (Vida <= 0f)
        {
            Die();
        }

    }

    void Die()
    {
        Destroy(gameObject);
    }
}
