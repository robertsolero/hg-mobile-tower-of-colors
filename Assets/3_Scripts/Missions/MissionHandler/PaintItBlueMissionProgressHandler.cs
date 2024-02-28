
using System.Collections.Generic;
using System.Linq;
using Submodule.Missions;
using UnityEngine;

public class PaintItBlueMissionProgressHandler : MissionProgressHandler
{
    public PaintItBlueMissionProgressHandler(MissionData missionData, MissionConditionsAtDifficulty missionConditionsAtDifficulty) 
        : base(missionData, missionConditionsAtDifficulty)
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
        var mat = tile.Renderer.sharedMaterial;
        var mission = MissionData as PaintItBlueMissionData;
        if (mission == null)
        {
            return;
        }

        if (mission.BlueColors.Any(col => col == mat.color))
        {
            UpdateProgress(CurrentProgress + 1);
        }
    }
}