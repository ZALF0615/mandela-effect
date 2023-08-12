using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombInsertableObj : MonoBehaviour
{
    
    SpriteRenderer sr;
    bool isInserted;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        isInserted = false;
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
    }
    public void BreakObj()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") BombInserted();
    }
}
