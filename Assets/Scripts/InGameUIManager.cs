using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    private static InGameUIManager _instance;

    public void Start()
    {
        _instance = this;
        _instance.UIBaseValues();
    }

    public static InGameUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InGameUIManager();
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject winBackground;
    [SerializeField] private TextMeshProUGUI doorUnlockedMessage;
    [SerializeField] private Image lockImage;
    [SerializeField] private Sprite locked;
    [SerializeField] private Sprite opened;

    private void UIBaseValues()
    {
        winBackground.SetActive(false);
        doorUnlockedMessage.gameObject.SetActive(false);
        doorUnlockedMessage.color = new Color(255, 255, 255, 0);
        lockImage.gameObject.SetActive(false);
    }

    public void DoorUnlocked()
    {
        StartCoroutine(AnimateDoorUnlockedMessage());
    }

    private IEnumerator AnimateDoorUnlockedMessage()
    {
        doorUnlockedMessage.gameObject.SetActive(true);

        /*
        int alphaValue = 0;
        doorUnlockedMessage.color = new Color(255, 255, 255, alphaValue);
        while (doorUnlockedMessage.color.a < 255)
        {
            doorUnlockedMessage.color = new Color(255, 255, 255, alphaValue);
            alphaValue += 5;
            yield return null;
            //yield return new WaitForSeconds(0.1f);
        }
        */
        doorUnlockedMessage.color = new Color(255, 255, 255, 255);

        lockImage.gameObject.SetActive(true);
        lockImage.sprite = locked;
        yield return new WaitForSeconds(1f);
        lockImage.sprite = opened;
        yield return new WaitForSeconds(2f);
        lockImage.gameObject.SetActive(false);

        /*
        while (doorUnlockedMessage.color.a > 0)
        {
            doorUnlockedMessage.color = new Color(255, 255, 255, alphaValue);
            alphaValue -= 5;
            yield return null;
            //yield return new WaitForSeconds(0.1f);
        }
        */
        doorUnlockedMessage.color = new Color(255, 255, 255, 0);

        doorUnlockedMessage.gameObject.SetActive(false);
    }

    public void YouEscapedToggle(bool toggleValue)
    {
        winBackground.gameObject.SetActive(toggleValue);
    }
}
