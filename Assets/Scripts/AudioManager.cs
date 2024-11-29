using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    [Header("Audio Clips")]
    public AudioClip swim;
    public AudioClip crystal;
    public AudioClip menuMusic;
    public AudioClip inGameMusic;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        playMusic(inGameMusic);
    }
    public void playMusic (AudioClip musicClip)
    {
        if(musicSource != null && musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    
    public void playSFX (AudioClip sfxClip)
    {
        if(sfxClip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}
