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
            playButton.onClick.AddListener(OnPlayButtonPressed);
        }

        private void OnPlayButtonPressed()
        {
            Close();
        }
    }
}