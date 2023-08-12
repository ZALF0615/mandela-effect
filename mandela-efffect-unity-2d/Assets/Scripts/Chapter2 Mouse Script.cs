using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2MouseScript : MonoBehaviour
{
    Vector3 mousePos;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        camera.transform.position = camera.WorldToScreenPoint(mousePos);
    }
}
