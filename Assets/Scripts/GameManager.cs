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

    public State CurrentState { get; private set; }

    public Lumberjack lumberjack;
    public ScrollingBackground background;
    public Music music;
    public TreeSpawner treeSpawner;

    private void Update()
    {
        switch (CurrentState)
        {
            case State.Started:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartPlaying();
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
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Restart();
                }
                break;
        }
    }

    private void StartPlaying()
    {
        lumberjack.StartRunning();
        background.ToggleScrolling(true);
        music.PlayRunning();
        treeSpawner.StartSpawning();
    }

    public void Lose()
    {
        lumberjack.Die();
        background.ToggleScrolling(false);
        music.PlayLose();
        treeSpawner.StopSpawning();
    }

    public void Win()
    {
        lumberjack.Win();
        background.ScrollToWin();
        music.PlayWin();
        treeSpawner.StopSpawning();
    }

    private void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

}
