
using UnityEngine;

[CreateAssetMenu(fileName = "BoosterMissionReward", menuName = "Missions/BoosterMissionReward", order = 1)]
public class BoosterMissionReward : MissionReward
{
    [SerializeField] private BoosterData boosterData;
    public BoosterData BoosterData => boosterData;
    public override string RewardName => BoosterData.BoosterName;
    public override Sprite RewardImage => BoosterData.BoosterImage;
    public override void AddRewardToInventory(int amount)
    {
        boosterData.IncreaseAmountOnInventory(amount);
    }
}