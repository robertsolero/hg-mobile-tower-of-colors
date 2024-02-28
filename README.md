# Tower Of Colors

## Pooling

https://github.com/robertsolero/hg-mobile-tower-of-colors/assets/15109541/995cc812-7b63-4b42-81d8-7cb33ee6e184

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

<img width="550" alt="Screenshot 2024-02-28 at 08 41 09" src="https://github.com/robertsolero/hg-mobile-tower-of-colors/assets/15109541/c3bfd6bb-f321-4145-b060-1aa2ce7b6879">

- Even tho the UI is simple, the optimization consist in introducing `Sprite Atlases`
- They batch multiple sprites together resulting in less draw calls. 

## Missions

https://github.com/robertsolero/hg-mobile-tower-of-colors/assets/15109541/0107320e-117f-42de-83ac-5d4921a0dc96

- Check in depth README with instruction and comments here: [LINK](https://github.com/robertsolero/hg-mobile-tower-of-colors/tree/main/Assets/Submodule.Missions)

## Bonus Feature: Boosters

https://github.com/robertsolero/hg-mobile-tower-of-colors/assets/15109541/0466ca74-efd3-4cc2-be2c-8cbb106f72c7

- How doesn't like freebies?! 
- Missions on Tower Color rewards boosters when completing a mission.
- When a mission is completed the reward can be claimed.
- `InventoryService` takes care of saving and retrieving userdata.
- The implemented example booster is `MoreBallsBooster` and can be found at `Assets/3_Scripts/Boosters/Datatype/MoreBallsBooster.cs`
- When the user is left with one ball, if the inventory has `MoreBallsBooster` a popup is presented for the user to choose for its usage.
- Users can dismiss or accept the offers, 5 more balls are provided and the inventory is updated.
- use `RemoteConfig.BOOL_BOOSTERS_ENABLED` to enabled and disable it.
