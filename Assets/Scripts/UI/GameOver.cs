using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private IntEvent _onHealthChanged;
    [SerializeField] private GameObject _uiElement;

    private void Awake()
    {
        _onHealthChanged.RegisterAction(OnHealthChanged);
    }

    private void OnHealthChanged(int health)
    {
        if (health > 0) return;
        if (_uiElement == null) return;
        Time.timeScale = 0;
        _uiElement.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
