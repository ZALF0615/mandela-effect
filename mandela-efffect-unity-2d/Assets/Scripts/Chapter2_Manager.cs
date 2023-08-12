using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Chapter2_Manager : MonoBehaviour
{
    Camera camera;
    GameObject cameraAngleObject;

    GameObject prisonerMandela;
    GameObject prisoners;
    GameObject prisoners2;

    GameObject prisoners3;
    SpriteRenderer prisonerMandela_Close_eye;
    SpriteRenderer prisonerMandela_Open_eye;
    float close_Eye_Cool;
    float close_Eye_Time;
    bool close_Eye_Bool;

    Text captureNumText;
    int chaptureNum;
    int chaptureSuccess;

    int phaseNum;

    AudioSource cameraSound;

    GameObject resultImages;

    //UnityEngine.Color flash;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        cameraAngleObject = GameObject.Find("CameraAngleObject");
        prisonerMandela = GameObject.Find("prisoner mandela1");
        prisoners = GameObject.Find("PrisonerObjects");
        prisoners2 = GameObject.Find("PrisonerObjects2"); prisoners2.SetActive(false);
        prisoners3 = GameObject.Find("PrisonerObjects3"); prisoners3.SetActive(false);
        prisonerMandela_Close_eye = GameObject.Find("mandela_close_eye").GetComponent<SpriteRenderer>();
        prisonerMandela_Open_eye = GameObject.Find("mandela_open_eye").GetComponent<SpriteRenderer>();
        captureNumText = GameObject.Find("CaptureNumText").GetComponent<Text>();
        resultImages = GameObject.Find("Canvas").transform.Find("ResultBackGround").gameObject;
        cameraSound = gameObject.GetComponent<AudioSource>();
        chaptureNum = 1;
        phaseNum = 1;
        close_Eye_Cool = 3;
        close_Eye_Time = 3;
        close_Eye_Bool = true;
        //flash = GameObject.Find("Flash").GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        //���� �Կ� ǥ��
        captureNumText.text = "���� " + chaptureNum + "��° �Կ�";

        //if (flash.a > 0) flash.a -= 0.1f * Time.deltaTime;

        // 4��° �Կ� �Ͻ� ���â����
        if (chaptureNum == 4)
        {
            resultImages.SetActive(true);
        }
        else
        {
            //����� ���� ��ȭ
            GamePhase(phaseNum);

            //ī�޶� �ޱ� Ű���� �̵�
            if (Input.GetKey(KeyCode.UpArrow)) cameraAngleObject.transform.Translate(Vector2.up * 0.1f);
            else if (Input.GetKey(KeyCode.DownArrow)) cameraAngleObject.transform.Translate(Vector2.down * 0.1f);
            if (Input.GetKey(KeyCode.LeftArrow)) cameraAngleObject.transform.Translate(Vector2.left * 0.1f);
            else if (Input.GetKey(KeyCode.RightArrow)) cameraAngleObject.transform.Translate(Vector2.right * 0.1f);

            cameraAngleObject.transform.position = new Vector3(Mathf.Clamp(cameraAngleObject.transform.position.x, -15, 15),
                Mathf.Clamp(cameraAngleObject.transform.position.y, -1.5f, 1.5f), 0);

            //ī�޶� �ޱ� �̵��� ���� ī�޶� �̵�(������ ��������)
            camera.transform.position = cameraAngleObject.transform.position;
            camera.transform.position = new Vector3(Mathf.Clamp(camera.transform.position.x, -15, 15), Mathf.Clamp(camera.transform.position.y, -1.5f, 1.5f), -10);

            //�����̽��� ���� �Կ�
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cameraSound.Play(); // ������ ���
                //flash.a = 1;

                //���� ��ġ ���
                var capturePointX = math.abs(cameraAngleObject.transform.position.x) - math.abs(prisonerMandela.transform.position.x);
                var capturePointY = math.abs(cameraAngleObject.transform.position.y) - math.abs(prisonerMandela.transform.position.y);

                if (phaseNum == 3)
                {
                    //����
                    if (capturePointX < 1 && capturePointX > -1 && capturePointY < 1.5f && capturePointY > -1.5f && close_Eye_Bool)
                    {
                        Debug.Log(capturePointX + " " + capturePointY + " " + close_Eye_Bool.ToString());
                        chaptureSuccess++;
                        Debug.Log("����");
                    }
                    //����
                    else
                    {
                        Debug.Log(capturePointX + " " + capturePointY + close_Eye_Bool.ToString());
                        Debug.Log("����");
                    }
                }
                else
                {
                    //����
                    if (capturePointX < 1 && capturePointX > -1 && capturePointY < 1 && capturePointY > -1)
                    {
                        Debug.Log(capturePointX + " " + capturePointY);
                        chaptureSuccess++;
                        Debug.Log("����");
                    }
                    //����
                    else
                    {
                        Debug.Log(capturePointX + " " + capturePointY);
                        Debug.Log("����");
                    }
                }
                phaseNum++; // ������ +1
                chaptureNum++; // ������ȸ +1
            }
        }
    }

    private void GamePhase(int phase)
    {
        switch(phase)
        {
            case 1:
                prisoners.transform.Translate(Vector2.left * 0.007f);
                break;
            case 2:
                prisoners.SetActive(false);
                prisoners2.SetActive(true);
                prisonerMandela = GameObject.Find("prisoner mandela2");
                break;
            case 3:
                prisoners2.SetActive(false);
                prisoners3.SetActive(true);
                close_Eye_Cool -= Time.deltaTime;
                if (close_Eye_Cool < 0)
                {
                    prisonerMandela.GetComponent<SpriteRenderer>().sprite = prisonerMandela_Close_eye.sprite;
                    close_Eye_Bool = false;
                    close_Eye_Time -= Time.deltaTime;
                }
                if (close_Eye_Time < 0)
                {
                    prisonerMandela.GetComponent<SpriteRenderer>().sprite = prisonerMandela_Open_eye.sprite;
                    close_Eye_Bool = true;
                    close_Eye_Cool = 3;
                    close_Eye_Time = 3;
                }
                prisonerMandela = GameObject.Find("prisoner mandela3");
                break;
            default:
                break;
        }
    }
}
