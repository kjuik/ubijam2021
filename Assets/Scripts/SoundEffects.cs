using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects Instance;

    private void Awake()
    {
        Instance = this;
    }

    public AudioSource[] chopSources;
    public AudioSource dieSource;
    public AudioSource winSource;

    public void PlayChop()
    {
        chopSources[Random.Range(0,chopSources.Length)].Play();
    }

    public void PlayDie()
    {
        dieSource.Play();
    }

    public void PlayWin()
    {
        winSource.Play();
    }
}
