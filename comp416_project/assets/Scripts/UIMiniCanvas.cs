using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMiniCanvas : MonoBehaviour
{
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    // rearranges the mini UI canvas of objects to be looking at the camera
    void Update()
    {
        transform.eulerAngles = cam.transform.eulerAngles;
    }
}