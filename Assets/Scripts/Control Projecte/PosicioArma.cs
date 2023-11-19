using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicioArma : MonoBehaviour
{
    void Update()
    {
        transform.localPosition = new Vector3 (0.24f, 0.28f, 0f);
        transform.localEulerAngles = new Vector3(-70.605f, 105.547f, -17.603f);
    }
}
