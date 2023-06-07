using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallPause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [HideInInspector] private bool pauseState = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseState = !pauseState; //inverts current status of pause
            pauseMenu.SetActive(pauseState);

            if(pauseState)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
