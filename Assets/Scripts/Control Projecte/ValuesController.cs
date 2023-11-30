using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValuesController : MonoBehaviour
{
    [Header("Texte Municio")]
    [SerializeField] private TextMeshProUGUI municio;
    [SerializeField] private TextMeshProUGUI maxMunicio;

    [Header("Texte Vida")]
    [SerializeField] private TextMeshProUGUI vidaJugador;

    private void OnEnable()
    {
        ShootController.MunicioModificada += UpdateAmmo;
        ShootController.MunicioMaxModificada += MaxUpdateAmmo;
        StatsPlayer.VidaModificada += UpdateVida;
    }

    private void OnDisable()
    {

        ShootController.MunicioModificada -= UpdateAmmo;
        ShootController.MunicioMaxModificada -= MaxUpdateAmmo;
        StatsPlayer.VidaModificada -= UpdateVida;
    }

    private void UpdateAmmo(int ammo)
    {
        municio.text = ammo.ToString();
        if (ammo < 10)
        {
            municio.color = Color.red;
        }
        else
        {
            municio.color = Color.white;
        }
    }

    private void MaxUpdateAmmo(int ammo)
    {
        maxMunicio.text = ammo.ToString();
    }

    private void UpdateVida(float vida)
    {
        vidaJugador.text = vida.ToString();
    }
}
