using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources: ")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips: ")]
    public AudioClip background;
    public AudioClip background2;
    public AudioClip die;
    public AudioClip hit;
    public AudioClip respawn;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) {
            musicSource.clip = background;
            musicSource.Play();
        } else if(SceneManager.GetActiveScene().buildIndex == 1) {
            musicSource.clip = background2;
            musicSource.Play();
        }
    }
    
    public void playClip(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
