using System;

public interface IPoolableObject
{
    public Action<IPoolableObject> OnAddBackToPoolRequested { get; set; }
    public Action<IPoolableObject> OnRemoveFromPoolRequested { get; set; }
    public void OnInstantiatedOnPool();
    public void OnRetrievedFromPool();
    public void AddedBackToPool();
}