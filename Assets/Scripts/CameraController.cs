using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class CameraController : MonoBehaviour
{
    public List<Camera> cameras;
    private int currentCameraIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(var camera in cameras){
            camera.gameObject.SetActive(false);
        }
        if(cameras.Count > 0){
            cameras[currentCameraIndex % cameras.Count].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame && cameras.Count > 0){
            cameras[currentCameraIndex % cameras.Count].gameObject.SetActive(false);
            currentCameraIndex ++;
            cameras[currentCameraIndex % cameras.Count].gameObject.SetActive(true);
        }
    }

}
