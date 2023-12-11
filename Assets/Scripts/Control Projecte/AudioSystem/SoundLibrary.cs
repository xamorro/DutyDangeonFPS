using UnityEngine;

[CreateAssetMenu(fileName = "SoundLibrary", menuName = "SCO/SoundLibrary")]
public class SoundLibrary : ScriptableObject
{
    public SoundClip[] soundClips;
    public float fxVolume = 1f;
    public float musicVolume = 1f;

    private void OnValidate()
    {
        foreach (SoundClip soundClip in soundClips)
        {
            if (soundClip.hasPlayTimer && soundClip.playTimer == 0)
            {
                soundClip.playTimer = soundClip.audioClip.length;
            }
        }
    }
}

/// <summary>
/// enum all the clips that you will need to enumerate in your game
/// </summary>
public enum SoundName
{
    Level1Music,
    AkShot,
    KeyFind,
    ReloadAK,
    Pasos,
    InvaderCrash,
    UltimateCharged,
    UltimateRelease,
    MothershipExplosion,
    MothershipCollapsing,
    PlayerDamaged,
    MainMenuMusic
}

[System.Serializable]
public class SoundClip
{
    public SoundName soundName;
    public AudioClip audioClip;
    public bool loop;
    [Range(0f, 1f)]
    public float volume;
    public bool hasPlayTimer;
    public float playTimer;
    [Range(0f, 1f)]
    public float spacialBlend;
}