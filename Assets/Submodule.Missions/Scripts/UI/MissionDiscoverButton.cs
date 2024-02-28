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
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnButtonPressed);
        }

        void OnButtonPressed()
        {
            MissionManager.Instance.UIHandler.ShowMissionListUI();
        }
    }
}