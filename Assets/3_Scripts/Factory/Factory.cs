
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Factory : MonoBehaviour
{
    private static Factory _instance;
    public static Factory Instance //TODO improve this a simplified version of a Singleton
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("Factory");
                return _instance = go.AddComponent<Factory>();
            }

            return _instance;
        }
    }

    public Hashtable _poolCollections;

    public void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Pool<T> GetPoolOf<T>(T prefab, bool isDoNotDestroyOnLoad, int initialCapacity ) where T : PoolableObject
    {
        if (_poolCollections == null)
            _poolCollections = new Hashtable();

        if (_poolCollections.Contains(prefab))
        {
            //TODO add check for argument value changed from cached.
            return _poolCollections[prefab] as Pool<T>;
        }

        var pool = new Pool<T>(prefab, isDoNotDestroyOnLoad, initialCapacity);
        _poolCollections.Add(prefab, pool);
        
        // Debug.Log($"Pools count: {_poolCollections.Count}");
        // Debug.Log($"Name: {prefab.name} Hash: {prefab.GetHashCode()} count: {pool.PoolCount}");

        return pool;
    }
}