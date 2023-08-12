using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    public float speed = 3f;
    Rigidbody2D rigid;
    void Start()
    {
        instance = this;
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rigid.velocity = (Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical")).normalized * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn") SaboGameManager.instance.FireAllBomb();
    }
}
