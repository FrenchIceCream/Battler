using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip missSound;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            audioSource.playOnAwake = false;
            audioSource.clip = backgroundMusic;
            audioSource.Play();
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayHitSoundOneShot()
    {
        audioSource.PlayOneShot(hitSound, Random.Range(1f, 2f));
    }

    public void PlayMissSoundOneShot()
    {
        audioSource.PlayOneShot(missSound, Random.Range(1f, 2f));
    }
}
