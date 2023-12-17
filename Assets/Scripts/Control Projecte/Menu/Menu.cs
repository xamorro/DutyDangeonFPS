using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    
    //Aixo auria de estar a nes sound manager.
    private float soundVolume;
    [SerializeField] TMP_Text textSoundVolume;
    [SerializeField] Slider soundMusicSlider;
    [SerializeField] Slider soundFX;
    [SerializeField] SoundLibrary soundLibrary;

    //private AudioSource musicasound;


    [SerializeField] private GameObject principalMenu;
    [SerializeField] private GameObject optMenu;
    [SerializeField] private GameObject controlMenu;
    [SerializeField] private GameObject aboutMenu;

    public GameObject menuFirstButton, optionsFirstButton, controlsFirstButton ,aboutFirstButton;

    private void Start()
    {
        //crida s'script quan es valor de slider cambia
        //soundMusicSlider.onValueChanged.AddListener(delegate { setSoundVolume(); });
        soundFX.value = soundLibrary.fxVolume;
        soundMusicSlider.value = soundLibrary.musicVolume;
        //musicasound = GameObject.FindGameObjectWithTag("Musica").GetComponent<AudioSource>();
    }

    //Obrir Scene Joc
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    //Tancar Menu Options
    public void CloseMenuOptions()
    {
        optMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
        principalMenu.SetActive(true);
    }

    //Tancar Menu About
    public void CloseMenuAbout()
    {
        aboutMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
        principalMenu.SetActive(true);
    }

    //Tancar Menu Control
    public void CloseMenuControl()
    {
        controlMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
        principalMenu.SetActive(true);
    }

    //Obrir Menu Options
    public void OpenMenuOptions()
    {
        optMenu.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        principalMenu.SetActive(false);
    }

    //Obrir Menu About
    public void OpenMenuAbout()
    {
        aboutMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(aboutFirstButton);
        principalMenu.SetActive(false);
    }

    //Obrir Menu Control
    public void OpenMenuControl()
    {
        controlMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(controlsFirstButton);
        principalMenu.SetActive(false);
    }

    //Sortir Joc
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

    //Canvia Sonido FX
    public void ChangeSoundFX()
    {
        soundLibrary.fxVolume = soundFX.value;
    }

    //Canvia Musica sonido en temps real
    public void ChangeMusicVolume()
    {
        soundLibrary.musicVolume = soundMusicSlider.value;
        //musicasound.volume = soundMusicSlider.value;
        AudioSource asa = GameObject.FindGameObjectWithTag("Musica").GetComponent<AudioSource>();
        asa.volume = soundMusicSlider.value / 100;
    }

}
