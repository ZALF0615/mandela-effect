using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaboGameManager : MonoBehaviour
{
    public static SaboGameManager instance;
    public Transform playerRespawn;
    public List<BombInsertableObj> insertedObj = new List<BombInsertableObj>();
    public Sprite[] sprite;
    void Start()
    {
        instance = this;
    }
    public void Arrest()
    {
        foreach (var item in insertedObj) item.BombRemoved();
        insertedObj.Clear();
        PlayerMove.instance.gameObject.transform.position = playerRespawn.position;
    }
    public void FireAllBomb()
    {
        Debug.Log("Fire");
        foreach (var item in insertedObj) item.BreakObj();
        insertedObj.Clear();
    }
}
