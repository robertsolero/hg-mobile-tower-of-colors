public interface IInventoriable
{
    public string ID { get; }
    public string UserdataInventoryKey { get; }
    public int GetAmountOnInventory();
    public void IncreaseAmountOnInventory(int amountToIncrease);
    public void DecreaseAmountOnInventory(int amountToDecrease);
}