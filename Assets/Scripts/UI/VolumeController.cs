using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private TextMeshProUGUI volumeText;

    private void Start()
    {
        LoadValues();
    }
    
    public void VolumeSlider(float volume)
    {
        volumeText.text = Mathf.Round(volume * 100).ToString();
    }

    public void SaveVolume()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}
