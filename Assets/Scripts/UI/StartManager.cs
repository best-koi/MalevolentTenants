using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] public GameObject doorLight;

    IEnumerator FadeIn()
    {
        doorLight.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }

    public void Play()
    {
        StartCoroutine(FadeIn());
        
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
