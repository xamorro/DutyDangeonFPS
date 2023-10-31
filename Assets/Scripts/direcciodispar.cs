using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class direcciodispar : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        // ? sa pregunta indica " si es diana o null
        target = GameObject.Find("diana")?.transform;
    }


    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(target);
    }
}