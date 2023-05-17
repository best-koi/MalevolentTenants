using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Passcode : MonoBehaviour
{
    private string code="12345";
    private string number;
    private int numberIndex=0; 
    [SerializeField] private int maxnum=5;//Not allows player input over 5 number
    [SerializeField] private TextMeshProUGUI NumberText;
    [SerializeField] public GameObject keypad;
    private ReadInteraction readInteraction;
    private void Start() {
        readInteraction=keypad.GetComponent<ReadInteraction>();
        NumberText.text=null;
    }
    public void CodeFunction(string Nr){
        numberIndex++;
        if(numberIndex>maxnum){
            Debug.Log("Wrong code!"); //probably change to error sound
        }else{
            number=number+Nr;
            NumberText.text=number;
        }
    }
    public void Enter(){
        if(number==code){
            if(readInteraction!=null){
                readInteraction.uiPanel.SetActive(false);
                readInteraction.isReadNote=false;
                Time.timeScale=1f;
            }
            numberIndex=0;
            number=null;
            NumberText.text=null;
            Debug.Log("Door Open!");//change this to door open animation or something else
        }else{
            Debug.Log("Wrong code!");//probably change to error sound
        }
    }
    public void Clear(){
        numberIndex=0;
        number=null;
        NumberText.text=null;
    }

    public void Return(){
        if(readInteraction!=null){
                readInteraction.uiPanel.SetActive(false);
                readInteraction.isReadNote=false;
                Time.timeScale=1f;
            }
            numberIndex=0;
            number=null;
            NumberText.text=null;
    }
}
