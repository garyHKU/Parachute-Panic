using UnityEngine;

public class FuelPickup : Obstacle
{
    [SerializeField]
    private float amount = 40f;

    public override void OnHitPlayer(PlayerCar player)
    {
        // Add fuel to the player and destroy the pickup
        player.AddFuel(amount);
        Destroy(gameObject);
    }
}
