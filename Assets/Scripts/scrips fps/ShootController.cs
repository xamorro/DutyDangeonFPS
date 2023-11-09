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

    [SerializeField] float For�aImpacte = 4f;

    //BALES I RECARREGUES
    private int MaxMunicio = 30;
    private int Municio;

    private float nextShootTime = 0f;

    private void Start()
    {
        Municio = MaxMunicio;
    }


    private void Update()
    {
        if (Municio <= 0)
        {
            Reload();
            //Es return fa que no continui llegint es codi.
            return;
        }

        //Mos dibuixa un laser desde sa posicio de sa camera, cap a nes forward 
        Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red);
    }

    public void Shoot()
    {
        

        if (Time.time >= nextShootTime)
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
                target.Da�oRecibido(9);
                hit.transform.GetComponent<Animator>().SetBool("die", true);
            }

            if (hit.rigidbody != null)
            {
                //Aplica for�a cap enrera de on esteiem fent raycast
                hit.rigidbody.AddForce(-hit.normal * For�aImpacte);
            }

            //Si sa distancia des hit es menor a nes rango que li hem donat, fe lo seguent
            if (hit.distance <= range)
            {
                //IMPACTE - Si tenim un objecte de impacte fe aix�
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

    void Reload()
    {
        Debug.Log("Recargando");
        Municio = MaxMunicio;
    }


}
