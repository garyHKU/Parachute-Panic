using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform[] lanes;

    [Header("Traffic")]
    public GameObject[] trafficPrefabs;
    public float trafficInterval = 1.5f;
    public float trafficGap = 4f;

    [Header("Fuel")]
    public GameObject fuelPrefab;
    public float fuelInterval = 8f;
    public float fuelGap = 3f;


    public float spawnInterval = 1.5f;

    float trafficTimer;
    float fuelTimer;
    float[] laneOpenAt;

    void Start()
    {
        laneOpenAt = new float[lanes.Length];
        fuelTimer = fuelInterval;
    }

    void Update()
    {
        trafficTimer -= Time.deltaTime;
        if (trafficTimer <= 0f)
        {
            GameObject prefab = trafficPrefabs[Random.Range(0, trafficPrefabs.Length)];
            if (TrySpawn(prefab, trafficGap))
                trafficTimer = trafficInterval;
        }

        fuelTimer -= Time.deltaTime;
        if (fuelTimer <= 0f)
        {
            if (TrySpawn(fuelPrefab, fuelGap))
                fuelTimer = fuelInterval;
        }
    }

    bool TrySpawn(GameObject prefab, float gap)
    {
        List<int> free = new List<int>();
        for (int i = 0; i < lanes.Length; i++)
            if (PlayerCar.Distance >= laneOpenAt[i])
                free.Add(i);

        if (free.Count == 0) return false; // all lanes closed, try again next frame

        int lane = free[Random.Range(0, free.Count)];
        float y = Camera.main.orthographicSize + 2f;
        Instantiate(prefab, new Vector3(lanes[lane].position.x, y, 0f), Quaternion.identity);
        laneOpenAt[lane] = PlayerCar.Distance + gap;
        return true;
    }
}