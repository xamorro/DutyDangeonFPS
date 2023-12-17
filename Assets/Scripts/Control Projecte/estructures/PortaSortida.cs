using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortaSortida : MonoBehaviour
{
    [SerializeField] private GameObject clauCanvas;
    [SerializeField] private GameObject infoCanvas;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private GameObject finaljoc;
    [SerializeField] private TextMeshProUGUI InfoZona;

    private float contadorenemics;
    private Image panelInfo;
    public Animator animator;
    private bool obert = false;


    private void Start()
    {
        panelInfo = infoCanvas.GetComponentInChildren<Image>();
    }
    
    //Mira si ja has matat a tots els enemics i agafat la clau. Depen del que tenguis complit, et sortirá un missatge o no. Si o tens tot, obrirás la porta
    private void OnTriggerEnter(Collider other)
    {
        contadorEnemigoZ3();
        if (contadorenemics == 0 && clauCanvas.activeInHierarchy)
        {
            obert = true;
            animator.SetTrigger("Obert");
            AudioManager.I.PlaySound(SoundName.PortaMuro, transform.position);
            clauCanvas.SetActive(false);
        }
        else if (obert == false)
        {
            if (contadorenemics > 0 && !clauCanvas.activeInHierarchy)
            {
                StartCoroutine(Texte("Enemies and Key Remaining"));
            }
            else if (!clauCanvas.activeInHierarchy)
            {
                StartCoroutine(Texte("You need a Key"));
            }
            else if (contadorenemics > 0)
            {
                StartCoroutine(Texte("Enemies Remaining"));
            }
        }

    }

    //En sortir del trigger, s'activa un canvas de fi del joc i acaba.
    private void OnTriggerExit(Collider other)
    {
        if (obert == true)
        {
            //StartCoroutine(Texte("Thanks for playing!"));
            finaljoc.SetActive(true);
            Time.timeScale = 0f;
            //isDeath = false;
            //mouseLook.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Menu Final");
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

    //Conta els enemics que tenen un script anomanat EnemigoZ3
    private void contadorEnemigoZ3()
    {
        EnemigoZ3[] enemics = FindObjectsByType<EnemigoZ3>(FindObjectsSortMode.None);
        //Debug.Log(enemics.Length);
        contadorenemics = enemics.Length;
    }
}
