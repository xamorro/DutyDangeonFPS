using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WASDcontrol : MonoBehaviour
{

    [SerializeField] private float speed = 5;
    [SerializeField] private bool NormalizarVector;

    [SerializeField] private bool controlforce;
    [SerializeField] private float forceAmount = 12;

    [SerializeField] private float quantitatbot = 5;
    
    
    private Rigidbody rigidb;

    void Start()
    {
        rigidb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction.z = 1;

        if (Input.GetKey(KeyCode.S))
            direction.z = -1;

        if (Input.GetKey(KeyCode.A))
            direction.x = -1;

        if (Input.GetKey(KeyCode.D))
            direction.x = 1;

        //CRIDA DIRECCIO AMB CORDENADES
        if (NormalizarVector)
            Move(direction);

        //CRIDA DIRECCIO AMB CORDENADES I FORCE
        if (controlforce)
            Move2(direction);


        if (Input.GetKeyDown(KeyCode.Space))
            salto();

    }
    
    private void Move(Vector3 direction)
     {
         //si el bool es si , fe aixó. Fa que les diagonals contin com a 1 per lo que fa que en diagonal el personatge es mogui a la mateixa velocidad que amb X o Z.
         if (NormalizarVector)
             direction = Vector3.Normalize(direction);

         transform.Translate(direction * speed * Time.deltaTime);
     }
  
    private void salto()
    {
        rigidb.AddForce(transform.up * quantitatbot, ForceMode.VelocityChange);
    }


   private void Move2(Vector3 direction)
    {
        //si el bool es si, mou cap a sa direccio amb force
        if (controlforce)
            direction = Vector3.Normalize(direction);
            rigidb.AddForce(direction* forceAmount, ForceMode.Force);
    }
  
}