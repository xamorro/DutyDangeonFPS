using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I { get; private set; }

    [SerializeField] private bool playSceneMusic;
    [SerializeField] private SoundName sceneMusicClip;

    [SerializeField] SoundLibrary soundLibrary;


    private Dictionary<SoundName, float> soundTimers;
    private AudioSource audioSource2d;
    private AudioSource sceneMusicAudioSource;

    private void Awake()
    {
        if (I is null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (I != this)
        {
            Destroy(gameObject);
        }

        InitializeSoundTimers();
    }

    private void Start()
    {
        if (playSceneMusic)
            PlaySceneMusic(sceneMusicClip);
    }

    private void InitializeSoundTimers()
    {
        soundTimers = new Dictionary<SoundName, float>();
        foreach (SoundClip soundClip in soundLibrary.soundClips)
        {
            if (soundClip.hasPlayTimer)
                soundTimers[soundClip.soundName] = soundClip.playTimer;
        }
    }

    public void PlaySound(SoundName soundName)
    {
        SoundClip soundClip = GetAudioClip(soundName);
        if (soundClip == null || !CanPlaySound(soundClip))
            return;

        if (audioSource2d is null)
        {
            GameObject soundGameObject = new("2D Sound");
            soundGameObject.transform.SetParent(transform);
            audioSource2d = soundGameObject.AddComponent<AudioSource>();
        }

        audioSource2d.loop = soundClip.loop;
        audioSource2d.volume = soundClip.volume;


        audioSource2d.PlayOneShot(soundClip.audioClip);
    }

    public void PlaySound(SoundName soundName, Vector3 position)
    {
        SoundClip soundClip = GetAudioClip(soundName);
        if (soundClip == null || !CanPlaySound(soundClip))
            return;

        GameObject soundGameObject = new("3D Sound");
        soundGameObject.transform.position = position;

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.loop = soundClip.loop;
        audioSource.volume = soundClip.volume;
        audioSource.spatialBlend = soundClip.spacialBlend;
        audioSource.PlayOneShot(soundClip.audioClip);

        Destroy(soundGameObject, soundClip.audioClip.length);
    }

    public void PlaySceneMusic(SoundName soundName)
    {
        SoundClip soundClip = GetAudioClip(soundName);
        if (soundClip == null || !CanPlaySound(soundClip))
            return;

        if (sceneMusicAudioSource is null)
        {
            sceneMusicAudioSource = gameObject.AddComponent<AudioSource>();
        }

        sceneMusicAudioSource.loop = true;
        sceneMusicAudioSource.volume = soundClip.volume;
        sceneMusicAudioSource.clip = soundClip.audioClip;

        sceneMusicAudioSource.Play();
    }

    public void StopSceneMusic()
    {
        if (sceneMusicAudioSource is null)
            return;

        sceneMusicAudioSource.Stop();
    }

    private bool CanPlaySound(SoundClip soundClip)
    {
        if (!soundClip.hasPlayTimer || !soundTimers.ContainsKey(soundClip.soundName))
            return true;

        float lastTimePlayed = soundTimers[soundClip.soundName];
        if (Time.time > lastTimePlayed + soundClip.playTimer)
        {
            soundTimers[soundClip.soundName] = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

    private SoundClip GetAudioClip(SoundName soundName)
    {
        foreach (SoundClip soundClip in soundLibrary.soundClips)
        {
            if (soundClip.soundName.Equals(soundName))
            {
                return soundClip;
            }
        }
        return null;
    }
}