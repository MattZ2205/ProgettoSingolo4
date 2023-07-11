using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject storageCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shef"))
        {
            mainCam.SetActive(!mainCam.activeSelf);
            storageCam.SetActive(!storageCam.activeSelf);
        }
    }
}
