using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SaboTageUIManager : MonoBehaviour
{
    public Slider timer;
    public Image timerFillImage, leftHumanImage;
    public GameObject gameOverScreen;
    public static SaboTageUIManager instance;

    void Start()
    {
        instance = this;
    }
    public void UpdateLife(int leftLife)
    {
        leftHumanImage.rectTransform.sizeDelta = new Vector2(54 * leftLife, 110);
    }
    public void UpdateTimer(float rate)
    {
        timer.value = rate;
        timerFillImage.color = Color.Lerp(Color.yellow, Color.green, rate);
    }
}
