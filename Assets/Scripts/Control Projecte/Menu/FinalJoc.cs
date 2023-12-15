using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalJoc : MonoBehaviour
{
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private GameObject menuFinal;
    public bool isDeath;

    // Start is called before the first frame update
    void Start()
    {
        menuFinal.SetActive(false);
        //isDeath = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MenuFinal()
    {
        Time.timeScale = 0f;
        //isDeath = false;
        menuFinal.SetActive(true);
        mouseLook.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Menu Final");
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
