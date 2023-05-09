using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Alaina Klaes
// Script based off of https://www.tutorialspoint.com/unity/unity_basic_movement_scripting.htm
public class BasicTestingPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        gameObject.transform.position = new Vector3(transform.position.x + (h * speed),
           transform.position.y, transform.position.z + (v * speed));
    }
}