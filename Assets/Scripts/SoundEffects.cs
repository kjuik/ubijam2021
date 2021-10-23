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
    public AudioSource walkmanSource;
    public AudioSource HitHeadSource;

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

    internal void PlayWalkmanClick()
    {
        walkmanSource.Play();
    }

    internal void PlayHitHead()
    {
        HitHeadSource.Play();
    }
}
