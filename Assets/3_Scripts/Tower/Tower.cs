﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    [Header("Tower Settings")]
    public float TileHeight = 1.2f;
    public float TileRadius = 0.5f;
    public int TileCountPerFloor = 15;
    public int FloorCount = 15;
    public int PlayableFloors = 8;
    public float SpecialTileChance = 0.1f;
    public TowerTile TilePrefab;
    public TowerTile[] SpecialTilePrefabs;
    public bool BuildOnStart = true;

    [Header("Scene")]
    public Transform CameraTarget;

    private List<List<TowerTile>> tilesByFloor;
    private int currentFloor = -1;
    private int maxFloor = 0;

    public System.Action<TowerTile> OnTileDestroyedCallback;

    private void Start()
    {
        if (BuildOnStart) {
            BuildTower();
        }
        
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public float CaculateTowerRadius(float sideLength, float sideCount)
    {
        return sideLength / (2 * Mathf.Sin(Mathf.Deg2Rad * (180.0f / sideCount)));
    }

    public void BuildTower()
    {
        ResetTower();
        tilesByFloor = new List<List<TowerTile>>();
        float towerRadius = CaculateTowerRadius(TileRadius * 2, TileCountPerFloor);
        float angleStep = 360.0f / TileCountPerFloor;
        Quaternion floorRotation = transform.rotation;
        for (int y = 0; y < FloorCount; y++) {
            tilesByFloor.Add(new List<TowerTile>());
            for (int i = 0; i < TileCountPerFloor; i++) {
                Quaternion direction = Quaternion.AngleAxis(angleStep * i, Vector3.up) * floorRotation;
                Vector3 position = transform.position + Vector3.up * y * TileHeight + direction * Vector3.forward * towerRadius;
                TowerTile tileInstance = GetFromPoolOrInstantiate(position, direction);
                tileInstance.SetColorIndex(Mathf.FloorToInt(Random.value * TileColorManager.Instance.ColorCount));
                tileInstance.SetFreezed(true);
                tileInstance.Floor = y;
                tileInstance.Tower = this;
                tilesByFloor[y].Add(tileInstance);
            }
            floorRotation *= Quaternion.AngleAxis(angleStep / 2.0f, Vector3.up);
        }
        maxFloor = FloorCount - 1;

        SetCurrentFloor(tilesByFloor.Count - PlayableFloors);
        for (int i = 1; i < PlayableFloors; i++) {
            SetFloorActive(currentFloor + i, true);
        }
    }

    TowerTile GetFromPoolOrInstantiate(Vector3 position, Quaternion direction)
    {
        var isDefaultTile = Random.value > SpecialTileChance;
        var prefab = isDefaultTile
            ? TilePrefab
            : SpecialTilePrefabs[Random.Range(0, SpecialTilePrefabs.Length)];

        var initialRotation = direction * TilePrefab.transform.rotation;
        
        if (RemoteConfig.BOOL_POOLING_OPTIMAZATION_ENABLED)
        {
            var initialCapacity = isDefaultTile ? 200 : 30;
            var pool = Factory.Instance.GetPoolOf(prefab, true, initialCapacity);
            var item = pool.GetFromPoolOrInstantiate();
            item.transform.position = position;
            item.transform.rotation = initialRotation;
            return item;
        }

        return Instantiate(prefab, position, initialRotation, transform);
    }


    public void OnTileDestroyed(TowerTile tile)
    {
        OnTileDestroyedCallback?.Invoke(tile);
        
        if (maxFloor > PlayableFloors - 1 && tilesByFloor != null) {
            float checkHeight = (maxFloor - 1) * TileHeight + TileHeight * 0.9f;
            float maxHeight = 0;
            foreach (List<TowerTile> floor in tilesByFloor) {
                foreach (TowerTile t in floor) {
                    if (t != null)
                    {
                        if (!t.isActiveAndEnabled)
                            continue;
                        
                        maxHeight = Mathf.Max(t.transform.position.y, maxHeight);
                    }
                }
            }

            if (maxHeight < checkHeight) {
                maxFloor--;
                if (currentFloor > 0) {
                    SetCurrentFloor(currentFloor - 1);
                }
            }
        }
    }
    
    private void OnSceneUnloaded(Scene arg0)
    {
        if (!RemoteConfig.BOOL_POOLING_OPTIMAZATION_ENABLED)
            return;
        
        foreach (List<TowerTile> tileList in tilesByFloor)
        {
            foreach (TowerTile tile in tileList) 
                tile.DestroyOrReleaseToPool();
        }
    }

    public void ResetTower()
    {
        if (tilesByFloor != null) {
            foreach (List<TowerTile> tileList in tilesByFloor) {
                foreach (TowerTile tile in tileList) {
                    if (Application.isPlaying)
                    {
                        tile.DestroyOrReleaseToPool();
                    }
                    else
                    {
                        DestroyImmediate(tile.gameObject);
                    }
                }
                tileList.Clear();
            }
            tilesByFloor.Clear();
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence()
    {
        for (int i = 0; i < tilesByFloor.Count - PlayableFloors; i++) {
            yield return new WaitForSeconds(0.075f * Time.timeScale);
            SetFloorActive(i, false, false);
        }
        yield return null;
    }

    public void SetCurrentFloor(int floor)
    {
        currentFloor = floor;
        CameraTarget.position = transform.position + Vector3.up * floor * TileHeight;
        SetFloorActive(currentFloor, true);
    }

    public void SetFloorActive(int floor, bool value, bool setFreezed = true)
    {
        foreach (TowerTile tile in tilesByFloor[floor]) {
            if (tile && tile.isActiveAndEnabled) {
                tile.SetEnabled(value);
                if (setFreezed)
                    tile.SetFreezed(!value);
            }
        }
    }
}
