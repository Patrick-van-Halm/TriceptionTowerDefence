using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeIsReadyText : MonoBehaviour
{
    [SerializeField] private TMP_Text readyElement;

    public void ReadyStateChanged(bool isReady)
    {
        string text = $"Is{(isReady ? "" : " not")} ready!";
        readyElement.text = text;
    }
}
