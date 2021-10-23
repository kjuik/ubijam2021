using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource ambienceSource;

    private void Start()
    {
        if (ambienceSource)
        {
            ambienceSource.Play();
        }
    }

    internal void PlayRunning()
    {
        musicSource.Play();
        StartCoroutine(WinWhenMusicEnds());
    }

    private IEnumerator WinWhenMusicEnds()
    {
        yield return new WaitForSeconds(musicSource.clip.length);
        if (GameManager.Instance.CurrentState == GameManager.State.Playing)
        {
            GameManager.Instance.Win();
        }
    }

    internal void PlayLose()
    {
        musicSource.Stop();
    }

    internal void PlayWin()
    {
        musicSource.Stop();
    }
}
