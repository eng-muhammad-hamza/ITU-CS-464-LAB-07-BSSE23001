using System.Collections;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    public bool Running { get; private set; }
    public bool Paused { get; private set; }
    public int Shards { get; private set; }
    public int Target { get; private set; }
    public float Elapsed { get; private set; }
    public bool LastWin { get; private set; }
    public int LastStars { get; private set; }
    public int LastGems { get; private set; }
    public bool LastNewBest { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void BuildLevel(int levelIndex)
    {
        Cleanup();
        LevelDef level = Catalog.Levels[levelIndex];
        Shards = level.shardTarget;
        Target = level.shardTarget;
        Elapsed = 0f;

        Camera cam = Camera.main;
        if (cam != null)
        {
            cam.orthographic = true;
            cam.orthographicSize = 8f;
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = UiTheme.BgDeep;
            cam.transform.position = new Vector3(0f, 0f, -10f);
        }
    }

    public void StartCountdown()
    {
        Running = true;
    }

    void Update()
    {
        if (!Running || Paused) return;
        Elapsed += Time.deltaTime;
    }

    public void SetPaused(bool paused)
    {
        Paused = paused;
        Time.timeScale = paused ? 0f : 1f;
    }

    public void EndLevel(bool win)
    {
        if (!Running) return;
        Running = false;
        LastWin = win;
        LastStars = win ? 3 : 0;
        LastGems = win ? Target * 3 + LastStars * 10 : 5;
        LastNewBest = win && LastStars > GameData.GetStars(GameData.SelectedLevel);
        if (win) GameData.SetStarsIfBetter(GameData.SelectedLevel, LastStars);
        GameData.AddGems(LastGems);
        PlayerPrefs.Save();

        UIManager.Instance.ShowResults();
    }

    public void Cleanup()
    {
        StopAllCoroutines();
        Running = false;
        Paused = false;
        Time.timeScale = 1f;
    }
}
