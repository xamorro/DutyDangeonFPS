using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemics2 : MonoBehaviour{
    public float Vida = 50f;
    [SerializeField] private GameObject enemigo;

    private void Start()
    {
    }
    public void DañoRecibido(float cantidad)
    {
         Vida -= cantidad;
        if (Vida <= 0f)
        {
            //Agafam es component animator de s'objecte pare principal.
            //transform.root.gameObject.GetComponent<Animator>().SetBool("die", true);
            
            StartCoroutine(DeleteMort());

            //PASAT ES PARE SERIALITZAT
            enemigo.GetComponent<Animator>().SetBool("die", true);


            //Die();

        }

    }
        

    void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator DeleteMort()
    {
        yield return new WaitForSeconds(5);
        //per root pare
        //Destroy(transform.root.gameObject);

        Destroy(enemigo);
    }

}
