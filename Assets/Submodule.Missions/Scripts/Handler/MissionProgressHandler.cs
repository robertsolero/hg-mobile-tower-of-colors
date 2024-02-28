using System;
using UnityEngine;

namespace Submodule.Missions
{
    public abstract class MissionProgressHandler
    {
        protected MissionData _missionData;
        protected MissionConditionsAtDifficulty _missionConditionsAtDifficulty;
        public int CurrentProgress { get; protected set; }
        public virtual int MissionRequirement => _missionConditionsAtDifficulty.MissionRequirement;

        public MissionProgressHandler(MissionData missionData, MissionConditionsAtDifficulty missionConditionsAtDifficulty)
        {
            _missionData = missionData;
            _missionConditionsAtDifficulty = missionConditionsAtDifficulty;
        }
        
        public abstract void Start();

        protected void UpdateProgress(int newProgress)
        {
            CurrentProgress = newProgress;
            CurrentProgress = Mathf.Clamp(CurrentProgress, 0, MissionRequirement);
            ProgressChanged();
            
            Debug.Log($"Mission Progress on {_missionData.MissionID} is {CurrentProgress}/{MissionRequirement}");
            
            if (IsCompleted)
                SetAsCompleted();
        }

        public virtual bool IsCompleted => CurrentProgress >= _missionConditionsAtDifficulty.MissionRequirement;

        public virtual void SetAsCompleted()
        {
            Debug.Log($"Mission {_missionData.MissionID} completed");
            _missionData.SetMissionAtDifficultyLevelCompleted(_missionConditionsAtDifficulty);
            OnCompleted?.Invoke();
        }

        protected void ProgressChanged() => OnProgressChanged?.Invoke(CurrentProgress, MissionRequirement);
        
        public Action OnCompleted;
        public Action<int, int> OnProgressChanged;
    }
}