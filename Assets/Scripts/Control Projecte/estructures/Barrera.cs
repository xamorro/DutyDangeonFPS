using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class Barrera: MonoBehaviour
{
    [SerializeField] private GameObject clauCanvas;
    [SerializeField] private TextMeshProUGUI InfoZona;
    [SerializeField] private GameObject infoCanvas;

    private Image panelInfo;
    public Animator animator;
    public Transform player;
    public Transform barrera;
    private bool obert = false;

    private void Start()
    {
        panelInfo = infoCanvas.GetComponentInChildren<Image>();
    }

    private void OnTriggerEnter(Collider other)
    {

        float distance = Vector3.Distance(player.position, barrera.position);

        //Si tenc sa clau aixó
        if (distance <= 3 && clauCanvas.activeInHierarchy)
        {
            obert = true;
            animator.SetBool("Obrir", true);
            AudioManager.I.PlaySound(SoundName.PortaReja, transform.position);
            StartCoroutine(Texte("Kills all enemies"));
            clauCanvas.SetActive(false);


        }
        else if (distance <= 3 && obert == false)
        {
            StartCoroutine(Texte("You need a Key"));
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
