using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInteraction : MonoBehaviour
{
    private Camera currentCam;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI promptText;
    GameObject player;
    private void Start() {
        uiPanel.SetActive(false);
        player= GameObject.FindWithTag("Player");
    }
    private void Update() {
        //Let the text rotate with the camera
        currentCam=Camera.main;
        uiPanel.transform.LookAt(uiPanel.transform.position + currentCam.transform.rotation * Vector3.forward,
                             currentCam.transform.rotation * Vector3.up);
        // Check if the player object is found
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            // Follow the player's movement in the X and Z axes while keeping the Y value
            transform.position = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
        }
    }
    public bool IsDisplayed=false;
    public void SetUp(string theText){
        promptText.text=theText;
        uiPanel.SetActive(true);
        IsDisplayed=true;
    }
    public void closePanel(){
        uiPanel.SetActive(false);
        IsDisplayed=false;
    }
}
