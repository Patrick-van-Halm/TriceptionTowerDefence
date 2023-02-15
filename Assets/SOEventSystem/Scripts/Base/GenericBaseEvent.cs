using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GenericBaseEvent<T> : ScriptableObject
{
    protected List<GenericBaseEventListener<T>.Listener> _listeners = new();
    protected List<UnityAction<T>> _actions = new();

    public virtual void Invoke(T T1)
    {
        foreach (GenericBaseEventListener<T>.Listener listener in _listeners) listener.Invoke(T1);
        foreach (UnityAction<T> action in _actions) action.Invoke(T1);
    }

    public void RegisterListener(GenericBaseEventListener<T>.Listener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(GenericBaseEventListener<T>.Listener listener)
    {
        _listeners.Remove(listener);
    }

    public void RegisterAction(UnityAction<T> action)
    {
        _actions.Add(action);
    }

    public void UnregisterAction(UnityAction<T> action)
    {
        _actions.Remove(action);
    }
}

public abstract class GenericBaseEvent<T1, T2> : ScriptableObject
{
    protected List<GenericBaseEventListener<T1, T2>.Listener> _listeners = new();
    protected List<UnityAction<T1, T2>> _actions = new();

    public virtual void Invoke(T1 T1, T2 T2)
    {
        foreach (GenericBaseEventListener<T1, T2>.Listener listener in _listeners) listener.Invoke(T1, T2);
        foreach (UnityAction<T1, T2> action in _actions) action.Invoke(T1, T2);
    }

    public void RegisterListener(GenericBaseEventListener<T1, T2>.Listener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(GenericBaseEventListener<T1, T2>.Listener listener)
    {
        _listeners.Remove(listener);
    }

    public void RegisterAction(UnityAction<T1, T2> action)
    {
        _actions.Add(action);
    }

    public void UnregisterAction(UnityAction<T1, T2> action)
    {
        _actions.Remove(action);
    }
}