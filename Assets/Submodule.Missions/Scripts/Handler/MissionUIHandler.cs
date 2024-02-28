using UnityEngine;
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
    }
}