using UnityEngine;
using UnityEngine.UI;

public abstract class BoosterData : ScriptableObject, IInventoriable
{
    [SerializeField] BoosterType boosterType;
    public BoosterType BoosterType => boosterType;
    
    [SerializeField] private string boosterName;
    public string BoosterName => boosterName;


    [SerializeField] private Sprite boosterImage;
    public Sprite BoosterImage => boosterImage;

    public abstract void PerformBoosterAction();
    public abstract string Description { get; } 
    public string ID => BoosterType.ToString();
    public string UserdataInventoryKey => $"Boosters.{ID}";

    public int GetAmountOnInventory() => InventoryService.GetAmountOnInventory(this);

    public void IncreaseAmountOnInventory(int amountToIncrease)
    {
        InventoryService.IncreaseAmountOnInventory(this, amountToIncrease);
    }

    public void DecreaseAmountOnInventory(int amountToDecrease)
    {
        InventoryService.DecreaseAmountOnInventory(this, amountToDecrease);
    }
}
