using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortaSortida : MonoBehaviour
{
    [SerializeField] private GameObject clauCanvas;
    [SerializeField] private GameObject infoCanvas;
    [SerializeField] private TextMeshProUGUI InfoZona;
    
    private Image panelInfo;
    public Animator animator;
    private bool obert = false;

    private void Start()
    {
        panelInfo = infoCanvas.GetComponentInChildren<Image>();
    }
    

    private void OnTriggerEnter(Collider other)
    {

        //Si tenc sa clau aixó
        if (clauCanvas.activeInHierarchy)
        {
            obert = true;
            animator.SetTrigger("Obert");
            clauCanvas.SetActive(false);


        }
        else if (obert == false)
        {
            StartCoroutine(Texte("You need a Key"));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (obert == true)
        {
            StartCoroutine(Texte("Thanks for playing!"));
        }
        
    }


    public IEnumerator Texte(string texte)
    {
        InfoZona.enabled = true;
        panelInfo.enabled = true;
        InfoZona.text = (texte);

        yield return new WaitForSeconds(2);

        InfoZona.enabled = false;
        panelInfo.enabled = false;
    }
}
