using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip startClip;
    public AudioClip runningClip;
    public AudioClip lossClip;
    public AudioClip winClip;

    private void Start()
    {
        if (startClip)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(startClip);
        }
    }

    internal void PlayRunning()
    {
        audioSource.loop = false;
        audioSource.PlayOneShot(runningClip);
        StartCoroutine(WinWhenMusicEnds());
    }

    private IEnumerator WinWhenMusicEnds()
    {
        yield return new WaitForSeconds(runningClip.length);
        if (GameManager.Instance.CurrentState == GameManager.State.Playing)
        {
            GameManager.Instance.Win();
        }
    }

    internal void PlayLose()
    {
        audioSource.Stop();
        if (lossClip)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(lossClip);
        }
    }

    internal void PlayWin()
    {
        audioSource.Stop();
        if (winClip)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(winClip);
        }
    }
}
