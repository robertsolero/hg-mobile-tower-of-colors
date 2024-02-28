using System;
using System.Collections.Generic;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    static BoosterManager _instance;
    public static BoosterManager Instance //TODO improve this a simplified version of a Singleton
    {
        get
        {
            if (_instance == null)
            {
                if (!RemoteConfig.BOOL_MISSION_ENABLED)
                {
                    Debug.LogError($"Cannot access Mission Manager, RC BOOL_MISSION_ENABLED is {RemoteConfig.BOOL_MISSION_ENABLED}");
                    return null;
                }


                return _instance = FindObjectOfType<BoosterManager>();
            }

            return _instance;
        }
    }

    [SerializeField]
    private List<BoosterData> boosters = new List<BoosterData>();
    [SerializeField] 
    private BoosterUsageRequestUI boosterUsageRequestUIPrefab;
    [SerializeField] 
    private string uiContainerPath; // TODO Not ideal, but required for spawning 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        
        GameManager.OnNewManagerLoaded += OnNewManagerLoaded;
    }

    private void OnNewManagerLoaded(GameManager gameManager) => gameManager.OnAvailableBallAmountChanged += OnAvailableBallAmountChanged;

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
        var container = GameObject.Find(uiContainerPath);
        var ui = Instantiate(boosterUsageRequestUIPrefab, container.transform);
        ui.Setup(boosterData, usageCallback);
    }
}
