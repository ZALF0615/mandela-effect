using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScreenScript : MonoBehaviour
{
    public GameObject button;
    private void Start()
    {
        StartCoroutine(ButtonAppear());
        Time.timeScale = 0f;
    }
    IEnumerator ButtonAppear()
    {
        yield return new WaitForSecondsRealtime(2f);
        button.SetActive(true);
    }
    public void ReturnToStory(int num)
    {
        Time.timeScale = 1f;
        GameManager.GetInstance().ReturntoStory(num);
    }
}
