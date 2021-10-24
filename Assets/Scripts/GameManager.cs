using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }

   public enum State
    {
        Started,
        Playing,
        Lost,
        Won
    }

    public State CurrentState { get; private set; } = State.Started;

    public Lumberjack lumberjack;
    public ScrollingBackground[] backgrounds;
    public Music music;
    public TreeSpawner treeSpawner;

    public float walkmanStartDelay = 0.5f;
    public float restartDelay = 2f;

    float gameOverTime;

    private void Update()
    {
        switch (CurrentState)
        {
            case State.Started:
                if (InputDown)
                {
                    StartCoroutine(StartPlaying());
                }
                break;
            case State.Playing:
                if (InputDown)
                {
                    lumberjack.TryChop();
                }
                break;
            case State.Lost:
            case State.Won:
                if (InputDown && (Time.realtimeSinceStartup - gameOverTime) > restartDelay)
                {
                    Restart();
                }
                break;
        }
    }

    private bool InputDown => Application.isMobilePlatform
        ? Input.GetMouseButtonDown(0)
        : Input.GetKeyDown(KeyCode.Space);

    private IEnumerator StartPlaying()
    {
        CurrentState = State.Playing;
        lumberjack.StartRunning();

        SoundEffects.Instance.PlayWalkmanClick();

        yield return new WaitForSeconds(walkmanStartDelay);

        foreach(var b in backgrounds) b.Begin();
        music.PlayRunning();
        treeSpawner.StartSpawning();
    }

    public void Lose()
    {
        if (CurrentState == State.Lost)
        {
            return;
        }

        CurrentState = State.Lost;
        gameOverTime = Time.realtimeSinceStartup;

        lumberjack.Die();
        foreach (var b in backgrounds) b.Lose();
        music.PlayLose();
        treeSpawner.StopSpawning();
    }

    public void Win()
    {
        CurrentState = State.Won;
        gameOverTime = Time.realtimeSinceStartup;

        lumberjack.Win();
        foreach (var b in backgrounds) b.Win();
        music.PlayWin();
        treeSpawner.StopSpawning();
    }

    private void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

}
