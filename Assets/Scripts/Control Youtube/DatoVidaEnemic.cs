using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DatoVidaEnemic : MonoBehaviour
{
    public Enemics VidaEnemic;
    private float Vida;

    public Slider VidaEnemicSlider;

    private void Update()
    {
        VidaEnemic = FindObjectOfType<Enemics>();
        Vida = (VidaEnemic.Vida);
        //Debug.Log(Vida);
    }


}
