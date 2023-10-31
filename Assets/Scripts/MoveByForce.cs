using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveByForce : MonoBehaviour
{
    [SerializeField] private float forceAmount = 5;

    private Rigidbody rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.up * forceAmount, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //rb.AddForce(Vector3.forward * forceAmount, ForceMode.Force);
        rb.AddForce(transform.forward * forceAmount, ForceMode.Acceleration);
    }

}
