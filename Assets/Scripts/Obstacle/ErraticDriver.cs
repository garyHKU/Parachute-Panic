using UnityEngine;

public class ErraticDriver : TrafficCar
{

    // Driver behavior
    [SerializeField] private float minSwitchDelay = 0.5f;
    [SerializeField] private float maxSwitchDelay = 1f;
    [SerializeField] private float switchSpeed = 3f;
    [SerializeField] private float carClearance = 1.5f;

    private SteerSprite steerSprite;
    private int lane;
    private int direction = 1;
    private float targetX;
    private float switchTimer;

    protected override void Start()
    {
        base.Start();

        steerSprite = GetComponent<SteerSprite>();
        
        lane = Lanes.Instance.ClosestLane(transform.position.x);
        targetX = transform.position.x;

        if (Random.value < 0.5f)
        {
            direction = -1;
        }

        ResetTimer();
    }

    protected override void Update()
    {
        base.Update();

        // Move towards the target lane
        Vector3 position = transform.position;
        position.x = Mathf.MoveTowards(position.x, targetX, switchSpeed * Time.deltaTime);
        transform.position = position;

        // Steer the sprite
        float steer = Mathf.Clamp(targetX - position.x, -1f, 1f);
        steerSprite.Steer(steer);

        switchTimer -= Time.deltaTime;
        if (switchTimer <= 0f)
        {
            TrySwitch();
            ResetTimer();
        }
    }

    private void TrySwitch()
    {
        int nextLane = lane + direction;

        // If the next lane is out of bounds, reverse direction and try again
        if (nextLane < 0 || nextLane >= Lanes.Instance.Count)
        {
            direction = -direction;
            nextLane = lane + direction;
        }

        // If the next lane is safe, switch to it :)
        if (IsLaneSafe(nextLane))
        {
            lane = nextLane;
            targetX = Lanes.Instance.GetX(lane);
        }
    }

    private bool IsLaneSafe(int targetLane)
    {

        float y = transform.position.y;

        // Check if there are any cars or fuel in the lane we want to switch to
        bool carNextToUs = Lanes.Instance.HasAny<TrafficCar>(targetLane, y - carClearance, y + carClearance);
        bool fuelAhead = Lanes.Instance.HasAny<FuelPickup>(targetLane, y - carClearance, Lanes.Instance.ScreenTop);
        
        return !carNextToUs && !fuelAhead;
    }

    private void ResetTimer()
    {
        // Reset the switch timer with random delay
        switchTimer = Random.Range(minSwitchDelay, maxSwitchDelay);
    }
}