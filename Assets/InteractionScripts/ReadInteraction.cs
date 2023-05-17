using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ReadInteraction : MonoBehaviour
{
    [SerializeField] public GameObject uiPanel;
    private Camera currentCam;
    public bool isReadNote;
    public bool UseKeyBoard;
    
    private void Start() {
        uiPanel.SetActive(false);
        isReadNote=false;
    }
    public void Update() {
        currentCam=Camera.main;
        if(isReadNote){
            uiPanel.transform.LookAt(uiPanel.transform.position + currentCam.transform.rotation * Vector3.forward,currentCam.transform.rotation * Vector3.up);
            uiPanel.SetActive(true);
            Time.timeScale=0;
            if(UseKeyBoard && Input.GetKeyDown(KeyCode.R)){
                uiPanel.SetActive(false);
                isReadNote=false;
                Time.timeScale=1f;
            }
        }
            
    }
}

