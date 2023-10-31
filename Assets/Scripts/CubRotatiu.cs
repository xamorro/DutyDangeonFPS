using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubRotatiu : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    [SerializeField] private Vector3 rotationDirection;
    [SerializeField] private float rotationSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        transform.Rotate(rotationDirection.normalized * rotationSpeed * Time.deltaTime);
    }
}
