
using UnityEngine;

public static class InventoryService
{
    static string GetInventoryKey(string key) => $"Inventory.{key}";
    public static int GetAmountOnInventory(IInventoriable item)
    {
        return PlayerPrefs.GetInt(GetInventoryKey(item.UserdataInventoryKey));
    }
    
    public static void IncreaseAmountOnInventory (IInventoriable item, int amountToIncrease)
    {
        var previous = GetAmountOnInventory(item); 
        PlayerPrefs.SetInt(GetInventoryKey(item.UserdataInventoryKey), previous + amountToIncrease);
    }
    
    public static void DecreaseAmountOnInventory (IInventoriable item, int amountToDecrease)
    {
        var previous = GetAmountOnInventory(item); 
        PlayerPrefs.SetInt(GetInventoryKey(item.UserdataInventoryKey), previous - amountToDecrease);
    }
}