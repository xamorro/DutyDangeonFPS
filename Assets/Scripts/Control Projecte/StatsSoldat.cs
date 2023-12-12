using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsSoldat : MonoBehaviour
{
    public float VidaSoldat = 50f;
    private bool MunicioMollada = false;
    public GameObject soldat;
    private IAEnemicRaycast IAEnemic;
    [SerializeField] private bool dropMunicio;
    [SerializeField] private GameObject municio;


    // Start is called before the first frame update
    void Start()
    {
        IAEnemic = GetComponent<IAEnemicRaycast>();
    }
    public void Da�oRecibido(float cantidad)
    {
        VidaSoldat -= cantidad;
        if (VidaSoldat <= 0f && soldat.GetComponent<Animator>())
        {

            //A sa animacio die li posam true i far� que s'activi.
            soldat.GetComponent<Animator>().SetBool("dispar", false);
            IAEnemic.enabled = false;
            soldat.GetComponent<Animator>().SetBool("die", true);

            //Cridam una funcio que est� dins un altre escript de l'enemic i li deim que se posi true per  aturar l'enemic desde la navmesh.
            IAEnemic.IsStop(true);
            if (dropMunicio && !MunicioMollada) 
            {
                Debug.Log("Municio Mollada");
                transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
                Instantiate(municio, transform.position, Quaternion.identity);
                MunicioMollada = true;
            }
            
            //Eliminam l'enemic pasat 5 segons
            Destroy(gameObject, 5);

        }
    }
}
