using System;
using UnityEngine;

public class PoolableObject : MonoBehaviour, IPoolableObject
{
    public Action<IPoolableObject> OnAddBackToPoolRequested { get; set; }
    public Action<IPoolableObject> OnRemoveFromPoolRequested { get; set; }

    public void OnInstantiatedOnPool()
    {
        SetObjectActiveState(false);
    }

    public void OnRetrievedFromPool()
    {
        SetObjectActiveState(true);
    }

    public void AddedBackToPool()
    {
        SetObjectActiveState(false);
        OnAddBackToPoolRequested?.Invoke(this);
    }

    protected void SetObjectActiveState(bool active)
    {
        gameObject.SetActive(active);
    }

    private void OnDestroy()
    {
        OnRemoveFromPoolRequested?.Invoke(this);
    }
}