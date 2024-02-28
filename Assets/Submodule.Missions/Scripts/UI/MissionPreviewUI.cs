using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Submodule.Missions
{
    public class MissionPreviewUI : MonoBehaviour
    {
        [SerializeField] 
        Image missionImage;
        [SerializeField]
        TMP_Text missionName;
        [SerializeField]
        TMP_Text missionType;
        [SerializeField]
        Image missionTypePanel;

        [SerializeField] private Button button;

        private MissionData _missionData;
        private MissionConditionsAtDifficulty _conditionsAtDifficulty;

        public event Action<MissionData, MissionConditionsAtDifficulty> OnButtonPressed;

        public void Setup(MissionData missionData, MissionConditionsAtDifficulty conditionsAtDifficulty)
        {
            _missionData = missionData;
            _conditionsAtDifficulty = conditionsAtDifficulty;

            var difficultyDisplayData = missionData.GetDisplayDataOfDefault(conditionsAtDifficulty.DifficultyType); 
            
            missionName.text = missionData.MissionName;
            missionType.text = difficultyDisplayData.DifficultyName;
            missionTypePanel.color = difficultyDisplayData.BackgroundDifficultyColor;
            missionImage.sprite = missionData.MissionSprite;
            button.onClick.AddListener(ButtonPressed);
            
        }

        private void ButtonPressed()
        {
            OnButtonPressed?.Invoke(_missionData, _conditionsAtDifficulty);
        }
    }
}