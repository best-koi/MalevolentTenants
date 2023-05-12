using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Alaina Klaes
// Script based off of Jimmy Vegas tutorial, https://www.youtube.com/watch?v=_TL1X_ecou0&list=PLZ1b66Z1KFKgClHCa0cfJ5qc6PMrpU9PR&index=8
public class ActivateCamera : MonoBehaviour
{
    [SerializeField] public GameObject cameraToActivate;
    [SerializeField] public GameObject cameraToDeactivate;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cameraToActivate.SetActive(true);
            cameraToDeactivate.SetActive(false);
        }
    }
}