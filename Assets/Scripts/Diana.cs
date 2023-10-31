using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{

    private int impactes = 0;
    [SerializeField] private int vidamaxima = 10;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        Debug.Log("Collision amb: " + obj.name);
        
        impactes++;

        if (impactes >= vidamaxima)
        {
            Destroy(gameObject);
        }
        
    }
}
