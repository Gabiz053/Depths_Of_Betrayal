using UnityEngine;


// Para gestionar el sonido del jugador (Son local todos estos sonidos)
public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip swim;
    public AudioClip crystal;
    public AudioClip menuMusic;
    public AudioClip inGameMusic;

    public static AudioManager instance;


    // Lo hacemos singleton para que solo haya uno
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // que empiece a reproducir el sonido de fondo
    void Start()
    {
        playMusic(inGameMusic);
    }
    public void playMusic(AudioClip musicClip)
    {
        if (musicSource != null && musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // que se reproduzca el sonido
    public void playSFX(AudioClip sfxClip)
    {
        if (sfxClip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }

    // para parar musica
    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}
