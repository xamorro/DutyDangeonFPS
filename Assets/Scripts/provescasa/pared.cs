using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pared : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Som en: " + collision.gameObject.name + " i peg per colisio");
      
    }
    private void OnTriggerEnter(Collider colisioobjecte) //Colisio per trigger. Ha tenir trigger activat a collider i un rigidbody
    {
        Debug.Log("Ha arribat a meta: " + colisioobjecte.gameObject.name + " i peg per trigger");
    }
}
