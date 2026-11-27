using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public PlayerCar player;
    public TextMeshProUGUI livesText;   
    public Slider fuelBar;

    void Update()
    {
        // Update the HUD elements based on the player's current state
        livesText.text = "Lives: " + player.Lives;
        fuelBar.value = player.Fuel / player.FuelCapacity;
    }
}