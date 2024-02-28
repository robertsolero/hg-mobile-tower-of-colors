using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterUsageRequestUI : MonoBehaviour
{
    [SerializeField]
    private BoosterPreviewUI BoosterPreviewUI;

    [SerializeField] private TMP_Text description;
    [SerializeField] private Button useButton;
    [SerializeField] private Button closeButton;
    
    
    public void Setup(BoosterData boosterData, Action<bool> onComplete)
    {
        BoosterPreviewUI.Setup(boosterData.BoosterImage, boosterData.GetAmountOnInventory());
        description.text = string.Format(description.text, boosterData.Description);
        
        useButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        
        useButton.onClick.AddListener(()=> OnUseButtonPressed(boosterData, onComplete));
        closeButton.onClick.AddListener(()=> OnClosePressed(onComplete));
    }

    void OnUseButtonPressed(BoosterData boosterData, Action<bool> onComplete)
    {
        boosterData.DecreaseAmountOnInventory(1);
        boosterData.PerformBoosterAction();
        onComplete?.Invoke(false);
        Close();
    }

    void OnClosePressed(Action<bool> onComplete)
    {
        onComplete?.Invoke(false);
        Close();
    }
    
    void Close()
    {
        Destroy(gameObject);
    }
}