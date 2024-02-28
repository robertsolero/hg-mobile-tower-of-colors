using System.Collections.Generic;
using UnityEngine;

namespace Submodule.Missions
{
    public class MissionDataHandler : MonoBehaviour
    {
        [SerializeField]
        private List<MissionData> missionDataList;
        public List<MissionData> MissionDataList => missionDataList;
    }
}