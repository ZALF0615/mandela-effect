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
    public GameObject arrowObj;
    int leftLife, leftCount;
    void Start()
    {
        instance = this;
        leftLife = 3;
        leftCount = FindObjectsByType<BombInsertableObj>(FindObjectsSortMode.None).Length;
        Debug.Log(leftCount);
        Time.timeScale = 0f;
    }
    public void StartGame() { StartCoroutine(StartTimerActive(3)); }
    IEnumerator StartTimerActive(int leftTime)
    {
        WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(1f);
        while (leftTime-- >= 0)
        {
            yield return waitForSecondsRealtime;
            SaboTageUIManager.instance.StartTimer(leftTime);
        }
        Time.timeScale = 1f;
    }
    public void Arrest()
    {
        arrowObj.SetActive(false);
        StartCoroutine(MovePlayerRespawn());
        foreach (var item in insertedObj) item.BombRemoved();
        insertedObj.Clear();
        SaboTageUIManager.instance.arrestedImage.SetActive(true);
        if (leftLife <= 0) { GameOver(); return; }
        
    }
    IEnumerator MovePlayerRespawn()
    {
        yield return new WaitForSeconds(1f);
        PlayerMove.instance.gameObject.transform.position = playerRespawn.position;
        PlayerMove.instance.isArrest = false;
        SaboTageUIManager.instance.arrestedImage.SetActive(false);
        SaboTageUIManager.instance.UpdateLife(--leftLife - 1);
    }
    public void FireAllBomb()
    {
        foreach (var item in insertedObj) item.BreakObj();
        leftCount -= insertedObj.Count;
        if (leftCount == 0) GameClear();
        insertedObj.Clear();
        arrowObj.SetActive(false);
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
