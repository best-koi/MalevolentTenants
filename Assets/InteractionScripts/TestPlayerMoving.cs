using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMoving : MonoBehaviour
{
    void Update()
    {
        float xvalue= Input.GetAxis("Horizontal");
        float zvalue= Input.GetAxis("Vertical");
        transform.Translate(xvalue*Time.deltaTime*7f,0,zvalue*Time.deltaTime*7f);
        transform.Rotate(0,Input.GetAxis("Jump"),0); //rotate player
    }
}
