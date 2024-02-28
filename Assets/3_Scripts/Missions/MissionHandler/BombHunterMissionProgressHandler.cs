using Submodule.Missions;

public class BombHunterMissionProgressHandler : MissionProgressHandler
{
    public BombHunterMissionProgressHandler(MissionData missionData, MissionConditionsAtDifficulty missionConditionsAtDifficulty) : base(missionData, missionConditionsAtDifficulty)
    {
    }

    public override void Start()
    {
        GameManager.OnNewManagerLoaded += SubscribeToTileDestroyed;// used to handle scene loading and a different manager spawned
        SubscribeToTileDestroyed(GameManager.Instance);
    }

    private void SubscribeToTileDestroyed(GameManager manager) 
    {
        manager.Tower.OnTileDestroyedCallback += OnTileDestroyed;
    }

    private void OnTileDestroyed(TowerTile tile)
    {
        if (tile is ExplodingTile){
            UpdateProgress(CurrentProgress + 1);
        }
    }
}