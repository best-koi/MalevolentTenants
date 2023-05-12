using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    [SerializeField] 
    private Text Ans;

    private Text Reset;

    [SerializeField]
    private string Answer;

    public void Number(int number)
    {
        if(Ans.text == "Correct" || Ans.text == "Wrong")
        {
            Ans.text = "";
        }
        Ans.text += number.ToString();
    }

    public void Execute()
    {
        if (Ans.text == Answer)
        {
            Ans.text = "Correct";
        }
        else
        {
            Ans.text = "Wrong";
        }
    }
}
