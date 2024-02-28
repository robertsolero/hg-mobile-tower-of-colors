
using Submodule.Missions;
using UnityEngine;

[CreateAssetMenu(fileName = "BombHunter", menuName = "Missions/BombHunter", order = 1)]
public class BombHunterMissionData : MissionData
{
    public override MissionProgressHandler CreateMissionProgressHandler(MissionData missionData,
        MissionConditionsAtDifficulty missionConditionsAtDifficulty)
    {
        return new BombHunterMissionProgressHandler(missionData, missionConditionsAtDifficulty);
    }
}