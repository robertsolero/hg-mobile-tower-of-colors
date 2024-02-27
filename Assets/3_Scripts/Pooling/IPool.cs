using System.Collections.Generic;

public interface IPool<T> where T : IPoolableObject
{
    public T Prefab { get; set; }
    public Stack<T> PoolContainer { get; set; }
    
    public int PoolCount { get; }
    public void Populate(int amount);
    public void GetFromPoolOrInstantiate();
    public T Instantiate();
    public void AddToPool();
}