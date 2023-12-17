using UnityEngine;

public class BarreraZones : MonoBehaviour
{
    private float contadorenemics;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject clauCanvas;

    private enum Zona
    {
        portazona1,
        portazona2,
        portazona3
    }

    [SerializeField] private Zona zonaEnemics;


    //Depenent de la zona que has seleccionat dins l'script de la porta entrar� a un contador o una altre. Si per exemple has triat Z2, obrir� la porta un cop hagi vist que hi hagi 0 enemics de Z2.
    //Conta els enemics mitjan�ant un script que tenen buit cada enemic.
    private void Update()
    {
        switch (zonaEnemics)
        {
            default:
            case Zona.portazona1:
                contadorEnemigoZ1();
                if (contadorenemics == 0)
                {
                    animator.SetBool("Obrir", true);
                    AudioManager.I.PlaySound(SoundName.PortaReja, transform.position);
                    GetComponent<BarreraZones>().enabled = false;
                }
                break;

            case Zona.portazona2:
                contadorEnemigoZ2();
                if (contadorenemics == 0)
                {
                    animator.SetBool("Obrir", true);
                    AudioManager.I.PlaySound(SoundName.PortaReja, transform.position);
                    GetComponent<BarreraZones>().enabled = false;
                }
                break;
        }
    }

    private void contadorEnemigoZ1()
    {
        //Agafa objectes que tenen ficat un script EnemigoZ1 i que t'hes igual s'ordre de retorn.
        EnemigoZ1[] enemics = FindObjectsByType<EnemigoZ1>(FindObjectsSortMode.None);

        //.length me diu quants resultats hi ha
        //Debug.Log(enemics.Length);

        contadorenemics = enemics.Length;
    }

    private void contadorEnemigoZ2()
    {
        EnemigoZ2[] enemics = FindObjectsByType<EnemigoZ2>(FindObjectsSortMode.None);
        //Debug.Log(enemics.Length);
        contadorenemics = enemics.Length;
    }


}
