using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValuesController : MonoBehaviour
{
    [Header("Texte Municio")]
    [SerializeField] private TextMeshProUGUI municio;

    private void OnEnable()
    {
        ShootController.MunicioModificada += UpdateAmmo;
    }

    private void OnDisable()
    {

        ShootController.MunicioModificada -= UpdateAmmo;
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

}
