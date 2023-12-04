using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsSoldat : MonoBehaviour
{
    public float VidaSoldat = 50f;
    public GameObject soldat;
    private IAEnemicRaycast IAEnemic;
    [SerializeField] private bool dropMunicio;
    [SerializeField] private GameObject municio;


    // Start is called before the first frame update
    void Start()
    {
        IAEnemic = GetComponent<IAEnemicRaycast>();
    }
    public void DañoRecibido(float cantidad)
    {
        VidaSoldat -= cantidad;
        if (VidaSoldat <= 0f && soldat.GetComponent<Animator>())
        {
            //------NO L'ACTIVAM PERQUE DINS ES DESTROY() JA LI POTS DONAR TEMPS
            //StartCoroutine(DeleteMort());


            //A sa animacio die li posam true i fará que s'activi.
            soldat.GetComponent<Animator>().SetBool("dispar", false);
            IAEnemic.enabled = false;
            soldat.GetComponent<Animator>().SetBool("die", true);

            //Cridam una funcio que está dins un altre escript de l'enemic i li deim que se posi true per  aturar l'enemic desde la navmesh.
            IAEnemic.IsStop(true);
            if (dropMunicio) 
            {
                Debug.Log("Municio Mollada");
                Instantiate(municio, transform.position, Quaternion.identity);
            }
            
            //Eliminam l'enemic pasat 5 segons
            Destroy(gameObject, 5);

        }
    }
}
