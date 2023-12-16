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

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }


    public void CloseMenuOptions()
    {
        optMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
        principalMenu.SetActive(true);
    }

    public void CloseMenuAbout()
    {
        aboutMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
        principalMenu.SetActive(true);
    }

    public void CloseMenuControl()
    {
        controlMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
        principalMenu.SetActive(true);
    }

    public void OpenMenuOptions()
    {
        optMenu.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        principalMenu.SetActive(false);
    }

    public void OpenMenuAbout()
    {
        aboutMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(aboutFirstButton);
        principalMenu.SetActive(false);
    }
    public void OpenMenuControl()
    {
        controlMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(controlsFirstButton);
        principalMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

    public void ChangeSoundFX()
    {
        soundLibrary.fxVolume = soundFX.value;
    }

    public void ChangeMusicVolume()
    {
        soundLibrary.musicVolume = soundMusicSlider.value;
        //musicasound.volume = soundMusicSlider.value;
        AudioSource asa = GameObject.FindGameObjectWithTag("Musica").GetComponent<AudioSource>();
        asa.volume = soundMusicSlider.value / 100;
    }

    //public void setSoundVolume()
    //{
    //    //Aqui auriem de agafar es valor des sound manager. Ara es una prova
    //    soundVolume = soundMusicSlider.value;
    //    textSoundVolume.text = soundVolume.ToString();
    //}


}
