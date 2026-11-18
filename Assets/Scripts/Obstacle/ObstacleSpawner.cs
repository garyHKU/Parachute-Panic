using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float spawnMargin = 2f; // how far above the screen things spawn

    // note for garon aka me: Damn I forgot all aobut the header in other scripts xD
    [Header("Traffic")]
    [SerializeField] private GameObject[] trafficPrefabs;
    [SerializeField] private float trafficSpacing = 3f; // "minimum" screen distance between two spawns
    [SerializeField] private float trafficGap = 4f;     // free space needed in the lane itself

    [Header("Fuel")]
    [SerializeField] private GameObject fuelPrefab;
    [SerializeField] private float fuelEveryDistance = 120f; // minimum distance between fuel pickups
    [SerializeField] private float fuelGap = 3f;             // free space needed in the lane itself

    private Transform lastTraffic;
    private float nextFuelDistance;

    private float SpawnY => Lanes.Instance.ScreenTop + spawnMargin;

    void Start()
    {
        nextFuelDistance = fuelEveryDistance;
    }

    void Update()
    {
        bool lastCarMovedOn = lastTraffic == null || lastTraffic.position.y < SpawnY - trafficSpacing;
        if (lastCarMovedOn)
        {
            GameObject prefab = trafficPrefabs[Random.Range(0, trafficPrefabs.Length)];
            GameObject spawned = TrySpawn(prefab, TrafficCanSpawn);
            if (spawned != null) lastTraffic = spawned.transform;
        }

        if (PlayerCar.Distance >= nextFuelDistance)
        {
            GameObject spawned = TrySpawn(fuelPrefab, FuelCanSpawn);
            if (spawned != null) nextFuelDistance = PlayerCar.Distance + fuelEveryDistance;
        }
    }

    // Check if the spawn area is free of obstacles
    private bool TrafficCanSpawn(int lane)
    {
        return !Lanes.Instance.HasAny<Obstacle>(lane, SpawnY - trafficGap, SpawnY + trafficGap);
    }

    // Check if the spawn area is free and no car on screen could drive over the fuel
    private bool FuelCanSpawn(int lane)
    {
        bool spawnAreaFree = !Lanes.Instance.HasAny<Obstacle>(lane, SpawnY - fuelGap, SpawnY + fuelGap);
        bool noCarOnScreen = !Lanes.Instance.HasAny<TrafficCar>(lane, Lanes.Instance.ScreenBottom, SpawnY);
        return spawnAreaFree && noCarOnScreen;
    }

    private GameObject TrySpawn(GameObject prefab, System.Func<int, bool> canSpawn)
    {
        List<int> freeLanes = new List<int>();
        for (int i = 0; i < Lanes.Instance.Count; i++)
        {
            if (canSpawn(i)) freeLanes.Add(i);
        }

        if (freeLanes.Count == 0) return null; // no free lane, try again next frame

        // Pick random free lane and spawn
        int lane = freeLanes[Random.Range(0, freeLanes.Count)];
        Vector3 spawnPosition = new Vector3(Lanes.Instance.GetX(lane), SpawnY, 0f);
        return Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}