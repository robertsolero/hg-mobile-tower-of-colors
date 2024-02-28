using System;
using UnityEngine;

namespace Submodule.Missions
{
    public class MissionLogicHandler
    {
        public bool IsMissionInProgress => CurrentProgressHandler != null;
        public MissionProgressHandler CurrentProgressHandler { get; private set; }
        
        public Action<MissionProgressHandler> OnMissionProgressChanged;

        public Action OnMissionCompleted;
        
        public Action<MissionProgressHandler> OnMissionStarted;

        public Action OnMissionDisposed;
        
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
            CurrentProgressHandler.OnProgressChanged += OnProgressChanged;
            OnMissionStarted?.Invoke(CurrentProgressHandler);
        }

        private void OnProgressChanged(int currentValue, int requiredValue)
        {
            OnMissionProgressChanged?.Invoke(CurrentProgressHandler);
        }
        
        public void ResetLastMissionCompleted()
        {
            CurrentProgressHandler = null;
            OnMissionDisposed?.Invoke();
        }
    }
}