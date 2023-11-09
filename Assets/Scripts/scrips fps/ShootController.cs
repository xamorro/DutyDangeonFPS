using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] float shootRate = 10f;
    [SerializeField] float range = 100f;

    [SerializeField] Camera fpsCamera;
    [SerializeField] ParticleSystem fireFX;
    [SerializeField] GameObject impactFX;
    
    [SerializeField] private LayerMask LayerPersonatge;

    [SerializeField] float ForçaImpacte = 4f;
    [SerializeField] int tempsreload = 5;

    //BALES I RECARREGUES
    private int MaxMunicio = 30;
    private int Municio;
    private bool Recargant = false;

    private float nextShootTime = 0f;

    private void Start()
    {
        Municio = MaxMunicio;
    }


    private void Update()
    {
        //Mos dibuixa un laser desde sa posicio de sa camera, cap a nes forward 
        Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red);

        if (Recargant)
        {
            return;
        }
        else if (Municio <= 0)
        {
            //Per poder cridar a un corutin RELOAD q hem creat.
            StartCoroutine(Reload());
            //Es return fa que no continui llegint es codi.
            return;
        }


       
    }

    public void Shoot()
    {
        
        //Li pos municio > 0 aqui perque sinó constantment dispara i resta sense importar es reload
        if (Time.time >= nextShootTime && Municio > 0)
        {
            nextShootTime = Time.time + 1 / shootRate;
            PerformShoot();
            
        }
    }

    private void PerformShoot()
    {
        Municio--;

        if (fireFX != null)
            fireFX.Play();
            //AUDIO FX

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out RaycastHit hit, Mathf.Infinity, ~LayerPersonatge))
        {

            if (hit.transform.CompareTag("Enemigo"))
            {
                Enemics target = hit.transform.GetComponent<Enemics>();
                target.DañoRecibido(9);
                //O TENC FICAT DFINS ENEMICS.CS// hit.transform.GetComponent<Animator>().SetBool("die", true);
            }

            if (hit.rigidbody != null)
            {
                //Aplica força cap enrera de on esteiem fent raycast
                hit.rigidbody.AddForce(-hit.normal * ForçaImpacte);
            }

            //Si sa distancia des hit es menor a nes rango que li hem donat, fe lo seguent
            if (hit.distance <= range)
            {
                //IMPACTE - Si tenim un objecte de impacte fe aixó
                if (impactFX != null)
                {
                    Vector3 bulletDirection = hit.point - fpsCamera.transform.position;
                    Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection.normalized);
                    GameObject impact = Instantiate(impactFX, hit.point, bulletRotation);
                    Destroy(impact, 2f);
                }
            }
        }
    }

    IEnumerator Reload()
    {
        Recargant = true;
        Debug.Log("Recargando");

        yield return new WaitForSeconds(tempsreload);

        Municio = MaxMunicio;
        Recargant = false;
        

    }


}
