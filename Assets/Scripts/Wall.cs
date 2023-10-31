using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        Debug.Log("Collision amb: " + obj.name);

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        rb.AddForce(Vector3.back * 40, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ha arribat a meta: " + other.gameObject.name);
    }

    




}
