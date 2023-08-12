using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerMove.instance.isHoldSpace ) SaboGameManager.instance.FireAllBomb();
        
    }
}
