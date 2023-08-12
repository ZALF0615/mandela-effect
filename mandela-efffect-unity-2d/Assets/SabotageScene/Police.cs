using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Police : MonoBehaviour
{
    public float speed = 0.25f;
    float waitDelay;
    int nowPos = 0;
    [SerializeField]
    Vector3[] movePoses;
    Vector3 moveDir;
    public Transform spriteTrans;
    public Sprite[] sprite;
    SpriteRenderer sr;
    private void Start()
    {
        movePoses = GetComponentsInChildren<Transform>()[2..].Select(x => x.position).ToArray();
        moveDir = (movePoses[nowPos] - transform.position);
        transform.rotation = Quaternion.Euler(0, 0, Vector3.Angle(Vector3.right, moveDir) * (moveDir.y < 0 ? -1 : 1));
        spriteTrans.rotation = Quaternion.identity;
        waitDelay = 0;
        sr = spriteTrans.GetComponent<SpriteRenderer>();
        sr.sprite = sprite[moveDir.y > 0.75f ? 1 : moveDir.y < -0.75f ? 2 : 0];
        sr.flipX = moveDir.x > 0;
    }
    private void Update()
    {
        if(waitDelay > 0) { waitDelay -= Time.deltaTime; return; }
        moveDir = (movePoses[nowPos] - transform.position);
        if (moveDir.magnitude < 0.5f)
        {
            waitDelay = 1f;
            nowPos = (nowPos + 1) % movePoses.Length;
            moveDir = (movePoses[nowPos] - transform.position).normalized;
            transform.rotation = Quaternion.Euler(0, 0, Vector3.Angle(Vector3.right, moveDir) * (moveDir.y < 0 ? -1 : 1));
            spriteTrans.rotation = Quaternion.identity;
            sr.sprite = sprite[moveDir.y > 0.75f ? 1 : moveDir.y < -0.75f ? 2 : 0];
            sr.flipX = moveDir.x > 0;
        }
        transform.position += moveDir.normalized*Time.deltaTime*speed;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerMove.instance.transform.position.y > -5 && !PlayerMove.instance.isArrest) { 
            PlayerMove.instance.isArrest = true;
            SaboGameManager.instance.Arrest();
        }

    }
}
    