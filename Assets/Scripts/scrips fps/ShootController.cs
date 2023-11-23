using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class ShootController : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] float shootRate = 10f;
    [SerializeField] float range = 100f;

    [SerializeField] Camera fpsCamera;
    [SerializeField] ParticleSystem fireFX;
    [SerializeField] GameObject impactFX;
    [SerializeField] private GameObject arma;

    [Header("No impacta")]
    [SerializeField] private LayerMask LayerPersonatge;

    [SerializeField] float ForçaImpacte = 4f;
    [SerializeField] int tempsreload = 2;
    [SerializeField] int dañoarma = 9;

    //BALES I RECARREGUES
    private int MaxMunicio = 30;
    private int Municio;
    public static event Action<int> MunicioModificada;
    private bool Recargant = false;

    //PROVES LERP
    private float tempsPasat;
    private float duracioApuntat = 1f;
    [SerializeField] GameObject posicioApuntat;
    private float nextShootTime = 0f;


    private void Start()
    {
        Municio = MaxMunicio;
        MunicioModificada?.Invoke(Municio);
    }


    private void Update()
    {
        tempsPasat += Time.deltaTime;
        

        //Mos dibuixa un laser desde sa posicio de sa camera, cap a nes forward 
        Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red);

        if (Recargant)
        {
            return;
        }

        //else if (Municio <= 0)
        //{
        //    //per poder cridar a un corutin reload q hem creat.
        //    StartCoroutine(Reload());
        //}



    }

    public void Shoot()
    {
        
        //Li pos municio > 0 aqui perque sinó constantment dispara i resta sense importar es reload . Pot disparar si no está recargant
        if (!Recargant && Time.time >= nextShootTime && Municio > 0)
        {
            nextShootTime = Time.time + 1 / shootRate;
            PerformShoot();
            
        }
    }

    public void Apuntar()
    {
        float percentageComplete = tempsPasat / duracioApuntat;
        arma.transform.position = Vector3.Lerp(arma.transform.position, posicioApuntat.transform.position, percentageComplete);
    }

    private void PerformShoot()
    {
        arma.GetComponent<Animator>().Play("IniciDispar");
        //Resta 1 bala i l'invocam com event
        Municio--;
        MunicioModificada?.Invoke(Municio);

        if (fireFX != null)
            fireFX.Play();
            //AUDIO FX

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out RaycastHit hit, Mathf.Infinity, ~LayerPersonatge))
        {
             //MIRA ES TAG DE SA COLISIO
            if (hit.transform.root.gameObject.CompareTag("Enemigo"))
            {
                //Agafam es component des pare de s'objecte impactat
                StatsSoldat vidasoldat = hit.transform.gameObject.GetComponentInParent<StatsSoldat>();

                //Agafam es component de sa part des cos que hem impactat
                PartsEnemics target = hit.transform.GetComponent<PartsEnemics>();

                //Si sa part des cos impactat és head crida es Void public des pare de l'objecte impactat.
                if (target.damageType == PartsEnemics.collisionType.head)
                {
                    vidasoldat.DañoRecibido(dañoarma * 8);

                }
                else if (target.damageType == PartsEnemics.collisionType.body)
                {
                    vidasoldat.DañoRecibido(dañoarma * 2);
                }
                else
                {
                    vidasoldat.DañoRecibido(dañoarma * 1);
                }
                
            }

            if (hit.transform.root.gameObject.CompareTag("Diana"))
            {
                Destroy(hit.transform.gameObject);
                //StatsSoldat vidadiana = hit.transform.gameObject.GetComponent<StatsSoldat>();
                //vidadiana.DañoRecibido(dañoarma);

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

    public IEnumerator Reload()
    {
        Recargant = true;
        Debug.Log("Recargando");

        yield return new WaitForSeconds(tempsreload);

        Municio = MaxMunicio;
        MunicioModificada?.Invoke(Municio);
        Recargant = false;
    }
}
