using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : IPool<T> where T : MonoBehaviour, IPoolableObject
{
    public T Prefab { get; set; }
    public Stack<T> PoolContainer { get; set; }

    public int PoolCount => PoolContainer == null ? 0 : PoolContainer.Count;

    public void Populate(int amount)
    {
        if (PoolContainer == null)
            PoolContainer = new Stack<T>();

        
    }

    public void GetFromPoolOrInstantiate()
    {
        throw new System.NotImplementedException();
    }

    public T Instantiate()
    {
        throw new System.NotImplementedException();
    }

    public void AddToPool()
    {
        throw new System.NotImplementedException();
    }
}