using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pausa : MonoBehaviour
{
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject optMenu;
    [SerializeField] Slider soundMusicSlider;
    [SerializeField] Slider soundFX;

    [SerializeField] SoundLibrary soundLibrary;

    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        menuPausa.SetActive(false);
        isPaused = false;
        soundFX.value = soundLibrary.fxVolume;
        soundMusicSlider.value = soundLibrary.musicVolume;
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

    public void CloseMenuOptions()
    {
        optMenu.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(menuFirstButton);
        menuPausa.SetActive(true);
    }


    public void OpenMenuOptions()
    {
        optMenu.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        menuPausa.SetActive(false);
    }

    public void ChangeSoundFX()
    {
        soundLibrary.fxVolume = soundFX.value;
    }

    public void ChangeMusicVolume()
    {
        soundLibrary.musicVolume = soundMusicSlider.value;
        AudioSource asa = GameObject.FindGameObjectWithTag("Musica").GetComponent<AudioSource>();
        asa.volume = soundMusicSlider.value / 100;

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
