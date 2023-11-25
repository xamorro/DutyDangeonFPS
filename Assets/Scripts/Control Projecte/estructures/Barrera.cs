using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Barrera: MonoBehaviour
{
    [SerializeField] private GameObject clauCanvas;

    public Animator animator;
    public Transform player;
    public Transform barrera;
    private bool obert = false;

    private void Update()
    {
        float distance = Vector3.Distance(player.position, barrera.position);

        //Si tenc sa clau aixó
        if (distance <= 3 && clauCanvas.activeInHierarchy)
        {
            obert = true;
            animator.SetBool("Obrir", true);
            clauCanvas.SetActive(false);
        }
        else if (distance <= 3 && obert == false)
        {
            Debug.Log("Necesitas una llave!");
        }
    }

}
