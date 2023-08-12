using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Sprite[] humanSprites;
    public GameObject[] humans;
    Queue<GameObject> humanLine = new Queue<GameObject>(), blackHuman = new Queue<GameObject>(), whiteHuman = new Queue<GameObject>();
    GameObject nowHuman;
    SpriteRenderer sr;
    AudioSource audioSource;
    float leftTime, maxLeftTime;
    string nowName;
    bool isPlaying;
    public int score;
    int nowRan, nowHumanCount = 0;
    const int aimHumanAmount = 100, nearestHumanSize = 10, humanInLine = 6;
    const float widthBetweenHuman = 0.375f;
    void Start()
    {
        instance = this;
        score = 0;
        foreach (var item in humans)
        {
            item.SetActive(false);
        }
        for (int i = 0; i < 16; i++)
        {
            blackHuman.Enqueue(Instantiate(humans[0]));
            whiteHuman.Enqueue(Instantiate(humans[1]));
        }
        
        for (int i = 0; i < humanInLine; i++) SpawnHuman(i);
        PullLine();
        Time.timeScale = 0f;
        maxLeftTime = leftTime = 3f;
        isPlaying = false;
        audioSource = GetComponent<AudioSource>();
    }
    public void StartGame() { PlayerUI.instance.gameStartScreen.SetActive(false); StartCoroutine(StartTimerActive(3)); }
    void SpawnHuman(int pos)
    {
        nowRan = Random.Range(0, 2);
        nowHuman = (nowRan == 0 ? blackHuman : whiteHuman).Dequeue();
        nowHuman.name = nowRan == 0 ? "black" : "white";
        sr = nowHuman.GetComponent<SpriteRenderer>();
        sr.sortingOrder = aimHumanAmount - nowHumanCount + 10;
        sr.sprite = humanSprites[nowRan];
        nowHuman.transform.localScale = Vector3.one * 0.1f * (nearestHumanSize - pos);
        nowHuman.transform.position = Vector3.up * widthBetweenHuman * pos + Vector3.right * Random.Range(-0.5f,0.5f);
        nowHuman.SetActive(true);
        nowHumanCount++;
        humanLine.Enqueue(nowHuman);
    }
    void Update()
    {
        leftTime -= Time.deltaTime;
        if (leftTime < 0) GameOver();
        PlayerUI.instance.UpdateTimer(leftTime / maxLeftTime);
    }
    void GameOver()
    {
        PlayerUI.instance.GameOver();
        isPlaying = false;
    }
    void GameClear()
    {
        //게임 승리시
    }
    public void PullLine()
    {
        foreach (var item in humanLine)
        {
            item.transform.localScale += Vector3.one * 0.1f;
            item.transform.position -= Vector3.up * widthBetweenHuman;
        }
    }
    public void MoveHuman(int dir)
    {
        if (!isPlaying) return;//게임중 아니면 조작 불가 처리
        audioSource.Play();
        nowHuman = humanLine.Dequeue();//큐에서 제거
        nowName = nowHuman.name;
        (nowName == "black" ? blackHuman : whiteHuman).Enqueue(nowHuman);//게임 오브젝트가 돌아갈 큐에 넣기
        if (dir == 1 && nowName == "black" || dir == -1 && nowName == "white") { leftTime = leftTime + 1 > maxLeftTime ? maxLeftTime : leftTime + 1; score++; }//분류 성공
        else { leftTime -= 0.5f; }//분류 실패
        PlayerUI.instance.UpdateScore(score);
        StartCoroutine(Leave(nowHuman, dir));
        if(score >= aimHumanAmount)
        {
            GameClear();
            return;
        }
        SpawnHuman(humanLine.Count);
    }
    IEnumerator StartTimerActive(int leftTime)
    {
        WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(1f);
        while (leftTime-- >= 0)
        {
            yield return waitForSecondsRealtime;
            PlayerUI.instance.StartTimer(leftTime);
        }
        Time.timeScale = 1f;
        isPlaying = true;
    }
    IEnumerator Leave(GameObject h, int dir)
    {
        PullLine();
        h.GetComponent<SpriteRenderer>().sprite = humanSprites[h.name == "black" ? 2 : 3];
        h.transform.localScale = new Vector3(dir, 1, 1) * 0.125f * nearestHumanSize;
        WaitForSeconds delay = new(0.08f);
        for (int i = 0; i < 12; i++)
        {
            h.transform.position = Vector3.right * 0.625f * i * dir + Vector3.up * ( 0.25f * (i % 2) - 1.5f);
            yield return delay;
        }
        h.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Apartheid");
    }
}
