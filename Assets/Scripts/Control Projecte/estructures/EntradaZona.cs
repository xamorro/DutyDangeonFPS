using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class EntradaZona : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI InfoZona;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Texte());
    }

    public IEnumerator Texte()
    {
        InfoZona.enabled = true;
        InfoZona.text = ("Comedor - Termina con todos los enemigos");
        yield return new WaitForSeconds(3);
        InfoZona.enabled = false;
    }
}
