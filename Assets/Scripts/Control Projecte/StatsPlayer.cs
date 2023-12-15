using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsPlayer : MonoBehaviour
{
    //TEXTE PER PANTALLA
    [SerializeField] private TextMeshProUGUI InfoVida;
    [SerializeField] private Image canvasDa�o;

    private float alphaValue = 0.0f;

    
    private Mort mort;
    private Ferit ferit;
    private CameraShake cam;

    public static event Action<float> VidaModificada;

    public float VidaPlayer = 100f;
    public bool isDeath;



    // Start is called before the first frame update
    void Start()
    {
        VidaModificada?.Invoke(VidaPlayer);
        isDeath = false;
        mort = GameObject.Find("CanvasJOC").GetComponent<Mort>();
        ferit = GameObject.Find("CanvasJOC").GetComponent<Ferit>();
        cam = GameObject.Find("Camera").GetComponent<CameraShake>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -80)
        {
            DeathMenu();
        }
    }

    public void Da�oRecibido(float cantidad)
    {
        VidaPlayer -= cantidad;
        VidaModificada?.Invoke(VidaPlayer);
        
        ferit.CanvasFerit();
        cam.ShakeCamera();

        ChangeAlpha();

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

    private void ChangeAlpha()
    {
        //alphaValue = alphaValue + 0.08f;
        alphaValue = alphaValue + 0.01f;
        //canvasDa�o.color = new Color(canvasDa�o.color.r, canvasDa�o.color.r, canvasDa�o.color.r, alphaValue);
        canvasDa�o.color = new Color(255, 0, 0, alphaValue);
    }
}
