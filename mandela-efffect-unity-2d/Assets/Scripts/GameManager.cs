using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject canvas;
    public GameObject[] wards;
    GameObject[] gameWards;
    Image hpImage;
    
    public bool feverMode;
    float feverModeCool;
    float feverModeEnemeAttackCool;

    Image[] feverGauge;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        gameWards = new GameObject[5];
        hpImage = canvas.transform.Find("HpImage").GetComponent<Image>();
        feverModeCool = 5;
        feverGauge = new Image[2];
        feverGauge[0] = canvas.transform.Find("FeverImage1").GetComponent<Image>();
        feverGauge[1] = canvas.transform.Find("FeverImage2").GetComponent<Image>();
        feverMode = false;
        feverModeEnemeAttackCool = 0.5f;
    }


    void Update()
    {
        Chapter4Game(feverMode);
    }

    public void Chapter4Game(bool feverMode)
    {
        if (!feverMode)
        {
            hpImage.fillAmount -= 0.1f * Time.deltaTime;

            for (int i = 0; i < gameWards.Length; i++)
            {
                if (gameWards[i] == null && i != 4)
                {
                    gameWards[i] = gameWards[i + 1];
                    gameWards[i + 1] = null;
                }
                else if (gameWards[i] == null && i == 4)
                {
                    gameWards[i] = Instantiate(wards[Random.Range(0, 4)], new Vector2(1917, 774), Quaternion.identity);
                    gameWards[i].transform.parent = canvas.transform;
                }

                if (i == 0 && gameWards[i] != null) gameWards[i].transform.position = new Vector2(1117, 774);
                else if (i == 1 && gameWards[i] != null) gameWards[i].transform.position = new Vector2(1317, 774);
                else if (i == 2 && gameWards[i] != null) gameWards[i].transform.position = new Vector2(1517, 774);
                else if (i == 3 && gameWards[i] != null) gameWards[i].transform.position = new Vector2(1717, 774);
            }

            string inputWard = null;

            if (Input.GetKeyDown(KeyCode.UpArrow)) inputWard = "Up";
            else if (Input.GetKeyDown(KeyCode.DownArrow)) inputWard = "Down";
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) inputWard = "Left";
            else if (Input.GetKeyDown(KeyCode.RightArrow)) inputWard = "Right";
            //else if (Input.GetKeyDown(KeyCode.Space)) inputWard = "Space";

            if (gameWards[0] != null && inputWard == gameWards[0].tag && inputWard != null)
            {
                Destroy(gameWards[0]);
                gameWards[0] = null;
                hpImage.fillAmount += 0.05f;
            }
            else if (gameWards[0] != null && inputWard != gameWards[0].tag && inputWard != null)
            {
                Destroy(gameWards[0]);
                gameWards[0] = null;
                hpImage.fillAmount -= 0.2f;
            }

            feverModeCool -= Time.deltaTime;
            if (feverModeCool <= 0) this.feverMode = true;
        }
        else if (feverMode)
        {
            //피버 게이지 활성화
            if (GameObject.Find("FeverImage1") == null && GameObject.Find("FeverImage2") == null)
            {
                feverGauge[0].gameObject.SetActive(true);
                feverGauge[1].gameObject.SetActive(true);
                //지금까지의 이미지 삭제
                for (int i = 0; i < gameWards.Length; i++)
                {
                    Destroy(gameWards[i]);
                    gameWards[i] = null;
                }
                feverGauge[0].fillAmount = 0.5f;
                feverGauge[1].fillAmount = 0.5f;
            }

            //피버 모드 승리
            if (feverGauge[0].fillAmount >= 1)
            {
                feverGauge[0].gameObject.SetActive(false);
                feverGauge[1].gameObject.SetActive(false);
                this.feverMode = false;
                feverModeCool = 5;
                feverModeEnemeAttackCool = 0.5f;
                //지금까지의 이미지 삭제
                for (int i = 0; i < gameWards.Length; i++)
                {
                    Destroy(gameWards[i]);
                    gameWards[i] = null;
                }
                return;
            }
            //피버 모드 패배
            else if(feverGauge[0].fillAmount <= 0)
            {
                return;
            }

            //실시간 피버 게이지 증가 및 종료
            feverGauge[0].fillAmount -= 0.1f * Time.deltaTime;
            feverGauge[1].fillAmount += 0.1f * Time.deltaTime;

            //실시간 적 공격으로 게이지 증가 및 종료
            feverModeEnemeAttackCool -= Time.deltaTime;
            if (feverModeEnemeAttackCool <= 0) 
            {
                feverGauge[0].fillAmount -= 0.1f;
                feverGauge[1].fillAmount += 0.1f;
                feverModeEnemeAttackCool = 0.5f;
            }

            for (int i = 0; i < gameWards.Length; i++)
            {
                if (gameWards[i] == null && i != 4)
                {
                    gameWards[i] = gameWards[i + 1];
                    gameWards[i + 1] = null;
                }
                else if (gameWards[i] == null && i == 4)
                {
                    gameWards[i] = Instantiate(wards[4], new Vector2(1917, 774), Quaternion.identity);
                    gameWards[i].transform.parent = canvas.transform;
                }

                if (i == 0 && gameWards[i] != null) gameWards[i].transform.position = new Vector2(1117, 774);
                else if (i == 1 && gameWards[i] != null) gameWards[i].transform.position = new Vector2(1317, 774);
                else if (i == 2 && gameWards[i] != null) gameWards[i].transform.position = new Vector2(1517, 774);
                else if (i == 3 && gameWards[i] != null) gameWards[i].transform.position = new Vector2(1717, 774);
            }

            string inputWard = null;

            if (Input.GetKeyDown(KeyCode.UpArrow)) inputWard = "Up";
            else if (Input.GetKeyDown(KeyCode.DownArrow)) inputWard = "Down";
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) inputWard = "Left";
            else if (Input.GetKeyDown(KeyCode.RightArrow)) inputWard = "Right";
            else if (Input.GetKeyDown(KeyCode.Space)) inputWard = "Space";

            //맞춤
            if (gameWards[0] != null && inputWard == gameWards[0].tag && inputWard != null)
            {
                Destroy(gameWards[0]);
                gameWards[0] = null;
                feverGauge[0].fillAmount += 0.05f;
                feverGauge[1].fillAmount -= 0.05f;
            }
            //못맞춤
            else if (gameWards[0] != null && inputWard != gameWards[0].tag && inputWard != null)
            {
                Destroy(gameWards[0]);
                gameWards[0] = null;
                feverGauge[0].fillAmount -= 0.05f;
                feverGauge[1].fillAmount += 0.05f;
            }
        }
    }
}
