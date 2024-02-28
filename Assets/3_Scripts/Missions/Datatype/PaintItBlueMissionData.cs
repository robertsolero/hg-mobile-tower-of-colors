using System.Collections.Generic;
using Submodule.Missions;
using UnityEngine;

[CreateAssetMenu(fileName = "PaintItBlue", menuName = "Missions/PaintItBlue", order = 1)]
public class PaintItBlueMissionData : MissionData
{
    [SerializeField]
    private List<Color> blueColors = new List<Color>();
    public List<Color> BlueColors => blueColors; // manually inserted because project doesn't map colors with enum or type.
    
    public override MissionProgressHandler CreateMissionProgressHandler(MissionData missionData,
        MissionConditionsAtDifficulty missionConditionsAtDifficulty)
    {
        return new PaintItBlueMissionProgressHandler(missionData, missionConditionsAtDifficulty);
    }
}