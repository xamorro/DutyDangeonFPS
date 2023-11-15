using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSoldat : MonoBehaviour
{
    public float VidaSoldat = 50f;
    private EnemicIA EnemicIA;

    // Start is called before the first frame update
    void Start()
    {
        EnemicIA = GetComponent<EnemicIA>();
    }
    public void DañoRecibido(float cantidad)
    {
        VidaSoldat -= cantidad;
        if (VidaSoldat <= 0f && gameObject.GetComponent<Animator>())
        {
            //------NO L'ACTIVAM PERQUE DINS ES DESTROY() JA LI POTS DONAR TEMPS
            //StartCoroutine(DeleteMort());

 
            GetComponent<Animator>().SetBool("die", true);
            EnemicIA.IsStop(true);

            Destroy(gameObject,5);

        }
    }


//    IEnumerator DeleteMort()
//    {
//        yield return new WaitForSeconds(5);
//        //per root pare
//        //Destroy(transform.root.gameObject);

//        Destroy(gameObject);
//    }

}
