using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movperposicio: MonoBehaviour

{
    //Desde unity podem asignar un valor en aquesta variable que hem creat. Si no s'asigna , agafará es que posam aqui.
    [SerializeField] private float velocitat = 5f;

    


    void Start() // Start is called before the first frame update
    {


        //Sa posicio quan s'inicia es joc será 0, altura sa que té , 0. Si jo de altura pos 5, sempre que començará en aquella altura
        transform.position = new Vector3(0, transform.position.y, 0);


    }

    
    void Update()// Update is called once per frame
    {

        //Mou es transform cap a 0,0,1 * velocitat* temps entre frames.
        transform.Translate(transform.forward * velocitat * Time.deltaTime);


    }
}
