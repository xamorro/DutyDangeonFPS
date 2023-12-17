using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AddMunicio : MonoBehaviour
{
    [SerializeField] private int addAmmo = 5;
    private int municioActual;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,60);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Si el player pasa per damunt la munici�, cridam una funcio de sumar municio.
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            ShootController municio = Player.gameObject.GetComponentInChildren<ShootController>();
            municio.AddMunicioEnemic(addAmmo);
            AudioManager.I.PlaySound(SoundName.CollectingAmmo, transform.position);
        }
        Destroy(gameObject);
    }
}
