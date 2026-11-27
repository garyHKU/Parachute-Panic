using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public static float Speed { get; private set; }
    public static float Distance { get; private set; }

    [Header("Speed")]
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float acceleration = 5f;

    [Header("Steering")]
    [SerializeField] private float minSpeedSteering = 3f;
    [SerializeField] private float maxSpeedSteering = 8f;
    [SerializeField] private float roadEdgeMargin = 1f; // extra space to keep the car on the road

    [Header("Lives and fuel")]
    [SerializeField] private int lives = 3;
    [SerializeField] private float fuelCapacity = 150f;

    public int Lives => lives;
    public float FuelCapacity => fuelCapacity;
    public float Fuel { get; private set; }

    [SerializeField] private float fuelDrainPerSecond = 5f;

    [Header("Audio")]
    [SerializeField] private AudioSource engineAudio;

    private SteerSprite steerSprite;

    private void Start()
    {
        Speed = minSpeed;
        Fuel = FuelCapacity;
        steerSprite = GetComponent<SteerSprite>();
    }

    private void Update()
    {
        if (Fuel <= 0f) return;
        float _verticalInput = Input.GetAxis("Vertical");
        float _horizontalInput = Input.GetAxis("Horizontal");

        HandleSpeed(_verticalInput);
        HandleSteering(_horizontalInput);
        HandleFuel();
    }

    private void HandleSpeed(float verticalInput)
    {

        // update speed and distance
        Speed += verticalInput * acceleration * Time.deltaTime;
        Speed = Mathf.Clamp(Speed, minSpeed, maxSpeed);
        Distance += Speed * Time.deltaTime;
    }

    private void HandleSteering(float horizontalInput)
    {
        // steer faster at higher speeds
        float _speedFraction = Mathf.InverseLerp(minSpeed, maxSpeed, Speed);
        float _steering = Mathf.Lerp(minSpeedSteering, maxSpeedSteering, _speedFraction);
        // move sideways and stay on the road
        float _leftEdge = Lanes.Instance.GetX(0) - roadEdgeMargin;
        float _rightEdge = Lanes.Instance.GetX(Lanes.Instance.Count - 1) + roadEdgeMargin;
        Vector3 _position = transform.position;
        _position.x += horizontalInput * _steering * Time.deltaTime;
        _position.x = Mathf.Clamp(_position.x, _leftEdge, _rightEdge);
        transform.position = _position;
        steerSprite.Steer(horizontalInput);
    }

    private void HandleFuel()
    {
        // drain fuel over time
        Fuel -= fuelDrainPerSecond * Time.deltaTime;
        if (Fuel <= 0f)
        {
            Fuel = 0f;
            GameOver();
        }
    }

    public void AddFuel(float amount)
    {
        Fuel = Mathf.Min(Fuel + amount, FuelCapacity);
    }

    public void LoseLife()
    {
        lives--;
        if (Lives <= 0) GameOver();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null) obstacle.OnHitPlayer(this);
    }

    public static void ResetDistance()
    {
        Distance = 0f;
    }

    private void GameOver()
    {
        engineAudio.Stop();
        GameManager.Instance.GameOver();
    }
}