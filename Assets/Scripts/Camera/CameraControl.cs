using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CameraControl : MonoBehaviour
{
    public Transform frog;
    public float offsetY;
    public float zoomBase;
    private float _ratito;
    private ObjectPool<GameObject> _pool;
    

    private void Start()
    {
        //Camera.main.aspect = (float)Screen.width / (float)Screen.height;
        _ratito = (float)Screen.height / (float)Screen.width;
        if (Camera.main != null)
        {
        Camera.main.orthographicSize = zoomBase * _ratito * 0.5f;
        
        }else
        {
            Debug.Log("Camera is null");
        }
        
        
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(frog.position.x, frog.position.y + offsetY, transform.position.z);
    }
}
