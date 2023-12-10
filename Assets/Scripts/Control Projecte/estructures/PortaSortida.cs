using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortaSortida : MonoBehaviour
{
    [SerializeField] private GameObject clauCanvas;
    [SerializeField] private TextMeshProUGUI InfoZona;

    public Animator animator;
    private bool obert = false;

    private void OnTriggerEnter(Collider other)
    {

        //Si tenc sa clau aixó
        if (clauCanvas.activeInHierarchy)
        {
            obert = true;
            animator.SetTrigger("Obert");
            StartCoroutine(Texte());
            clauCanvas.SetActive(false);


        }
        else if (obert == false)
        {
            Debug.Log("Necesitas una llave!");
        }

    }
    public IEnumerator Texte()
    {
        InfoZona.enabled = true;
        InfoZona.text = ("FIN - ESCAPASTES");
        yield return new WaitForSeconds(3);
        InfoZona.enabled = false;
    }
}
