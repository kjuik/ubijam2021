using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BeatRecorder : MonoBehaviour
{
    public Text startInstruction;
    public Text playingInstruction;
    public Text endInstruction;

    public Text timerLabel;
    public Text beatsLabel;
    public Text saveFileLabel;

    public AudioSource audioSource;

    BeatData currentRecording = new BeatData();

    bool isPlaying;
    float startTime;

    private void Start()
    {
        saveFileLabel.gameObject.SetActive(false);
    }

    void Update()
    {
        startInstruction.gameObject.SetActive(!isPlaying && currentRecording.beats.Count == 0);
        playingInstruction.gameObject.SetActive(isPlaying);
        endInstruction.gameObject.SetActive(!isPlaying && currentRecording.beats.Count > 0);

        timerLabel.text = isPlaying
            ? TimeSpan.FromSeconds(Time.realtimeSinceStartup - startTime).ToString("mm\\:ss")
            : "";

        beatsLabel.text = currentRecording.beats.Count > 0 || isPlaying
            ? "beats: " + currentRecording.beats.Count
            : "";

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPlaying)
            {
                audioSource.Play();
                startTime = Time.realtimeSinceStartup;
                isPlaying = true;
            }
            else
            {
                currentRecording.beats.Add(Time.realtimeSinceStartup - startTime);
                SoundEffects.Instance.PlayChop();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && isPlaying)
        {
            SaveRecording();

            isPlaying = false;
            audioSource.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPlaying = false;
            audioSource.Stop();

            startTime = 0f;
            currentRecording = new BeatData();
            saveFileLabel.gameObject.SetActive(false);
        }

    }

    private void SaveRecording()
    {
        string jsonString = JsonUtility.ToJson(currentRecording);
        var saveFilePath = Application.persistentDataPath + 
            "/recording_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second 
            +".json";
        File.WriteAllText(saveFilePath, jsonString);

        saveFileLabel.gameObject.SetActive(true);
        saveFileLabel.text = ("Saved to " + saveFilePath);
    }
}
