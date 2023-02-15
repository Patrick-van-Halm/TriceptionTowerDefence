using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEvent : ScriptableObject
{
    protected List<BaseEventListener.Listener> _listeners = new();
    protected List<UnityAction> _actions = new();

    public void Invoke()
    {
        foreach (BaseEventListener.Listener listener in _listeners) listener.Invoke();
        foreach (UnityAction action in _actions) action.Invoke();
    }

    public void RegisterListener(BaseEventListener.Listener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(BaseEventListener.Listener listener)
    {
        _listeners.Remove(listener);
    }

    public void RegisterAction(UnityAction action)
    {
        _actions.Add(action);
    }

    public void UnregisterAction(UnityAction action)
    {
        _actions.Remove(action);
    }
}