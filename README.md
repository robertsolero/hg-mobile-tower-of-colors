# Tower Of Colors

## Pooling

- Pooling logic is available at `Assets/3_Scripts/Pooling`
- A Factory Singleton has also been added to keep track of Pools globally.
- You can create or get a pool by calling `Factory.Instance.GetPoolOf` 
  - if a pool for that type is available, that is returned, otherwise a new pool instance is created for you.
- From the pool object, get an item by calling `pool.GetFromPoolOrInstantiate()`
- You can create a Pool without the use of the Factory, which is only used to access the same pools between multiple MonoBehaviour.
  - call `_pool = new Pool<T>(prefab, isDoNotDestroyOnLoad, initialCapacity)` to create a pool directly.
- Pools internally use `Stack`.
- Factory internally uses `Hashtable`.
- Modifications has been done to the gameplay logic, to accomodate pooling of TowerTile:
  - `Tower`:
    - `GetFromPoolOrInstantiate()` now handle the instantiate/pool logic
    - `UnloadPreviousLevel()` is called to release the previous level Tiles to pool.
  - `TowerTile`:
    - Now inherits from `PoolableObject`
    - Uses `DestroyOrReleaseToPool()` instead of Destroy directly.
    - Overrides base class methods to initialize certain states with
- use `RemoteConfig.BOOL_POOLING_OPTIMAZATION_ENABLED` to enabled and disable it.
  
## UI Optimazation

- Even tho the UI is simple, the optimization consist in introducing `Sprite Atlases`
- They batch multiple sprites together resulting in less draw calls. 


## Missions

- Check readme here: TODO ADD LINK

## Bonus Feature: Boosters

- How doesn't like freebies?!
- Missions on Tower Color rewards boosters when completing a mission.
- When a mission is completed the reward can be claimed.
- `InventoryService` takes care of saving and retrieving userdata.
- The implemented example booster is `MoreBallsBooster` and can be found at `Assets/3_Scripts/Boosters/Datatype/MoreBallsBooster.cs`
- When the user is left with one ball, if the inventory has `MoreBallsBooster` a popup is presented for the user to choose for its usage.
- Users can dismiss or accept the offers, 5 more balls are provided and the inventory is updated.
- use `RemoteConfig.BOOL_BOOSTERS_ENABLED` to enabled and disable it.
