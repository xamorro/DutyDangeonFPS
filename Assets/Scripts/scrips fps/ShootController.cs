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

    private float nextShootTime = 0f;

    private void Update()
    {
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
        if (fireFX != null)
            fireFX.Play();
            //AUDIO FX

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out RaycastHit hit, Mathf.Infinity, ~LayerPersonatge))
        {

            if (hit.transform.CompareTag("Enemigo"))
            {
                Enemics target = hit.transform.GetComponent<Enemics>();
                target.DañoRecibido(9);
            }

            //if(hit.transform.TryGetComponent(out Enemy enemic))
            //{
            //    enemic.morir
            //}

            if (hit.distance <= range)
            {
                //IMPACTE
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
}
