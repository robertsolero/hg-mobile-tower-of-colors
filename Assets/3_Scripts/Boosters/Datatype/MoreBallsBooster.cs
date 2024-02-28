using UnityEngine;

[CreateAssetMenu(fileName = "TryAgainBooster", menuName = "Boosters/MoreBalls", order = 1)]
public class MoreBallsBooster : BoosterData
{
    [SerializeField] private int amountOfBallsToIncrease;

    public int AmountOfBallsToIncrease => amountOfBallsToIncrease;
    
    public override void PerformBoosterAction()
    {
        GameManager.Instance.IncreaseAvailableBallAmount(amountOfBallsToIncrease);
    }

    public override string Description => $"get {AmountOfBallsToIncrease} more balls";
}