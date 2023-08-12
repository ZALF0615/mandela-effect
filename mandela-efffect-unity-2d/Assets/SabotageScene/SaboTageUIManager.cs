using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;

public class SaboTageUIManager : MonoBehaviour
{
    public Slider timer;
    public TMP_Text startText;
    public Image timerFillImage, leftHumanImage;
    public GameObject gameOverScreen, arrestedImage;
    public static SaboTageUIManager instance;

    void Start()
    {
        instance = this;
    }
    public void StartTimer(int num)
    {
        if (num == -1) startText.text = "";
        else startText.text = num != 0 ? num.ToString() : "Start";
    }
    public void UpdateLife(int leftLife)
    {
        leftHumanImage.rectTransform.sizeDelta = new Vector2(86 * leftLife, 220);
    }
    public void UpdateTimer(float rate)
    {
        timer.value = rate;
        timerFillImage.color = Color.Lerp(Color.yellow, Color.green, rate);
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
