using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;

    private float startFOV, targetFOV;

    public float zoomSpeed = 1f;

    public Camera theCam;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        startFOV = theCam.fieldOfView;
        targetFOV = startFOV;
    }

    void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, targetFOV,zoomSpeed * Time.deltaTime);
    }

    public void zoomIn(float newZoom)
    {
        targetFOV = newZoom;
    }

    public void zoomOut()
    {
        targetFOV = startFOV;
    }
}
