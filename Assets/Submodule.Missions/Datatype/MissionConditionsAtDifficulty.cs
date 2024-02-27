using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public class MissionConditionsAtDifficulty
{
    [SerializeField] 
    MissionDifficultyType difficultyType;
    public MissionDifficultyType DifficultyType => difficultyType;
    
    [SerializeField] 
    Vector2Int minMaxMissionThreshold;
    public int GetMissionThreshold => Random.Range(minMaxMissionThreshold.x, minMaxMissionThreshold.y);
    
    [SerializeField] 
    int rewardAmount;
    public int RewardAmount => rewardAmount;

    [SerializeField] 
    private MissionReward missionReward;
    public MissionReward MissionReward => missionReward;
}
