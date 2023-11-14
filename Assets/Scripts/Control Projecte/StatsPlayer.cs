using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    //TEXTE PER PANTALLA
    [SerializeField] private TextMeshProUGUI InfoVida;

    public float VidaPlayer = 100f;


    // Start is called before the first frame update
    void Start()
    {
        InfoVida.text = (VidaPlayer.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
