using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Submodule.Missions
{
    public class MissionDataHandler : MonoBehaviour
    {
        [SerializeField]
        private List<MissionData> missionDataList;
        public List<MissionData> MissionDataList => missionDataList;

        public bool HasAnyUncompletedMission()
        {
            return MissionDataList.Any(mission => mission.TryGetNextUncompletedMissionConditions(out _));
        }
    }
}