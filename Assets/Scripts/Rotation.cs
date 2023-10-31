using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);


        Vector3 rotationToAdd = new Vector3(0, 1, 0);

        transform.eulerAngles = transform.eulerAngles + rotationToAdd.normalized * Time.deltaTime * rotationSpeed;
    }
}
