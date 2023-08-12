using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaboGameManager : MonoBehaviour
{
    public static SaboGameManager instance;
    public Transform playerRespawn;
    public List<BombInsertableObj> insertedObj = new List<BombInsertableObj>();
    public Sprite[] sprite;
    int leftLife, leftCount;
    void Start()
    {
        instance = this;
        leftLife = 3;
        leftCount = FindObjectsByType<BombInsertableObj>(FindObjectsSortMode.None).Length;
        Debug.Log(leftCount);
        Time.timeScale = 1f;
    }
    public void Arrest()
    {
        foreach (var item in insertedObj) item.BombRemoved();
        insertedObj.Clear();
        SaboTageUIManager.instance.UpdateLife(--leftLife - 1);
        if (leftLife <= 0) { GameOver(); return; }
        PlayerMove.instance.gameObject.transform.position = playerRespawn.position;
    }
    public void FireAllBomb()
    {
        foreach (var item in insertedObj) item.BreakObj();
        leftCount -= insertedObj.Count;
        if (leftCount == 0) GameClear();
        insertedObj.Clear();
    }
    void GameClear()
    {
        Debug.Log("GameClear");
    }
    void GameOver()
    {
        SaboTageUIManager.instance.GameOver();
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Sabotage");
    }
}
