using System.Collections.Generic;
using UnityEngine;

public interface IPool<T> where T : IPoolableObject
{
    public T Prefab { get; set; }
    public bool IsDoNotDestroyOnLoad { get; set; }
    public Stack<T> PoolContainer { get; set; }
    public int PoolCount { get; }
    public void Populate(int amount);
    public T GetFromPoolOrInstantiate();
    public T Instantiate();
    public void AddToPool(T item);
}