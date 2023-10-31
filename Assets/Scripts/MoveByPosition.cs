using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPosition : MonoBehaviour
{

    [SerializeField] private float velocitat = 0.1f;
    //[SerializeField] private Vector3 direccio;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direccio = transform.forward;

        //transform.position = transform.position + direccio * velocitat * Time.deltaTime;
        
        //transform.position += direccio * velocitat * Time.deltaTime;

        transform.Translate(Vector3.forward * velocitat * Time.deltaTime);
    }
}
