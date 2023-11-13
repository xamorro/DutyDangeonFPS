using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSoldat : MonoBehaviour
{
    public float VidaSoldat = 50f;

    // Start is called before the first frame update
    void Start()
    {
    }
    public void DañoRecibido(float cantidad)
    {
        VidaSoldat -= cantidad;
        if (VidaSoldat <= 0f && gameObject.GetComponent<Animator>())
        {
            //Agafam es component animator de s'objecte pare principal.
            //transform.root.gameObject.GetComponent<Animator>().SetBool("die", true);

            StartCoroutine(DeleteMort());

            //PASAT ES PARE SERIALITZAT
            GetComponent<Animator>().SetBool("die", true);


            //Die();

        }
        else if (VidaSoldat <= 0f && !GetComponent<Animator>())
        {
            Destroy(gameObject);
        }

    }

    IEnumerator DeleteMort()
    {
        yield return new WaitForSeconds(5);
        //per root pare
        //Destroy(transform.root.gameObject);

        Destroy(gameObject);
    }
}
