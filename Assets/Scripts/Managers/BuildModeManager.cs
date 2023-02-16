using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeManager : SingletonMonoBehaviour<BuildModeManager>
{
    [Header("Build Cam Settings")]
    [SerializeField] private GameObject _buildCam;
    [SerializeField] private GameObject _buildCanvas;
    [SerializeField] private LayerMask _buildLayers;
    
    [Header("Main Cam Settings")]
    [SerializeField] private GameObject _mainCam;
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private LayerMask _mainLayers;

    [Header("Buildables")]
    [SerializeField] private TowerData[] _towers;

    [Header("Settings")]
    [SerializeField, TagField] private string _groundTag;
    [SerializeField] private Material _towerRangeMaterial;

    private TowerData _selectedTower;
    private Tower _selectedTowerObj;

    public TowerData[] Towers => _towers;
    public Material TowerRangeMaterial => _towerRangeMaterial;

    private void Update()
    {
        if (!_selectedTower) return;
        if (Physics.Raycast(CameraManager.Instance.MainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
        {
            if (!hit.collider.CompareTag(_groundTag))
            {
                EnableGhost(false);
                return;
            }

            _selectedTowerObj.transform.position = hit.point;
            EnableGhost(true);
            if (Input.GetMouseButtonDown(0)) PlaceTower();
        }
        else EnableGhost(false);
    }

    private void PlaceTower()
    {
        MoneyManager.Instance.Spend(_selectedTower.Price);
        _selectedTowerObj.EnableRangeVisualizer(false);
        _selectedTowerObj.StartTowerLoop();
        _selectedTowerObj = null;
        _selectedTower = null;
    }

    public void ToggleBuildMode()
    {
        bool active = !CameraManager.Instance.IsCamActive(_buildCam);
        CameraManager.Instance.ActivateCam(active ? _buildCam : _mainCam);
        CameraManager.Instance.SetCamLayers(active ? _buildLayers : _mainLayers);
        _buildCanvas.SetActive(active);
        _mainCanvas.SetActive(!active);
        _selectedTower = null;
        ResetGhost();
    }

    public void Build(TowerData tower)
    {
        _selectedTower = tower;
        ResetGhost();

        _selectedTowerObj = Instantiate(_selectedTower.Prefab).GetComponent<Tower>();
        _selectedTowerObj.EnableRangeVisualizer(true);
    }

    private void EnableGhost(bool enabled)
    {
        if (!_selectedTowerObj) return;
        _selectedTowerObj.gameObject.SetActive(enabled);
    }

    private void ResetGhost()
    {
        if (!_selectedTowerObj) return;
        Destroy(_selectedTowerObj.gameObject);
        _selectedTowerObj = null;
    }
}
