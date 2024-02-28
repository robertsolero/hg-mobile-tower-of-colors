using System;
using UnityEngine;

[Serializable]
public abstract class MissionReward : ScriptableObject
{
    public abstract Sprite RewardImage { get; }
    
    public abstract void AddRewardToInventory(int amount);
}