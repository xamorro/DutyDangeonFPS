using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    
    //Aixo auria de estar a nes sound manager.
    private float soundVolume;
    [SerializeField] TMP_Text textSoundVolume;
    [SerializeField] Slider soundSlider;



    [SerializeField] private GameObject principalMenu;
    [SerializeField] private GameObject optMenu;
    [SerializeField] private GameObject aboutMenu;

    private void Start()
    {
        //crida s'script quan es valor de slider cambia
        soundSlider.onValueChanged.AddListener(delegate { setSoundVolume(); });
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void CloseMenuOptions()
    {
        optMenu.SetActive(false);
        principalMenu.SetActive(true);
    }

    public void CloseMenuAbout()
    {
        aboutMenu.SetActive(false);
        principalMenu.SetActive(true);
    }

    public void OpenMenuOptions()
    {
        optMenu.SetActive(true);
        principalMenu.SetActive(false);
    }

    public void OpenMenuAbout()
    {
        aboutMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }




    public void setSoundVolume()
    {
        //Aqui auriem de agafar es valor des sound manager. Ara es una prova
        soundVolume = soundSlider.value;
        textSoundVolume.text = soundVolume.ToString();

    }

}
