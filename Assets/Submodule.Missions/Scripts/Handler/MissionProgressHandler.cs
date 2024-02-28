using System;
using UnityEngine;

namespace Submodule.Missions
{
    public abstract class MissionProgressHandler
    {
        public readonly MissionData MissionData;
        public readonly MissionConditionsAtDifficulty MissionConditionsAtDifficulty;
        public int CurrentProgress { get; protected set; }
        public virtual int MissionRequirement => MissionConditionsAtDifficulty.MissionRequirement;

        public MissionProgressHandler(MissionData missionData, MissionConditionsAtDifficulty missionConditionsAtDifficulty)
        {
            MissionData = missionData;
            MissionConditionsAtDifficulty = missionConditionsAtDifficulty;
        }
        
        public abstract void Start();

        protected void UpdateProgress(int newProgress)
        {
            if (IsCompleted)
                return;
            
            CurrentProgress = newProgress;
            CurrentProgress = Mathf.Clamp(CurrentProgress, 0, MissionRequirement);
            
            if (!IsCompleted)
                ProgressChanged();
            else 
                SetAsCompleted();
        }

        public virtual bool IsCompleted => CurrentProgress >= MissionConditionsAtDifficulty.MissionRequirement;

        public virtual void SetAsCompleted()
        {
            Debug.Log($"Mission {MissionData.MissionID} completed");
            MissionData.SetMissionAtDifficultyLevelCompleted(MissionConditionsAtDifficulty);
            OnCompleted?.Invoke();
        }

        protected void ProgressChanged() => OnProgressChanged?.Invoke(CurrentProgress, MissionRequirement);
        
        public Action OnCompleted;
        public Action<int, int> OnProgressChanged;
    }
}