using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text scoreText, startText;
    public Slider timer;
    public Image timerFillImage;
    public GameObject gameOverScreen;
    public static PlayerUI instance;
    
    void Start()
    {
        instance = this;
    }
    public void UpdateTimer(float rate)
    {
        if(rate < 0.125f || rate > 0.875f) timer.gameObject.SetActive(false);
        else
        {
            if(!timer.gameObject.activeSelf) timer.gameObject.SetActive(true);
            timer.value = rate;
            timerFillImage.color = Color.Lerp(Color.red, Color.green, rate);
        }
        
    }
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void StartTimer(int num)
    {
        if (num == -1) startText.text = "";
        else startText.text = num != 0 ? num.ToString() : "Start";
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
}