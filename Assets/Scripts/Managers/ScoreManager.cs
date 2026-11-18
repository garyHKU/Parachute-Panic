using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private float pointsPerDistance = 1f;
    [SerializeField] private TMP_Text scoreText;

    private int bonusPoints;

    public int Score => Mathf.FloorToInt(PlayerCar.Distance * pointsPerDistance) + bonusPoints;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // Update score text
        scoreText.text = "Score: " + Score;
    }

    public void AddPoints(int amount)
    {
        // Add bonus points
        bonusPoints += amount;
    }

    public void RemovePoints(int amount)
    {
        // Remove bonus points
        bonusPoints -= amount;
    }
}