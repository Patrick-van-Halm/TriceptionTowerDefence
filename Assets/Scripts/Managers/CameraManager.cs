using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingletonMonoBehaviour<CameraManager>
{
    [SerializeField] private Camera _mainCam;
    [SerializeField] private List<CinemachineVirtualCamera> _vCams = new();
    private CinemachineVirtualCamera _activeVCam;

    public Camera MainCamera => _mainCam;

    private void OnEnable()
    {
        ActivateCam(0);
    }

    public void ActivateCam(string camName)
    {
        ActivateCam(_vCams.FindIndex(c => c.name == camName));
    }

    public void ActivateCam(GameObject camObj)
    {
        ActivateCam(_vCams.FindIndex(c => c.gameObject == camObj));
    }

    private void ActivateCam(int i)
    {
        if (i == -1) return;
        var cam = _vCams[i];
        cam.Priority = 10;
        if (_activeVCam != null) _activeVCam.Priority = 0;
        _activeVCam = cam;
    }

    public bool IsCamActive(GameObject camObj)
    {
        return _activeVCam.gameObject == camObj;
    }

    public void SetCamLayers(LayerMask layers)
    {
        _mainCam.cullingMask = layers;
    }
}
