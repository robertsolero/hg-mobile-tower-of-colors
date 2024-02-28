using System;
using UnityEngine;

namespace Submodule.Missions
{
    public class MissionLogicHandler
    {
        public bool IsMissionInProgress => CurrentProgressHandler != null;
        public MissionProgressHandler CurrentProgressHandler { get; private set; }
        
        public Action<MissionProgressHandler> OnMissionProgressChanged; // CurrentProgress, MissionRequirement

        public Action OnMissionCompleted;
        
        public Action<MissionProgressHandler> OnMissionStarted;

        public void StartMission(MissionData missionData, MissionConditionsAtDifficulty missionConditionsAtDifficulty)
        {
            if (IsMissionInProgress)
            {
                Debug.LogError($"mission {missionData.MissionID} is currently in progress");
                return;
            }
            
            Debug.Log($"Start mission {missionData.MissionID}");
            
            CurrentProgressHandler = missionData.CreateMissionProgressHandler(missionData, missionConditionsAtDifficulty);
            CurrentProgressHandler.Start();
            CurrentProgressHandler.OnCompleted += OnMissionCompleted;
            CurrentProgressHandler.OnCompleted += OnCurrentMissionCompleted;
            CurrentProgressHandler.OnProgressChanged += OnProgressChanged;
            OnMissionStarted?.Invoke(CurrentProgressHandler);
        }

        private void OnProgressChanged(int currentValue, int requiredValue)
        {
            OnMissionProgressChanged?.Invoke(CurrentProgressHandler);
        }

        private void OnCurrentMissionCompleted()
        {
            CurrentProgressHandler = null;
        }
    }
}