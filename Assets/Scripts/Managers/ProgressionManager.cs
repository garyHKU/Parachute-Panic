using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    public float length = 500f;
    public float milestoneBonusPoints = 100f;
    public float trafficSpacing = 3f;
}

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance { get; private set; }
    [SerializeField] private List<Wave> waves = new();

    private int waveIndex;
    private float waveStartDistance;

    public Wave CurrentWave => waves[waveIndex];

    void Awake() => Instance = this;

    void Update()
    {
        float distanceInWave = PlayerCar.Distance - waveStartDistance;

        if (distanceInWave >= CurrentWave.length)
            NextWave();
    }

    void NextWave()
    {
        waveStartDistance = PlayerCar.Distance;

        ScoreManager.Instance.AddBonusPoints((int)CurrentWave.milestoneBonusPoints);

        if (waveIndex < waves.Count - 1)
            waveIndex++;

        Debug.Log($"Wave {waveIndex + 1} started at distance {PlayerCar.Distance}");
    }
}