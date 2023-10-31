using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacioRandom : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    [SerializeField] private float rotationSpeed = 20;

    private Vector3 randomRotationVector;

    // Start is called before the first frame update
    void Start()
    {
        randomRotationVector = new Vector3(0, Random.Range(-1f, 1f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        transform.Rotate(randomRotationVector.normalized * rotationSpeed * Time.deltaTime);
    }
}
