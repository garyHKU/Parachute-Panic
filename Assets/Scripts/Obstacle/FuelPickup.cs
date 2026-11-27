using UnityEngine;

public class FuelPickup : Obstacle
{
    [SerializeField]
    private float amount = 40f;
    [SerializeField] private int catchPoints = 100;
    [SerializeField] private int missPenalty = 50;

    public override void OnHitPlayer(PlayerCar player)
    {
        // Add fuel to the player
        player.AddFuel(amount);

        // Add POINTS to the score (hopfully acts as falling parachute idea, giving points when caught)
        ScoreManager.Instance.AddBonusPoints(catchPoints);

        // Destroy pickup
        Destroy(gameObject);
    }

    protected override void OnMissed()
    {
        // Remove points when pickup is missed (also acts as falling parachute idea, losing points when not caught)
        ScoreManager.Instance.RemovePoints(missPenalty);
    }
}
