using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
    //Need to combine with inventory system
    //This is just a test
    public bool HasKey=false;//for door
    public bool HasItem=false;//for puzzle
    private void Update() {
        if(Input.GetKeyDown(KeyCode.K)) HasKey=!HasKey;
        if(Input.GetKeyDown(KeyCode.P)) HasItem=!HasItem;
    }
}
