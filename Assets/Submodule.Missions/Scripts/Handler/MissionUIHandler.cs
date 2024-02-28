using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Submodule.Missions
{
    public class MissionUIHandler : MonoBehaviour
    {
        [FormerlySerializedAs("missionsListUIPopupPrefab")] [SerializeField]
        MissionListUIPopup missionListUIPopupPrefab;
        [SerializeField] 
        MissionDetailsUIPopup missionsDetailsUIPopupPrefab;
        [SerializeField]
        MissionProgressUIWidget missionsWidgetPrefab;
        [SerializeField]
        MissionCompletedPopup missionCompletedPopupPrefab;


        [SerializeField]
        private string uiParentScenePath = "GameManager/UI";
        
        private Transform uiParent;
        private Transform UIParent
        {
            get
            {
                if (uiParent != null)
                    return uiParent;

                var canvasObject = GameObject.Find(uiParentScenePath); // ugly, because of the scene reload structure. 
                if (canvasObject == null)
                    Debug.LogError("Missions submodule Requires a Canvas");

                return canvasObject == null ? null : uiParent = canvasObject.transform;
            }
        }

        private MissionProgressUIWidget _widget;

        public void Initialize()
        {
            SceneManager.sceneLoaded += (scene, mode) => TrySpawnWidget();
            
            MissionManager.Instance.LogicHandler.OnMissionStarted += OnMissionStarted;
            MissionManager.Instance.LogicHandler.OnMissionProgressChanged += OnMissionProgressChanged;
            MissionManager.Instance.LogicHandler.OnMissionCompleted += OnMissionCompleted;
            MissionManager.Instance.LogicHandler.OnMissionDisposed += OnMissionDisposed;
        }

        void TrySpawnWidget()
        {
            _widget = Instantiate(missionsWidgetPrefab, UIParent);
        }
        
        private void OnMissionStarted(MissionProgressHandler progressHandler)
        {
            if (_widget)
                _widget.OnMissionStarted(progressHandler);
        }

        private void OnMissionProgressChanged(MissionProgressHandler progressHandler)
        {
            if (_widget)
                _widget.OnMissionProgressChanged(progressHandler);
        }

        private void OnMissionCompleted()
        {
            if (_widget)
                _widget.OnMissionCompleted();

            ShowMissionCompletedUI();
        }
        private void OnMissionDisposed()
        {
            if (_widget)
                _widget.OnMissionDisposed();
        }

        

        public void ShowMissionListUI()
        {
            var ui = Instantiate(missionListUIPopupPrefab, UIParent);
            ui.Setup(MissionManager.Instance.DataHandler.MissionDataList);
            ui.Show();
        }
        
        public void ShowMissionDetailsUI(MissionData missionData, MissionConditionsAtDifficulty missionConditionsAtDifficulty)
        {
            var ui = Instantiate(missionsDetailsUIPopupPrefab, UIParent);
            ui.Setup(missionData, missionConditionsAtDifficulty);
            ui.Show();
        }
        
        public void ShowMissionCompletedUI()
        {
            var missionData = MissionManager.Instance.LogicHandler.CurrentProgressHandler.MissionData;
            var missionConditionsAtDifficulty = MissionManager.Instance.LogicHandler.CurrentProgressHandler.MissionConditionsAtDifficulty;
            var ui = Instantiate(missionCompletedPopupPrefab, UIParent);
            ui.Setup(missionData, missionConditionsAtDifficulty);
            ui.Show();
        }
    }
}