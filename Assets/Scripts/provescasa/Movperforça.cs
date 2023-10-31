using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movperforça : MonoBehaviour


{
    Rigidbody objecte;
    [SerializeField] private float força = 15f;

    // Start is called before the first frame update
    void Start()
    {


        //ficam es component des rigidbody a dins una variable
        objecte = GetComponent<Rigidbody>();


    }


    private void FixedUpdate()// Update quan va per fisiques
    {


        objecte.AddForce(Vector3.forward * força, ForceMode.Acceleration);
        //objecte.AddForce(transform.forward * forceAmount, ForceMode.Force);


    }
}
