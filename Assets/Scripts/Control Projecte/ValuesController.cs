using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private float sliderDuration = 0.5f;
    

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
        
        if (ammo < 10)
        {
            municio.text = "0"+ammo.ToString();
            municio.color = Color.red;
        }
        else
        {
            municio.text = ammo.ToString();
            municio.color = Color.white;
        }
    }

    private void MaxUpdateAmmo(int ammo)
    {
        maxMunicio.text = ammo.ToString();
        if (ammo < 10)
        {
            maxMunicio.text = "0" + ammo.ToString();
            if (ammo == 0)
            {
                maxMunicio.color = Color.red;
            }
        }
        else
        {
            maxMunicio.text = ammo.ToString();
            maxMunicio.color = Color.white;
        }
    }

    private void UpdateVida(float vida)
    {
        vidaJugador.text = vida.ToString();
        StartCoroutine(MoveHealthBar(vida));

        percentatgeVida = vida / 100;

        if (percentatgeVida < 0.5f)
        {
            colorVida.color = Color.yellow;
            if (percentatgeVida < 0.2f)
            {
                colorVida.color = Color.red;
            }
        }
    }

    private IEnumerator MoveHealthBar(float vida)
    {
        float elapsedTime = 0;
        float healthStart = sliderVida.value;
        while (elapsedTime <= sliderDuration)
        {

            sliderVida.value = Mathf.Lerp(healthStart, vida / 100, elapsedTime / sliderDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
