using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{

    public static float Distance;

    [SerializeField]
    private string carName;
    [SerializeField]    
    private float minSpeed = 5f;
    [SerializeField] 
    private float maxSpeed = 15f;
    [SerializeField] 
    private float acceleration = 5f;

    // the amount of XP required to unlock this car not implemented yet, but will be used in the future
    public int xpToUnlock = 0;

    public int lives = 3;

    [SerializeField] 
    public float fuelCapacity = 100f;
    [SerializeField] 
    public float fuelDrainPerSecond = 5f;
    [SerializeField] 
    public float fuel;

    public static float Speed;

    [SerializeField]
    private  float steerSpeed = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = minSpeed;
        fuel = fuelCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if (fuel <= 0f) return;

        // get inputas from the player
        float vertical_input = Input.GetAxis("Vertical");
        float horizontal_input = Input.GetAxis("Horizontal");


        // update speed and distance
        Speed += vertical_input * acceleration * Time.deltaTime;
        Speed = Mathf.Clamp(Speed, minSpeed, maxSpeed);
        Distance += Speed * Time.deltaTime;


        // update position based on horizontal input
        transform.position = new Vector3(
            transform.position.x + horizontal_input * steerSpeed * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );


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
        fuel += amount;
        if (fuel > fuelCapacity)
            fuel = fuelCapacity;
    }

    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
            obstacle.OnHitPlayer(this);
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver();
    }
}