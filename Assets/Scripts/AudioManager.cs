using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    public static AudioManager Instance;

    public AudioClip menu;
    public AudioClip outdoor;
    public AudioClip mine;
    public AudioClip house;
    public AudioClip attack;
    public AudioClip playerDie;
    public AudioClip monsterDie;
    public AudioClip rest;
    public AudioClip win;
    public AudioClip lose;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
    }
}