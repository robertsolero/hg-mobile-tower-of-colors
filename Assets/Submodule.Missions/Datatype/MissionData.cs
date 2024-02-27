using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "Missions/MissionData", order = 1)]
public class MissionData : ScriptableObject
{
    [SerializeField]
    string missionID;
    public string MissionID => missionID;
    
    [SerializeField]
    string missionName;
    public string MissionName => missionName;
        
    [SerializeField]
    string descriptionFormat;
    public string DescriptionFormatFormat => descriptionFormat;
    
    [SerializeField]
    Sprite missionSprite;
    public Sprite MissionSprite => missionSprite;

    [SerializeField]
    private List<MissionConditionsAtDifficulty> thresholdAtDifficulty = new List<MissionConditionsAtDifficulty>();

    public List<MissionConditionsAtDifficulty> ThresholdAtDifficulty => thresholdAtDifficulty;

    private string CompletedMissionUserdataKey => "Mission.{0}.{1}.Completed";

    public bool TryGetNextUncompletedMissionType(out MissionDifficultyType type)
    {
        type = MissionDifficultyType.Easy;
        var found = false;
        
        foreach (var difficulty in thresholdAtDifficulty)
        {
            if (!IsMissionAtDifficultyLevelCompleted(difficulty.DifficultyType))
            {
                type = difficulty.DifficultyType;
                found = true;
                break;
            }
        }

        return found;
    }
    
    
    public bool IsMissionAtDifficultyLevelCompleted(MissionDifficultyType type)
    {
        var key = string.Format(CompletedMissionUserdataKey, MissionID, type.ToString());
        return PlayerPrefs.GetInt(key) == 1;
    }
    
    public void SetMissionAtDifficultyLevelCompleted(MissionDifficultyType type)
    {
        var key = string.Format(CompletedMissionUserdataKey, MissionID, type.ToString());
        PlayerPrefs.SetInt(key,1);
    } 
}
