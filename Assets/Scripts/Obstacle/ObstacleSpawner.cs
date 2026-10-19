using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform[] lanes;
    public GameObject[] obstaclePrefabs;
    public float spawnInterval = 1.5f;

    float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;
        timer = spawnInterval;

        Transform lane = lanes[Random.Range(0, lanes.Length)];
        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        float y = Camera.main.orthographicSize + 2f; // just above the top of the screen
        Instantiate(prefab, new Vector3(lane.position.x, y, 0f), Quaternion.identity);
    }
}