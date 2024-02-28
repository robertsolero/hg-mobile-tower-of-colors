using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Submodule.Missions
{
    public class MissionDetailsUIPopup : MissionUIPopupBase
    {
        [SerializeField] private TMP_Text missionName;
        [SerializeField] private TMP_Text missionDescription;
        [SerializeField] private Image missionImage;
        [SerializeField] private Button playButton;
        [SerializeField] private TMP_Text playButtonText;

        public void Setup(MissionData missionData, MissionConditionsAtDifficulty conditionsAtDifficulty)
        {
            missionImage.sprite = missionData.MissionSprite;
            missionName.text = missionData.MissionName;
            missionDescription.text = missionData.GetFormattedDescriptionBasedOnDifficulty(conditionsAtDifficulty);
            
            var isPlayingMission = MissionManager.Instance.LogicHandler.IsMissionInProgress;
            
            playButtonText.text = isPlayingMission ? "Continue" : "Play";
            playButton.onClick.AddListener(() => OnPlayButtonPressed(missionData, conditionsAtDifficulty, isPlayingMission));
        }

        private void OnPlayButtonPressed(MissionData missionData, MissionConditionsAtDifficulty conditionsAtDifficulty, bool isPlayingMission)
        {
            Close();
            
            if (!isPlayingMission)
                MissionManager.Instance.LogicHandler.StartMission(missionData, conditionsAtDifficulty);
        }
    }
}