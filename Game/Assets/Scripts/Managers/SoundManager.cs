
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    None,
    BuyGun,
    BuyAmmo,
    C4Plant,
    C4Activate,
    C4Explosion,
    PistolShot,
    ShotgunShot
}

public class SoundManager : MonoBehaviour
{

    public static SoundManager main;

    [SerializeField]
    private List<GameSound> sounds = new List<GameSound>();

    private bool sfxMuted = false;

    [SerializeField]
    private bool musicMuted = false;
    public bool MusicMuted { get { return musicMuted; } }

    [SerializeField]
    private AudioSource menuMusicSource;

    [SerializeField]
    private AudioSource gameMusicSource;
    private float originalMenuVolume;
    private float originalGameVolume;

    void Awake()
    {
        main = this;
    }

    private void Start()
    {
        originalMenuVolume = menuMusicSource.volume;
        originalGameVolume = gameMusicSource.volume;
        if (menuMusicSource != null)
        {
            if (musicMuted)
            {
                menuMusicSource.Pause();
                //UIManager.main.ToggleMusic();
            }
            else
            {
                menuMusicSource.Play();
            }
        }
    }

    public void PlaySound(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    AudioSource soundToBePlayed = gameSound.sound;
                    if (gameSound.sounds.Count > 0)
                    {
                        soundToBePlayed = gameSound.sounds[Random.Range(0, gameSound.sounds.Count)];
                    }
                    soundToBePlayed.Play();
                }
            }
        }
    }

    public void ToggleSfx()
    {
        sfxMuted = !sfxMuted;
        //UIManager.main.ToggleSfx();
    }

    /*public bool ToggleMusic()
    {
        musicMuted = !musicMuted;
        if (musicMuted)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }
        //UIManager.main.ToggleMusic();
        return musicMuted;
    }*/

    private bool fadingOut = false;
    private bool fadingIn = false;
    private AudioSource sourceBeingFaded;
    private float fadeInDuration = 1f;
    private float fadeOutDuration = 2f;
    private float originalVolume;
    private float targetVolume;

    private float lerpT = 0f;

    void FadeOutMenuMusic()
    {
        lerpT = 0f;
        fadingOut = true;
        originalVolume = menuMusicSource.volume;
        sourceBeingFaded = menuMusicSource;
        targetVolume = 0f;
    }

    void FadeInMenuMusic()
    {
        lerpT = 0f;
        fadingIn = true;
        originalVolume = 0f;
        sourceBeingFaded = menuMusicSource;
        sourceBeingFaded.Play();
        targetVolume = originalMenuVolume;
    }

    void FadeOutGameMusic()
    {
        lerpT = 0f;
        fadingOut = true;
        originalVolume = gameMusicSource.volume;
        sourceBeingFaded = gameMusicSource;
        targetVolume = 0f;
    }
    void FadeInGameMusic()
    {
        lerpT = 0f;
        fadingIn = true;
        originalVolume = 0f;
        sourceBeingFaded = gameMusicSource;
        sourceBeingFaded.Play();
        targetVolume = originalGameVolume;
    }

    private bool menuToGame = false;
    private bool gameToMenu = false;
    public void FadeMenuToGame()
    {
        menuToGame = true;
        FadeOutMenuMusic();
    }

    public void FadeGameToMenu()
    {
        gameToMenu = true;
        FadeOutGameMusic();
    }


    void Update()
    {
        if (fadingOut)
        {
            lerpT +=  Time.deltaTime / fadeOutDuration;
            sourceBeingFaded.volume = Mathf.Lerp(originalVolume, targetVolume, lerpT);

            if (Mathf.Abs(sourceBeingFaded.volume - targetVolume) < 0.05f)
            {
                sourceBeingFaded.volume = targetVolume;
                fadingOut = false;
                sourceBeingFaded.Pause();
                if (menuToGame)
                {
                    menuToGame = false;
                    FadeInGameMusic();
                }
                else if (gameToMenu)
                {
                    FadeInMenuMusic();
                }
            }
        }
        else if (fadingIn)
        {
            lerpT +=  Time.deltaTime / fadeInDuration;
            sourceBeingFaded.volume = Mathf.Lerp(originalVolume, targetVolume, lerpT);
            if (Mathf.Abs(sourceBeingFaded.volume - targetVolume) < 0.05f)
            {
                sourceBeingFaded.volume = targetVolume;
                fadingIn = false;
            }
        }
    }
}

[System.Serializable]
public class GameSound : System.Object
{
    public SoundType soundType;
    public AudioSource sound;
    public List<AudioSource> sounds;
}
