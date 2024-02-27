using System;
using UnityEngine;

public interface IPoolableObject
{ 
    public bool IsDoNotDestroyOnLoad { get; set; }
    public Action<IPoolableObject> OnAddBackToPoolRequested { get; set; }
    public Action<IPoolableObject> OnRemoveFromPoolRequested { get; set; }
    public void OnInstantiatedOnPool();
    public void OnRetrievedFromPool();
    public void AddedBackToPool();
}