using System.Collections.Generic;
using UnityEngine;

namespace Submodule.Missions
{
    public class MissionListUIPopup : MissionUIPopupBase
    {
        [SerializeField]
        MissionPreviewUI MissionPreviewUIPrefab;
        [SerializeField]
        Transform Container;
        
        public override void Show()
        {
            base.Show();
        }

        public void Setup(List<MissionData> missions)
        {
            foreach (var missionData in missions)
            {
                if (missionData.TryGetNextUncompletedMissionConditions(out var nextConditionsAtDifficulty) == false)
                    continue; // no more difficulties to completed for this mission.
                
                var uiPreview = Instantiate(MissionPreviewUIPrefab, Container);
                uiPreview.Setup(missionData, nextConditionsAtDifficulty);
                uiPreview.OnButtonPressed += UiPreviewOnOnButtonPressed;
            }
        }


        private void UiPreviewOnOnButtonPressed(MissionData missionData, MissionConditionsAtDifficulty conditionsAtDifficulty)
        {
            Close();
            var uiHandler = MissionManager.Instance.UIHandler;
            uiHandler.ShowMissionDetailsUI(missionData, conditionsAtDifficulty);
        }

        public override void Close()
        {
            base.Close();
        }
    }
}