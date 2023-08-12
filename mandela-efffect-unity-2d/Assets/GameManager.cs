using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region INSTANCE
    public static GameManager instance;
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            return null;
        }

        return instance;
    }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion INSTANCE

    public static GameScene currentScene;
    public static int currentGameIdx;
    public static int currentdialogIdx;

    // 미니게임 인덱스 관련

    public int[] game_start_idx = new int[5];

    public static void LoadScene(GameScene scene)
    {
        string sceneName = string.Empty;

        switch (scene)
        {
            case GameScene.Title:
                sceneName = "TitleScene";
                break;
            case GameScene.StoryScene:
                sceneName = "StoryScene";
                break;
            case GameScene.Game1_Apartheid:
                sceneName = "Game1_Apartheid";
                break;
            case GameScene.Game2_Sabotage:
                sceneName = "Game2_Sabotage";
                break;
            case GameScene.Game3_FindMandela:
                sceneName = "Game3_FindMandela";
                break;
            case GameScene.Game4_Liberation_day:
                sceneName = "Game4_Liberation_day";
                break;
        }

        try
        {
            SceneManager.LoadScene(sceneName);
        }
        catch
        {
            Debug.LogError("정의되지 않은 게임 신입니다.");
        }

    }
    public void ReturntoStory(int gameIdx)
    {
        currentdialogIdx = game_start_idx[gameIdx];

        LoadScene(GameScene.StoryScene);
    }
}

public enum GameScene
{
    Title,
    StoryScene,
    Game1_Apartheid,
    Game2_Sabotage,
    Game3_FindMandela,
    Game4_Liberation_day,
}