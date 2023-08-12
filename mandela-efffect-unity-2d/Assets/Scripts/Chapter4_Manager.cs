using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter4_Manager : MonoBehaviour
{
    GameObject canvas;
    public GameObject[] wards;
    GameObject[] gameWards;
    
    //�ǹ���� ����
    public bool feverMode;
    float feverModeCool;
    float feverModeTime;
    float feverModeEnemeAttackCool;
    Image[] feverGauge;
    //â�� ����
    float nextDayTime;
    Image windowImage;
    public Image[] windowImageArray;
    //������ �̹��� ����
    Image mandelaImage;
    public Image[] mandelaImageArray;
    //����� �̹��� ����
    Image presidentImage;
    public Image[] presidentImageArray;
    //�� �̹��� ����
    Image enemyImage;
    public Image[] EnemyImageArray;
    //�޷� ����
    Text calendarText;
    int calendarMonth;
    int calendarDay;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        gameWards = new GameObject[5];
        //�ǹ���� ����
        feverModeCool = 5;
        feverModeTime = 5;
        feverGauge = new Image[2];
        feverGauge[0] = canvas.transform.Find("FeverImage1").GetComponent<Image>();
        feverGauge[1] = canvas.transform.Find("FeverImage2").GetComponent<Image>();
        feverMode = false;
        feverModeEnemeAttackCool = 0.5f;
        //â�� ����
        nextDayTime = 2.1f;
        windowImage = GameObject.Find("WindowImage").GetComponent<Image>();
        //������ �̹��� ����
        mandelaImage = GameObject.Find("MandelaImage").GetComponent<Image>();
        //����� �̹��� ����
        presidentImage = GameObject.Find("PresidentImage").GetComponent<Image>();
        //�� �̹��� ����
        enemyImage = GameObject.Find("EnemyImage").GetComponent<Image>();
        //�޷� ����
        calendarText = GameObject.Find("CalendarText").GetComponent<Text>();
        calendarMonth = 12;
        calendarDay = 1;

    }


    void Update()
    {
        //�й� ���� ����
        if (feverGauge[0].fillAmount <= 0)
        {
            return;
        }

        //���� �ǹ� ��� üũ
        Chapter4Game(feverMode);

        //â�� �ǽð� �̹��� ����
        nextDayTime -= Time.deltaTime;
        if (nextDayTime < 0.7f) windowImage.sprite = windowImageArray[2].sprite;
        else if(nextDayTime < 1.4f) windowImage.sprite = windowImageArray[1].sprite;
        else windowImage.sprite = windowImageArray[0].sprite;

        //��¥ ����
        if (nextDayTime <= 0)
        {
            if (calendarDay == 30)
            {
                calendarDay = 1;
                switch (calendarMonth)
                {
                    case 12:
                        calendarMonth = 1;
                        break;
                    case 1:
                        calendarMonth = 2;
                        break;
                    default:
                        break;
                }
            }
            else calendarDay++;
            nextDayTime = 2.1f;
        }

        //Ŭ���� ���� ����
        if (calendarMonth == 2 && calendarDay == 11) return;

        //������, �����, �� �ǽð� �̹��� ����
        if (feverGauge[0].fillAmount > 0.75f)
        {
            mandelaImage.sprite = mandelaImageArray[0].sprite; // ������ �ǰ�
            presidentImage.sprite = presidentImageArray[0].sprite; // ����� �Ǳ���
            enemyImage.sprite = EnemyImageArray[0].sprite; // �� ����
        }
        else if (feverGauge[0].fillAmount > 0.5f)
        {
            mandelaImage.sprite = mandelaImageArray[1].sprite; // ������ ����
            presidentImage.sprite = presidentImageArray[1].sprite; // ����� ����
            enemyImage.sprite = EnemyImageArray[1].sprite; // �� ����
        }
        else if (feverGauge[0].fillAmount > 0.25f)
        {
            mandelaImage.sprite = mandelaImageArray[2].sprite; // ������ ���
            presidentImage.sprite = presidentImageArray[2].sprite; // ����� ����
            enemyImage.sprite = EnemyImageArray[2].sprite; // �� �Ǳ���
        }
        else if (feverGauge[0].fillAmount <= 0) mandelaImage.sprite = mandelaImageArray[3].sprite; // ������ ���

        calendarText.text = calendarMonth + "��\n" + calendarDay + "��";
    }

    public void Chapter4Game(bool feverMode)
    {
        if (!feverMode)
        {
            feverGauge[0].fillAmount -= 0.15f * Time.deltaTime;
            feverGauge[1].fillAmount += 0.15f * Time.deltaTime;

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
                feverGauge[0].fillAmount += 0.05f;
                feverGauge[1].fillAmount -= 0.05f;
            }
            else if (gameWards[0] != null && inputWard != gameWards[0].tag && inputWard != null)
            {
                Destroy(gameWards[0]);
                gameWards[0] = null;
                feverGauge[0].fillAmount -= 0.2f;
                feverGauge[1].fillAmount += 0.2f;
            }

            feverModeCool -= Time.deltaTime;
            if (feverModeCool <= 0)
            {
                for (int i = 0; i < gameWards.Length; i++)
                {
                    Destroy(gameWards[i]);
                    gameWards[i] = null;
                }
                this.feverMode = true;
            }
        }
        else if (feverMode)
        {
            feverModeTime -= Time.deltaTime;

            //�ǹ� ��� ����
            if (feverModeTime <= 0)
            {
                this.feverMode = false;
                feverModeCool = 5;
                feverModeTime = 5;
                feverModeEnemeAttackCool = 0.5f;
                //���ݱ����� �̹��� ����
                for (int i = 0; i < gameWards.Length; i++)
                {
                    Destroy(gameWards[i]);
                    gameWards[i] = null;
                }
                return;
            }

            //�ǽð� �ǹ� ������ ���� �� ����
            feverGauge[0].fillAmount -= 0.1f * Time.deltaTime;
            feverGauge[1].fillAmount += 0.1f * Time.deltaTime;

            //�ǽð� �� �������� ������ ���� �� ����
            feverModeEnemeAttackCool -= Time.deltaTime;
            if (feverModeEnemeAttackCool <= 0) 
            {
                feverGauge[0].fillAmount -= 0.1f;
                feverGauge[1].fillAmount += 0.1f;
                feverModeEnemeAttackCool = 0.35f;
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

            //����
            if (gameWards[0] != null && inputWard == gameWards[0].tag && inputWard != null)
            {
                Destroy(gameWards[0]);
                gameWards[0] = null;
                feverGauge[0].fillAmount += 0.05f;
                feverGauge[1].fillAmount -= 0.05f;
            }
            //������
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
