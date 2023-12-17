using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Barrera: MonoBehaviour
{
    [SerializeField] private GameObject clauCanvas;
    [SerializeField] private TextMeshProUGUI InfoZona;
    [SerializeField] private GameObject infoCanvas;
    [SerializeField] private GameObject llumsZ2;

    private Image panelInfo;
    public Animator animator;
    public Transform player;
    public Transform barrera;
    private bool obert = false;

    private void Start()
    {
        panelInfo = infoCanvas.GetComponentInChildren<Image>();
    }

    //Comprova si hi ha les claus actives a nes canvas. Aixó vol dir q el jugador te les claus. Si els té obri la porta y activa els llums de la z2 i un canvas de matar enemics.
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
            llumsZ2.SetActive(true);
            clauCanvas.SetActive(false);


        }//Si no tens clau et dona un missatge
        else if (distance <= 3 && obert == false)
        {
            StartCoroutine(Texte("You need a Key"));
        }
        
    }

    //Corrutine de texte per pantalla.
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
