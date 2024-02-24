using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraManager instance;
    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        if(instance == null)
        instance = this;
    }
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLookAt(Transform pos)
    {
        virtualCamera.LookAt = pos;
        virtualCamera.Follow = pos;
    }
}
