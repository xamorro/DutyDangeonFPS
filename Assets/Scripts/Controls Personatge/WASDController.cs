using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDController : MonoBehaviour
{
    // passem serialitzat la velocitat desitjada de moviment i si s'ha de normalitzar o no el vecor
    // de moviment
    [SerializeField] private float speed = 10;
    [SerializeField] private bool needToNormalize;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //capturem els inputs en forma de tecles i establim en -1 o 1 els valors dels
        // diferents components del vector de moviment 'direction'
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction.z = 1;

        if (Input.GetKey(KeyCode.S))
            direction.z = -1;

        if (Input.GetKey(KeyCode.A))
            direction.x = -1;

        if (Input.GetKey(KeyCode.D))
            direction.x = 1;

        Move(direction);
    }

    /// <summary>
    /// Aquest mètodes es crida des de l'Update i reb com a paràmetre el vector de direcció
    /// El podem normalitzar i utilitzar le mètode Translate de transform per moure en aquella direcció.
    /// Framerate independent amb Time.deltatime.
    /// </summary>
    /// <param name="direction"></param>
    private void Move(Vector3 direction)
    {
        if (needToNormalize)
            direction = Vector3.Normalize(direction);

        transform.Translate(direction * speed * Time.deltaTime);
    }
}