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
        currentCam=Camera.main;
        uiPanel.transform.LookAt(uiPanel.transform.position + currentCam.transform.rotation * Vector3.forward,
                             currentCam.transform.rotation * Vector3.up);
       
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
