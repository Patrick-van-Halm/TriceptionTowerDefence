using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildableListItem : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TowerData Tower;

    [Header("Button colors")]
    [SerializeField] private Color _colorDefault;
    [SerializeField] private Color _colorHover;
    [SerializeField] private Color _colorClick;

    [Header("Price")]
    [SerializeField] private TMP_Text _priceField;
    [SerializeField] private Color _colorAffordable;
    [SerializeField] private Color _colorNotAffordable;

    [Header("Elements")]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;

    [Header("Events")]
    [SerializeField] private IntEvent _onMoneyChanged;

    private Image _image;
    private bool _affordable;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = _colorDefault;

        _onMoneyChanged.RegisterAction(CheckAffordable);
    }

    private void Start()
    {
        if (!Tower) return;
        _name.text = Tower.DisplayName;
        _description.text = Tower.Description;
        _priceField.text = $"${Tower.Price}";

        if (!MoneyManager.Instance) return;
        CheckAffordable(MoneyManager.Instance.Money);
    }

    private void CheckAffordable(int money)
    {
        if (!Tower) return;
        _affordable = money >= Tower.Price;
        _priceField.color = _affordable ? _colorAffordable : _colorNotAffordable;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = _colorHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _colorDefault;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.color = _colorClick;
        if (!_affordable) return;
        BuildModeManager.Instance.Build(Tower);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.color = _colorHover;
    }
}
