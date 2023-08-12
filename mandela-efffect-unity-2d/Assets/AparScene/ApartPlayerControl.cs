using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlnoBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    float dir;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) ApartGameManager.instance.MoveHuman(1);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) ApartGameManager.instance.MoveHuman(-1);
    }
}
