using System;
using UnityEngine;

namespace Submodule.Missions
{
    public class MissionLogicHandler
    {
        public bool IsMissionInProgress => CurrentProgressHandler != null;
        public MissionProgressHandler CurrentProgressHandler { get; private set; }
        
        public Action<int, int> OnMissionProgressChanged; // CurrentProgress, MissionRequirement

        public Action OnMissionCompleted;

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
            CurrentProgressHandler.OnCompleted += OnCurrentMissionCompleted;
            CurrentProgressHandler.OnCompleted += OnMissionCompleted;
            CurrentProgressHandler.OnProgressChanged += OnMissionProgressChanged;
        }

        private void OnCurrentMissionCompleted()
        {
            CurrentProgressHandler = null;
        }
    }
}