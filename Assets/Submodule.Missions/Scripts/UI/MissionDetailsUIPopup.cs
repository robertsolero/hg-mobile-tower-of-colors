using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Submodule.Missions
{
    public class MissionDetailsUIPopup : MissionUIPopupBase
    {
        [SerializeField] private TMP_Text missionDescription;
        [SerializeField] private Image missionImage;
        [SerializeField] private Button playButton;

        public void Setup(MissionData missionData, MissionConditionsAtDifficulty conditionsAtDifficulty)
        {
            missionImage.sprite = missionData.MissionSprite;
            missionDescription.text = missionData.GetFormattedDescriptionBasedOnDifficulty(conditionsAtDifficulty);
            
            playButton.onClick.AddListener(() => OnPlayButtonPressed(missionData, conditionsAtDifficulty));
        }

        private void OnPlayButtonPressed(MissionData missionData, MissionConditionsAtDifficulty conditionsAtDifficulty)
        {
            MissionManager.Instance.LogicHandler.StartMission(missionData, conditionsAtDifficulty);
            Close();
        }
    }
}