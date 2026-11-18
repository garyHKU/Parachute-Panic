using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public static float Speed;
    public static float Distance;

    [Header("Speed")]
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float acceleration = 5f;

    [Header("Steering")]
    [SerializeField] private float minSpeedSteering = 3f;
    [SerializeField] private float maxSpeedSteering = 8f;
    [SerializeField] private float roadEdgeMargin = 1f; // extra space to keep the car on the road

    [Header("Lives and fuel")]
    public int lives = 3;
    public float fuelCapacity = 100f;
    public float fuel;
    [SerializeField] private float fuelDrainPerSecond = 5f;

    [Header("Audio")]
    [SerializeField] private AudioSource engineAudio;

    private SteerSprite steerSprite;

    void Start()
    {
        Speed = minSpeed;
        fuel = fuelCapacity;
        steerSprite = GetComponent<SteerSprite>();
    }

    void Update()
    {
        if (fuel <= 0f) return;

        // get inputs from the player
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // update speed and distance
        Speed += verticalInput * acceleration * Time.deltaTime;
        Speed = Mathf.Clamp(Speed, minSpeed, maxSpeed);
        Distance += Speed * Time.deltaTime;

        // steer faster at higher speeds
        float speedFraction = Mathf.InverseLerp(minSpeed, maxSpeed, Speed);
        float steering = Mathf.Lerp(minSpeedSteering, maxSpeedSteering, speedFraction);

        // move sideways and stay on the road
        float leftEdge = Lanes.Instance.GetX(0) - roadEdgeMargin;
        float rightEdge = Lanes.Instance.GetX(Lanes.Instance.Count - 1) + roadEdgeMargin;

        Vector3 position = transform.position;
        position.x += horizontalInput * steering * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, leftEdge, rightEdge);
        transform.position = position;

        steerSprite.Steer(horizontalInput);

        // drain fuel over time
        fuel -= fuelDrainPerSecond * Time.deltaTime;
        if (fuel <= 0f)
        {
            fuel = 0f;
            GameOver();
        }
    }

    public void AddFuel(float amount)
    {
        fuel = Mathf.Min(fuel + amount, fuelCapacity);
    }

    public void LoseLife()
    {
        lives--;
        if (lives <= 0) GameOver();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null) obstacle.OnHitPlayer(this);
    }

    private void GameOver()
    {
        engineAudio.Stop();
        GameManager.Instance.GameOver();
    }
}