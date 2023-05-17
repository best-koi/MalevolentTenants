using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInteraction : MonoBehaviour
{
    private Camera currentCam;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI promptText;
    private void Start() {
        uiPanel.SetActive(false);
    }
    private void Update() {
        //Let the text rotate with the camera
        currentCam=Camera.main;
        var rotation= currentCam.transform.rotation;
        transform.LookAt(transform.position+rotation*Vector3.forward, rotation * Vector3.up);
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
