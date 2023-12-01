using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mort : MonoBehaviour
{
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private GameObject menuMort;
    public bool isDeath;

    // Start is called before the first frame update
    void Start()
    {
        menuMort.SetActive(false);
        //isDeath = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MenuDeath()
    {
        Time.timeScale = 0f;
        //isDeath = false;
        menuMort.SetActive(true);
        mouseLook.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Menu Mort");
    }

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

