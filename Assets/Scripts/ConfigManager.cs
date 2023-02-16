using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerSettings.Instance.LoadData();
    }

    private void OnDisable()
    {
        PlayerSettings.Instance.SaveData();
    }
}
