using System.Collections.Generic;
using Submodule.Missions;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class MissionData : ScriptableObject
{
    public abstract MissionProgressHandler CreateMissionProgressHandler(MissionData missionData, MissionConditionsAtDifficulty missionConditionsAtDifficulty);
    
    [SerializeField]
    string missionID;
    public string MissionID => missionID;
    
    [SerializeField]
    string missionName;
    public string MissionName => missionName;
        
    [SerializeField]
    string descriptionFormat;
    public string GetFormattedDescriptionBasedOnDifficulty(MissionConditionsAtDifficulty conditionAtDifficulty)
    {
        return string.Format(descriptionFormat, conditionAtDifficulty.MissionRequirement);
    }
    
    [SerializeField]
    Sprite missionSprite;
    public Sprite MissionSprite => missionSprite;

    [SerializeField] 
    private List<MissionDifficultyDisplayData> difficultyDisplayData = new List<MissionDifficultyDisplayData>();
    private List<MissionDifficultyDisplayData> DifficultyDisplayData => difficultyDisplayData;

    public MissionDifficultyDisplayData GetDisplayDataOfDefault(MissionDifficultyType difficultyType)
    {
        var foundDisplayData = DifficultyDisplayData.Find(displayData => displayData.DifficultyType == difficultyType);
        return foundDisplayData != null ? foundDisplayData : null;
    }

    
    [SerializeField]
    private List<MissionConditionsAtDifficulty> conditionsAtDifficulty = new List<MissionConditionsAtDifficulty>();
    public List<MissionConditionsAtDifficulty> ConditionsAtDifficulty => conditionsAtDifficulty;

    private string CompletedMissionUserdataKey => "Mission.{0}.{1}.Completed";
    
    public bool TryGetMissionConditionsOfType(MissionDifficultyType type, out MissionConditionsAtDifficulty conditions)
    {
        conditions = null;
        
        foreach (var difficulty in conditionsAtDifficulty)
        {
            if (difficulty.DifficultyType == type)
            {
                conditions = difficulty;
                break;
            }
        }

        return conditions != null;
    }
    
    public bool TryGetNextUncompletedMissionConditions(out MissionConditionsAtDifficulty conditions)
    {
        conditions = null;
        
        foreach (var difficulty in conditionsAtDifficulty)
        {
            if (!IsMissionAtDifficultyLevelCompleted(difficulty.DifficultyType))
            {
                conditions = difficulty;
                break;
            }
        }

        return conditions != null;
    }
    
    
    public bool IsMissionAtDifficultyLevelCompleted(MissionDifficultyType type)
    {
        var key = string.Format(CompletedMissionUserdataKey, MissionID, type.ToString());
        return PlayerPrefs.GetInt(key) == 1;
    }
    
    public void SetMissionAtDifficultyLevelCompleted(MissionConditionsAtDifficulty conditions)
    {
        var key = string.Format(CompletedMissionUserdataKey, MissionID, conditions.DifficultyType.ToString());
        PlayerPrefs.SetInt(key,1);
    } 
}
