using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float jumpAmount=10;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpAction();
        }
    }

    private void JumpAction()
    {
        //rb.AddForce(transform.up * jumpAmount, ForceMode.Impulse);
        rb.AddForce(transform.up * jumpAmount, ForceMode.VelocityChange);
    }
}
