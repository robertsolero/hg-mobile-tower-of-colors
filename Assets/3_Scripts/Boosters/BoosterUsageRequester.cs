using System;
using System.Collections.Generic;
using UnityEngine;

public class BoosterUsageRequester : MonoBehaviour
{
    [SerializeField]
    private List<BoosterData> boosters = new List<BoosterData>();
    [SerializeField] 
    private BoosterUsageRequestUI boosterUsageRequestUIPrefab;
    [SerializeField] 
    private Transform uiContainer;  

    public void Start()
    {
        if (RemoteConfig.BOOL_BOOSTERS_ENABLED)
            GameManager.Instance.OnAvailableBallAmountChanged += OnAvailableBallAmountChanged;
    }
    
    private void OnAvailableBallAmountChanged(int amountOfBallsAvailable)
    {
        var moreBallsBooster = boosters.Find(x => x.BoosterType == BoosterType.MoreBalls);
        if (moreBallsBooster == null)
            return;
        
        if (amountOfBallsAvailable == 1 && InventoryService.GetAmountOnInventory(moreBallsBooster) > 0)
        {
            ShowBoosterUsageRequestPopup(moreBallsBooster, null);
        }
    }

    void ShowBoosterUsageRequestPopup(BoosterData boosterData, Action<bool> usageCallback)
    {
        var ui = Instantiate(boosterUsageRequestUIPrefab, uiContainer);
        ui.Setup(boosterData, usageCallback);
    }
}
