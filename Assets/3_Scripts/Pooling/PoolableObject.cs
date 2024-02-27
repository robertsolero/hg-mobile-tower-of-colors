using System;
using UnityEngine;

public class PoolableObject : MonoBehaviour, IPoolableObject
{
    public Transform Parent { get; set; }
    public bool IsDoNotDestroyOnLoad { get; set; }
    public Action<IPoolableObject> OnAddBackToPoolRequested { get; set; }
    public Action<IPoolableObject> OnRemoveFromPoolRequested { get; set; }

    public virtual void OnInstantiatedOnPool()
    {
        SetObjectActiveState(false);

        if (IsDoNotDestroyOnLoad) 
            DontDestroyOnLoad(gameObject);
    }

    public virtual void OnRetrievedFromPool()
    {
        SetObjectActiveState(true);
    }

    public virtual void AddedBackToPool()
    {
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