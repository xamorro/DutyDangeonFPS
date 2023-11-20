using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject optMenu;
    [SerializeField] private GameObject aboutMenu;


    public void StartGame()
    {
        //SceneManager.LoadScene(4);
        Debug.Log("Cargando Joc");
    }


    void CloseMenuOptions()
    {
        optMenu.SetActive(false);
    }

    void CloseMenuAbout()
    {
        aboutMenu.SetActive(false);
    }

    void OpenMenuOptions()
    {
        optMenu.SetActive(true);
    }

    void OpenMenuAbout()
    {
        aboutMenu.SetActive(false);
    }

    void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }



}
