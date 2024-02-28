using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : IPool<T> where T : MonoBehaviour, IPoolableObject
{
    public Pool(T prefab, bool isDoNotDestroyOnLoad, int initialCapacity)
    {
        Prefab = prefab;
        IsDoNotDestroyOnLoad = isDoNotDestroyOnLoad;
        Populate(initialCapacity);
    }

    public T Prefab { get; set; }
    public bool IsDoNotDestroyOnLoad { get; set; }
    public Stack<T> PoolContainer { get; set; }
    public int PoolCount => PoolContainer == null ? 0 : PoolContainer.Count;

    public void Populate(int amount)
    {
        if (PoolContainer == null)
            PoolContainer = new Stack<T>();

        for (var i = 0; i < amount; i++)
        {
            var item = Instantiate();
            AddToPool(item);
        }
    }

    public T GetFromPoolOrInstantiate()
    {
        T item;
        if (PoolCount > 0)
            item = PoolContainer.Pop();
        else 
            item = Instantiate();
        
        item.OnRetrievedFromPool();
        return item;
    }

    public T Instantiate()
    {
        var item = Object.Instantiate(Prefab);
        item.IsDoNotDestroyOnLoad = IsDoNotDestroyOnLoad;
        item.OnInstantiatedOnPool();
        item.OnAddBackToPoolRequested += OnAddBackToPoolRequested;
        item.OnRemoveFromPoolRequested += OnRemoveFromPoolRequested;
        return item;
    }

    public void AddToPool(T item)
    {
        if (!PoolContainer.Contains(item))
            PoolContainer?.Push(item);
    }
    
    private void OnRemoveFromPoolRequested(IPoolableObject item)
    {
        if (PoolContainer == null)
            return;
        
        if (PoolContainer.Contains((T)item))
            PoolContainer.Pop();
    }

    private void OnAddBackToPoolRequested(IPoolableObject item)
    {
        AddToPool(item as T);
    }
}