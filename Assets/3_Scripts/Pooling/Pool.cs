using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : IPool<T> where T : MonoBehaviour, IPoolableObject
{
    public Pool(T prefab, Transform parent, int initialCapacity)
    {
        Prefab = prefab;
        Parent = parent;
        Populate(initialCapacity);
    }

    public T Prefab { get; set; }
    public Transform Parent { get; set; }
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
        var item = Object.Instantiate(Prefab, Parent);
        item.OnInstantiatedOnPool();
        item.OnAddBackToPoolRequested += OnAddBackToPoolRequested;
        item.OnRemoveFromPoolRequested += OnRemoveFromPoolRequested;
        return item;
    }

    public void AddToPool(T item)
    {
        PoolContainer?.Push(item);
    }
    
    private void OnRemoveFromPoolRequested(IPoolableObject item)
    {
        PoolContainer?.Pop();
    }

    private void OnAddBackToPoolRequested(IPoolableObject item)
    {
        AddToPool(item as T);
    }
}