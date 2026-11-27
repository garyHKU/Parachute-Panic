using System;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using TMPro;


[Serializable]
public class Wave
{
    public float length = 500f;
    public float milestoneBonusPoints = 100f;
    public float trafficSpacing = 3f;
    [Range(0, 1)] public float erraticChance = 0f;
}

public class ProgressionManager : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private float showDuration = 2f;


    public static ProgressionManager Instance { get; private set; }
    [SerializeField] private List<Wave> waves = new();

    private int waveIndex;
    private float waveStartDistance;

    public Wave CurrentWave => waves[waveIndex];

    void Start()
    {
        waveText.gameObject.SetActive(false);
    }

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

        StopAllCoroutines();
        StartCoroutine(ShowWaveText());
    }

    private IEnumerator ShowWaveText()
    {
        waveText.text = $"Wave {waveIndex + 1}";
        waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(showDuration);
        waveText.gameObject.SetActive(false);
    }
}