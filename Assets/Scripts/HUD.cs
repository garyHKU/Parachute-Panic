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
        livesText.text = "Lives: " + player.lives;
        fuelBar.value = player.fuel / player.fuelCapacity;
    }
}