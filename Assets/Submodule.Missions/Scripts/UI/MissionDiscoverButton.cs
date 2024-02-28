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
            MissionManager.Instance.UIHandler.ShowMissionListUI();
        }

        void CheckActiveState()
        {
            gameObject.SetActive(RemoteConfig.BOOL_MISSION_ENABLED);
        }
    }
}