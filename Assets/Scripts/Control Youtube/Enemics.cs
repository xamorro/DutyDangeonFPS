using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemics : MonoBehaviour{
    public enum collisionType { head, body}
    public collisionType damageType;
    public float Vida = 50f;
    [SerializeField] private GameObject enemigo;

    private void Start()
    {
        //Debug.Log(damageType);
    }
    public void DañoRecibido(float cantidad)
    {
         Vida -= cantidad;
        if (Vida <= 0f && enemigo.GetComponent<Animator>())
        {
            //Agafam es component animator de s'objecte pare principal.
            //transform.root.gameObject.GetComponent<Animator>().SetBool("die", true);
            
            StartCoroutine(DeleteMort());

            //PASAT ES PARE SERIALITZAT
            enemigo.GetComponent<Animator>().SetBool("die", true);


            //Die();

        }
        else if (Vida <= 0f && !GetComponent<Animator>())
        {
            Destroy(enemigo);
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
