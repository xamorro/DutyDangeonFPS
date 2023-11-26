using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Barrera: MonoBehaviour
{
    [SerializeField] private GameObject clauCanvas;
    [SerializeField] private TextMeshProUGUI InfoZona;

    public Animator animator;
    public Transform player;
    public Transform barrera;
    private bool obert = false;

    private void OnTriggerEnter(Collider other)
    {

        float distance = Vector3.Distance(player.position, barrera.position);

        //Si tenc sa clau aixó
        if (distance <= 3 && clauCanvas.activeInHierarchy)
        {
            obert = true;
            animator.SetBool("Obrir", true);
            StartCoroutine(Texte());
            clauCanvas.SetActive(false);


        }
        else if (distance <= 3 && obert == false)
        {
            Debug.Log("Necesitas una llave!");
        }
        
    }
    public IEnumerator Texte()
    {
        InfoZona.enabled = true;
        InfoZona.text = ("Comedor - Termina con todos los enemigos");
        yield return new WaitForSeconds(3);
        InfoZona.enabled = false;
    }
}
