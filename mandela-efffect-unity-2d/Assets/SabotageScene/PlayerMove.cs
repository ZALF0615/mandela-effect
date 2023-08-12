using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    public float speed = 3f;
    public bool isHoldSpace { get { return Input.GetKey(KeyCode.Space); } }
    public bool isArrest;
    Rigidbody2D rigid;
    void Start()
    {
        instance = this;
        rigid = GetComponent<Rigidbody2D>();
        isArrest = false;
    }
    void Update()
    {
        rigid.velocity = isArrest ? Vector3.zero : (Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical")).normalized * speed;
    }
}
