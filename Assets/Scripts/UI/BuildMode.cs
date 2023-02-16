using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildMode : MonoBehaviour
{
    [SerializeField] private Transform _buildablesList;
    [SerializeField] private GameObject _buildableListItemPrefab;

    [SerializeField] private TMP_Text _moneyField;
    [SerializeField] private IntEvent _onMoneyChanged;

    private void Awake()
    {
        _onMoneyChanged?.RegisterAction(UpdateMoney);
    }

    public void Toggle()
    {
        BuildModeManager.Instance.ToggleBuildMode();
    }

    private void OnEnable()
    {
        if (_moneyField && MoneyManager.Instance) UpdateMoney();
        if (_buildablesList && BuildModeManager.Instance) UpdateBuildablesList();
    }

    private void UpdateMoney()
    {
        UpdateMoney(MoneyManager.Instance.Money);
    }

    private void UpdateMoney(int money)
    {
        _moneyField.text = $"${money}";
    }

    private void UpdateBuildablesList()
    {
        for(int i = _buildablesList.childCount - 1; i >= 0; i--)
            Destroy(_buildablesList.GetChild(i).gameObject);

        var towers = BuildModeManager.Instance.Towers;
        foreach(var tower in towers)
        {
            var listItem = Instantiate(_buildableListItemPrefab, _buildablesList);
            listItem.GetComponent<BuildableListItem>().Tower = tower;
        }
    }
}
