using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    //TEXTE PER PANTALLA
    [SerializeField] private TextMeshProUGUI InfoVida;

    public static event Action<float> VidaModificada;

    public float VidaPlayer = 100f;


    // Start is called before the first frame update
    void Start()
    {
        VidaModificada?.Invoke(VidaPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DañoRecibido(float cantidad)
    {
        VidaPlayer -= cantidad;
        VidaModificada?.Invoke(VidaPlayer);

        if (VidaPlayer <= 0f)
        {
            Debug.Log("Has mort");
        }
    }
}
