using Submodule.Missions;
using UnityEngine.SceneManagement;

public class RushHourMissionProgressHandler : MissionProgressHandler
{
    public RushHourMissionProgressHandler(MissionData missionData, MissionConditionsAtDifficulty missionConditionsAtDifficulty) 
        : base(missionData, missionConditionsAtDifficulty)
    {
    }

    public override void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameManager.OnNewManagerLoaded += SubscribeToLevelWonAndLost;// used to handle scene loading and a different manager spawned
        SubscribeToLevelWonAndLost(GameManager.Instance);
    }

    

    private void SubscribeToLevelWonAndLost(GameManager manager) 
    {
        //manager.Tower.OnTileDestroyedCallback += OnTileDestroyed;
        manager.OnLevelWon += OnLevelWon;
        manager.OnLevelLost += OnLevelLost;
    }

    //update this first, then CurrentProgress on scene loaded, so we get a better UX to show completed mission on level start.
    int _internalProgress;

    private void OnLevelLost()
    {
        _internalProgress = 0;
    }

    private void OnLevelWon()
    {
        _internalProgress++;
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        UpdateProgress(_internalProgress);
    }
}