using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Submodule.Missions
{
    public class MissionCompletedPopup : MissionUIPopupBase
    {
        [SerializeField] 
        private Button claimButton;
        [SerializeField] 
        private MissionRewardUI rewardUI;
        [SerializeField]
        private TMP_Text claimInfoText;
        
        public void Setup(MissionData missionData, MissionConditionsAtDifficulty conditionsAtDifficulty)
        {
            conditionsAtDifficulty.MissionReward.AddRewardToInventory(conditionsAtDifficulty.RewardAmount);
            
            rewardUI.Setup(conditionsAtDifficulty.MissionReward, conditionsAtDifficulty.RewardAmount);
            claimButton.onClick.AddListener(OnClaimButtonPressed);

            claimInfoText.text = string.Format(claimInfoText.text, conditionsAtDifficulty.MissionReward.RewardName);
        }

        private void OnClaimButtonPressed()
        {
            Close();
        }

        private void OnDestroy()
        {
            MissionManager.Instance.LogicHandler.ResetLastMissionCompleted();
        }
    }
}