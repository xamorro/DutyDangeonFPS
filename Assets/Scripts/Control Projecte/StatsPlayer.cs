using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    //TEXTE PER PANTALLA
    [SerializeField] private TextMeshProUGUI InfoVida;
    
    private Mort mort;

    public static event Action<float> VidaModificada;

    public float VidaPlayer = 100f;
    public bool isDeath;



    // Start is called before the first frame update
    void Start()
    {
        VidaModificada?.Invoke(VidaPlayer);
        isDeath = false;
        mort = GameObject.Find("CanvasJOC").GetComponent<Mort>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -80)
        {
            DeathMenu();
        }
    }

    public void DañoRecibido(float cantidad)
    {
        VidaPlayer -= cantidad;
        VidaModificada?.Invoke(VidaPlayer);

        if (VidaPlayer <= 0f)
        {
            DeathMenu();
        }
    }


    private void DeathMenu()
    {
        VidaModificada?.Invoke(0);
        mort?.MenuDeath();
        Debug.Log("Has mort");
    }
}
