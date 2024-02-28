using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Submodule.Missions
{
    public class MissionRewardUI : MonoBehaviour
    {
        public Image Image;
        public TMP_Text Amount;

        public void Setup(MissionReward reward, int amount)
        {
            Image.sprite = reward.RewardImage;
            Amount.text = amount.ToString();
        }
    }
}