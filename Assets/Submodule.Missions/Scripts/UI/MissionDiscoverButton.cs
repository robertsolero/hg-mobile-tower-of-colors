using System;
using UnityEngine;
using UnityEngine.UI;

namespace Submodule.Missions
{
    public class MissionDiscoverButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            CheckActiveState();
            
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnButtonPressed);
        }

        void OnButtonPressed()
        {
            var logicHandler = MissionManager.Instance.LogicHandler;
            
            if (logicHandler.IsMissionInProgress)
            {
                var progressHandler = logicHandler.CurrentProgressHandler; 
                MissionManager.Instance.UIHandler.ShowMissionDetailsUI(progressHandler.MissionData, progressHandler.MissionConditionsAtDifficulty);
            }
            else
            {
                MissionManager.Instance.UIHandler.ShowMissionListUI();
            } 
        }

        void CheckActiveState()
        {
            if (RemoteConfig.BOOL_MISSION_ENABLED)
            {
                var missionManager = MissionManager.Instance;
                var isActive = missionManager.DataHandler.HasAnyUncompletedMission() ||
                               missionManager.LogicHandler.IsMissionInProgress;
                gameObject.SetActive(isActive);
            }
        }
    }
}