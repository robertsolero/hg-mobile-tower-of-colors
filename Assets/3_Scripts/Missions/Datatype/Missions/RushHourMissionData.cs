using Submodule.Missions;
using UnityEngine;

[CreateAssetMenu(fileName = "RushHour", menuName = "Missions/RushHour", order = 1)]
public class RushHourMissionData : MissionData
{
    public override MissionProgressHandler CreateMissionProgressHandler(MissionData missionData,
        MissionConditionsAtDifficulty missionConditionsAtDifficulty)
    {
        return new RushHourMissionProgressHandler(missionData, missionConditionsAtDifficulty);
    }
}