using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    public static SoundController instance;

    [Tooltip("Source for sounds repeating")]
    [SerializeField] private AudioSource gameSource;

    [Tooltip("Source for sounds that are called nonsimultaneous")]
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private Clip backgroundClip;
    [SerializeField] private Clip jumpClip;
    [SerializeField] private Clip buttonClip;
    [SerializeField] private Clip passedLevelClip;
    [SerializeField] private Clip playerDeadClip;
    [SerializeField] private Clip playerShatterClip;
    [SerializeField] private Clip playerInvincibleClip;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        StartBackgroundMusic();
    }

    private void Singleton()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [System.Serializable]
    public class Clip
    {
        public AudioClip clip;
        public bool onLoop = false;
        [Range(0f, 1f)]
        public float volume = .5f;
    }

    private void StartBackgroundMusic()
    {
        gameSource.clip = backgroundClip.clip;
        gameSource.volume = backgroundClip.volume;
        gameSource.loop = backgroundClip.onLoop;
        gameSource.Play();
    }

    public void PlayJumpMusic()
    {
        musicSource.clip = jumpClip.clip;
        musicSource.volume = jumpClip.volume;
        musicSource.loop = jumpClip.onLoop;
        musicSource.Play();
    }

    public void PlayClickButtonMusic()
    {
        musicSource.clip = buttonClip.clip;
        musicSource.volume = buttonClip.volume;
        musicSource.loop = buttonClip.onLoop;
        musicSource.Play();
    }

    public void PlayPassedLevelMusic()
    {
        musicSource.clip = passedLevelClip.clip;
        musicSource.volume = passedLevelClip.volume;
        musicSource.loop = passedLevelClip.onLoop;
        musicSource.Play();
    }

    public void PlayPlayerDeadMusic()
    {
        musicSource.clip = playerDeadClip.clip;
        musicSource.volume = playerDeadClip.volume;
        musicSource.loop = playerDeadClip.onLoop;
        musicSource.Play();
    }

    public void PlayBreakStackMusic()
    {
        musicSource.clip = playerShatterClip.clip;
        musicSource.volume = playerShatterClip.volume;
        musicSource.loop = playerShatterClip.onLoop;
        musicSource.Play();
    }

    public void PlayPlayerInvincibleMusic()
    {
        //Invincible sound is dizzy
        musicSource.clip = playerInvincibleClip.clip;
        musicSource.volume = playerInvincibleClip.volume;
        musicSource.loop = playerInvincibleClip.onLoop;
        musicSource.Play();
    }
}
