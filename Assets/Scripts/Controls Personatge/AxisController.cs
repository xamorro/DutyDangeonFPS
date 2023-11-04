using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisController : MonoBehaviour
{
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Capturem valors cont�nus float entre -1 i 1 per les accions descrites com a 
        // Axis Vertical i Horizontal. Amb aix� donem valor al nostre vector de direcci�
        Vector3 direction = Vector3.zero;

        direction.z = Input.GetAxis("Vertical");
        direction.x = Input.GetAxis("Horizontal");

        Move(direction);
    }

    /// <summary>
    /// Aquest m�todes es crida des de l'Update i reb com a par�metre el vector de direcci�
    /// El podem normalitzar i utilitzar le m�tode Translate de transform per moure en aquella direcci�.
    /// Framerate independent amb Time.deltatime.
    /// </summary>
    /// <param name="direction"></param>
    private void Move(Vector3 direction)
    {
        // no normalitzem per poder acceptar vectors de direcci� amb magnitud menor que 1. 
        // en lloc d'aix� feim un "cap" a 1.
        direction = Vector3.ClampMagnitude(direction, 1);

        transform.Translate(direction * speed * Time.deltaTime);
    }
}