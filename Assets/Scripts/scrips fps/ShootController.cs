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
    [SerializeField] GameObject bloodFX;
    [SerializeField] private GameObject arma;

    [Header("No impacta")]
    [SerializeField] private LayerMask LayerPersonatge;

    [SerializeField] float For�aImpacte = 4f;
    [SerializeField] float tempsreload = 1.5f;
    [SerializeField] int da�oarma = 9;

    [Header("Apuntar Arma")]
    [SerializeField] private Transform aimPoint;
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private float aimTime = .3f;

    private float interpolator;
    private bool resetAim;

    //BALES I RECARREGUES
    [Header("Arma")]
    [SerializeField] private  int MAXMUNICIOCARGADOR = 30;
    [SerializeField] private  int MAXMUNICIOARMA = 80;
    private int Cargador;
    private int Municio;
    public static event Action<int> MunicioModificada;
    public static event Action<int> MunicioMaxModificada;
    private bool Recargant = false;


    private float nextShootTime = 0f;

    private Animator armaAnimator;
    private float initialFOV;

    private void Start()
    {
        Cargador = MAXMUNICIOCARGADOR;
        Municio = MAXMUNICIOARMA;
        MunicioModificada?.Invoke(Cargador);
        MunicioMaxModificada?.Invoke(Municio);

        armaAnimator = arma.GetComponent<Animator>();

        initialFOV = fpsCamera.fieldOfView;
    }


    private void Update()
    {
        

        //Mos dibuixa un laser desde sa posicio de sa camera, cap a nes forward 
        Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red);

        if (resetAim)
        {
            AimOut();
        }

        if (Recargant)
        {
            return;
        }


    }

    public void Shoot()
    {
        
        //Li pos municio > 0 aqui perque sin� constantment dispara i resta sense importar es reload . Pot disparar si no est� recargant
        if (!Recargant && Time.time >= nextShootTime && Cargador > 0)
        {
            nextShootTime = Time.time + 1 / shootRate;
            PerformShoot();
            
        }
    }

    private void PerformShoot()
    {
        AudioManager.I.PlaySound(SoundName.AkShot);
        armaAnimator.Play("Retroces");
        //Resta 1 bala i l'invocam com event
        Cargador--;
        MunicioModificada?.Invoke(Cargador);

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
                IAEnemicRaycast iaenemic = hit.transform.gameObject.GetComponentInParent<IAEnemicRaycast>();
                iaenemic.AttackDistance(transform);

                //Agafam es component de sa part des cos que hem impactat
                PartsEnemics target = hit.transform.GetComponent<PartsEnemics>();

                //Si sa part des cos impactat �s head crida es Void public des pare de l'objecte impactat. Aqui depenguent de la zona que hem impactat, li donarem un d'any o un altre.
                if (target.damageType == PartsEnemics.collisionType.head)
                {
                    vidasoldat.Da�oRecibido(da�oarma * 8);

                }
                else if (target.damageType == PartsEnemics.collisionType.body)
                {
                    vidasoldat.Da�oRecibido(da�oarma * 2);
                }
                else
                {
                    vidasoldat.Da�oRecibido(da�oarma * 1);
                }
                
            }

            //Si impactam a una diana, Reproduir� un sonido y destruir� la zona. Apart ens retorna municio ja que es un lloc per practicar.
            if (hit.transform.root.gameObject.CompareTag("Diana"))
            {
                Destroy(hit.transform.gameObject);
                AudioManager.I.PlaySound(SoundName.SoundWood, transform.position);
                Municio++;
                MunicioMaxModificada?.Invoke(Municio);

                //value zona entrenamiento fe un if hit diana??
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
                if (impactFX != null && hit.transform.root.gameObject.CompareTag("Estructura"))
                {
                    GameObject impact = Instantiate(impactFX, hit.point, Quaternion.LookRotation(hit.normal));
                    impact.transform.position += impact.transform.forward / 1000;
                    Destroy(impact, 5f);

                    //-------per si o vull instancia cap a sa direcio on apuntava. ----////
                    //Vector3 bulletDirection = hit.point - fpsCamera.transform.position;

                    //Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection.normalized);

                    //GameObject impact = Instantiate(impactFX, hit.point, bulletRotation);
                }
                else if (impactFX != null && hit.transform.root.gameObject.CompareTag("Enemigo"))
                {
                    //-------per si o vull instancia cap a sa direcio on apunta. ----////
                    Vector3 bloodDirection = hit.point - fpsCamera.transform.position;
                    Quaternion bloodRotation = Quaternion.LookRotation(bloodDirection.normalized);
                    GameObject impact = Instantiate(bloodFX, hit.point, bloodRotation);
                    Destroy(impact, 5f);

                    //GameObject impact = Instantiate(bloodFX, hit.point, Quaternion.LookRotation(hit.normal));
                    //impact.transform.position += impact.transform.forward / 1000;
                    //Destroy(impact, 5f);
                }
            }
        }
    }

    //Per cridar la funcio de recargar, es pot activar quan no est�s recargant
    public void HandleReload()
    {
        if (!Recargant)
            StartCoroutine(Reload());   
    }

    private IEnumerator Reload()
    {
        Recargant = true;
        int balesConsumides = MAXMUNICIOCARGADOR - Cargador;
        //Debug.Log("Bales Consumides: " + balesConsumides);
        
        if (Municio == 0)
        {
            Debug.Log("No tens municio");
        }
        else if (balesConsumides != 0)
        {
            
            armaAnimator.Play("Recarga");
            AudioManager.I.PlaySound(SoundName.ReloadAK);
        }

        //Debug.Log("Recargando");

        yield return new WaitForSeconds(tempsreload);

        if (balesConsumides > Municio)
        {
            Cargador += Municio;
            Municio = 0;
        }
        else 
        {
            //Debug.Log("Ompliras el carregador");
            Cargador += balesConsumides;
            //A municio(180) - (30 -(30-6))  = 176
            Municio -= MAXMUNICIOCARGADOR - (MAXMUNICIOCARGADOR - balesConsumides);
        }

        MunicioModificada?.Invoke(Cargador);
        MunicioMaxModificada?.Invoke(Municio);
        Recargant = false;
        

    }

    //Quan apuntam, fa un lerp cap una posicio en concret.
    public void AimIn()
    {
        if (interpolator > 1)
            return;

        resetAim = false;
        interpolator += Time.deltaTime / aimTime;
        LerpAim();
    }

    //Quan deixam de apuntar, fa un lerp cap a la posicio inicial
    private void AimOut()
    {
        interpolator -= Time.deltaTime / aimTime;
        LerpAim();

        resetAim = interpolator > 0;
    }

    //Aqui feim el lerm del canvas de vida amb un field of view
    private void LerpAim()
    {
        weapon.position = Vector3.Lerp(weaponHolder.position, aimPoint.position, interpolator);
        weapon.rotation = aimPoint.rotation;
        fpsCamera.fieldOfView =  Mathf.Lerp(initialFOV, 40, interpolator);
    }

    public void SetAimOut()
    {
        resetAim = true;
    }

    //Municio Replegada dels enemics
    public void AddMunicioEnemic(int municio)
    {
        Municio = Municio + municio;
        MunicioMaxModificada?.Invoke(Municio);
    }
}
