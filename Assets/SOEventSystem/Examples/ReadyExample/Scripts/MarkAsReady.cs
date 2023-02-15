using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkAsReady : MonoBehaviour
{
    [SerializeField] BoolEvent readyStateChangedEvent;
    private bool isReady = false;

    private void Start()
    {
        readyStateChangedEvent?.Invoke(isReady);
    }

    public void ReadyStateChanged()
    {
        isReady = !isReady;
        readyStateChangedEvent?.Invoke(isReady);
    }
}
