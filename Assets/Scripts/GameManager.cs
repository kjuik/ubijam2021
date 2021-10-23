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

    private void Update()
    {
        switch (CurrentState)
        {
            case State.Started:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(StartPlaying());
                }
                break;
            case State.Playing:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    lumberjack.TryChop();
                }
                break;
            case State.Lost:
            case State.Won:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Restart();
                }
                break;
        }
    }

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

        lumberjack.Die();
        foreach (var b in backgrounds) b.Lose();
        music.PlayLose();
        treeSpawner.StopSpawning();
    }

    public void Win()
    {
        CurrentState = State.Won;

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
