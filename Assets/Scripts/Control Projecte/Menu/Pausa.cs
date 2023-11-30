using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private GameObject menuPausa;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        menuPausa.SetActive(false);
        isPaused = false;
}

    // Update is called once per frame
    void Update()
    {

    }


    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
        menuPausa.SetActive(true);
        mouseLook.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Pause");

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        menuPausa.SetActive(false);
        mouseLook.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log("Resume");
    }

    public void MenuPausa()
    {
        if (isPaused)
        {
            ResumeGame(); 
        }
        else
        {
            PauseGame();
        }
    }

    //public void CloseMenuOptions()
    //{
    //    optMenu.SetActive(false);
    //    EventSystem.current.SetSelectedGameObject(menuFirstButton);
    //    principalMenu.SetActive(true);
    //}


    //public void OpenMenuOptions()
    //{
    //    optMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(optionFirstButton);
    //    principalMenu.SetActive(false);
    //}


    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }
}
