using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Submodule.Missions
{
    [Serializable]
    public class MissionConditionsAtDifficulty
    {
        [SerializeField] MissionDifficultyType difficultyType;
        public MissionDifficultyType DifficultyType => difficultyType;

        [SerializeField] int missionRequirement;
        public int MissionRequirement => missionRequirement;

        [SerializeField] int rewardAmount;
        public int RewardAmount => rewardAmount;

        [SerializeField] private MissionReward missionReward;
        public MissionReward MissionReward => missionReward;
    }
}