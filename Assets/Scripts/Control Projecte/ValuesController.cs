using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;
using UnityEditor.ShaderGraph;

public class ValuesController : MonoBehaviour
{
    [Header("Texte Municio")]
    [SerializeField] private TextMeshProUGUI municio;
    [SerializeField] private TextMeshProUGUI maxMunicio;

    [Header("Texte Vida")]
    [SerializeField] private TextMeshProUGUI vidaJugador;
    [SerializeField] private Slider sliderVida;
    [SerializeField] private Image colorVida;
    private float percentatgeVida;
    //private float vidaLerp;
    //private float timeScale = 0;
    

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

        percentatgeVida = vida / 100;
        sliderVida.value = percentatgeVida;
        if (percentatgeVida < 0.5f)
        {
            colorVida.color = Color.yellow;
            if (percentatgeVida < 0.2f)
            {
                colorVida.color = Color.red;
            }
        }
        
        //timeScale += Time.deltaTime * 2;
        //sliderVida.value = Mathf.Lerp(1, 0, timeScale);

        //vidaLerp = percentatgeVida;
    }
}
