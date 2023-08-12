using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaboGameManager : MonoBehaviour
{
    public static SaboGameManager instance;
    public Transform playerRespawn;
    public List<BombInsertableObj> insertedObj = new List<BombInsertableObj>();
    public Sprite[] sprite;
    int leftLife;
    void Start()
    {
        instance = this;
        leftLife = 3;
    }
    public void Arrest()
    {
        foreach (var item in insertedObj) item.BombRemoved();
        insertedObj.Clear();
        PlayerMove.instance.gameObject.transform.position = playerRespawn.position;
        SaboTageUIManager.instance.UpdateLife(--leftLife);
    }
    public void FireAllBomb()
    {
        foreach (var item in insertedObj) item.BreakObj();
        insertedObj.Clear();
    }
}
