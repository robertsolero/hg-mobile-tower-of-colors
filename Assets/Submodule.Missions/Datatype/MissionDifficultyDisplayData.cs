using UnityEngine;

namespace Submodule.Missions
{
    [CreateAssetMenu(fileName = "MissionDifficultyDisplayData", menuName = "Missions/MissionDifficultyDisplayData", order = 1)]
    public class MissionDifficultyDisplayData : ScriptableObject
    {
        [SerializeField]
        public MissionDifficultyType difficultyType;
        public MissionDifficultyType DifficultyType => difficultyType;

        [SerializeField]
        private Color backgroundDifficultyColor;
        public Color BackgroundDifficultyColor => backgroundDifficultyColor;

        [SerializeField]
        private string difficultyName;
        public string DifficultyName => difficultyName;
    }
}