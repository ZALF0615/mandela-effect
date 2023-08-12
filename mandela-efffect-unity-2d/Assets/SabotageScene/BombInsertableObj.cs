using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombInsertableObj : MonoBehaviour
{
    
    SpriteRenderer sr;
    bool isInserted;
    float leftTime;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        isInserted = false;
        leftTime = 3f;
    }
    void BombInserted()
    {
        if (isInserted) return;
        isInserted = true;
        sr.sprite = SaboGameManager.instance.sprite[1];
        SaboGameManager.instance.insertedObj.Add(this);
    }
    public void BombRemoved()
    {
        isInserted = false;
        sr.sprite = SaboGameManager.instance.sprite[0];
        leftTime = 3f;
    }
    public void BreakObj() { Destroy(gameObject); }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (leftTime < 0) return;
        if (collision.gameObject.tag == "Player") { SaboTageUIManager.instance.timer.gameObject.SetActive(true); leftTime = 3f; }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (leftTime < 0) return;
        if (collision.gameObject.tag == "Player") { SaboTageUIManager.instance.timer.gameObject.SetActive(false); leftTime = 3f; }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isInserted) return;
        if (collision.gameObject.tag == "Player") {
            leftTime = PlayerMove.instance.isHoldSpace ? leftTime -= Time.deltaTime : 3f;
            SaboTageUIManager.instance.UpdateTimer((3f - leftTime) / 3f);
            if (leftTime < 0) { BombInserted(); SaboTageUIManager.instance.timer.gameObject.SetActive(false); }
        }
    }
}
