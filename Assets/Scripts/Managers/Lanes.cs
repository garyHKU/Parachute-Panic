using UnityEngine;

public class Lanes : MonoBehaviour
{
    public static Lanes Instance { get; private set; }

    [SerializeField] private Transform[] laneMarkers;

    private float laneWidth;

    public int Count => laneMarkers.Length;

    public float ScreenTop
    {
        get
        {
            // Calculate the top of the screen in world coordinates
            float cameraY = Camera.main.transform.position.y;
            float halfScreenHeight = Camera.main.orthographicSize;
            return cameraY + halfScreenHeight;
        }
    }

    public float ScreenBottom
    {
        get
        {
            // Calculate the bottom of the screen in world coordinates
            float cameraY = Camera.main.transform.position.y;
            float halfScreenHeight = Camera.main.orthographicSize;
            return cameraY - halfScreenHeight;
        }
    }

    void Awake()
    {
        Instance = this;

        // Calculate the width of a lane based on the positions of the lane markers
        float firstLaneX = laneMarkers[0].position.x;
        float secondLaneX = laneMarkers[1].position.x;
        laneWidth = Mathf.Abs(secondLaneX - firstLaneX);
    }

    public float GetX(int lane)
    {
        return laneMarkers[lane].position.x;
    }

    public int ClosestLane(float x)
    {
        int closestLane = 0;
        float closestDistance = Mathf.Abs(GetX(0) - x);

        for (int lane = 1; lane < Count; lane++)
        {
            float distance = Mathf.Abs(GetX(lane) - x);
            if (distance < closestDistance)
            {
                closestLane = lane;
                closestDistance = distance;
            }
        }

        return closestLane;
    }

    public bool HasAny<T>(int lane, float minY, float maxY) where T : Obstacle
    {
        
        float boxCenterY = (minY + maxY) / 2f;
        float boxHeight = maxY - minY;
        float boxWidth = laneWidth * 0.5f;

        // Create a box collider to check for obstacles in the lane
        Vector2 boxCenter = new Vector2(GetX(lane), boxCenterY);
        Vector2 boxSize = new Vector2(boxWidth, boxHeight);

        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f);

        // Check if any of the hits are type T
        foreach (Collider2D hit in hits)
        {
            T found = hit.GetComponent<T>();
            if (found != null)
            {
                return true;
            }
        }

        return false;
    }
}