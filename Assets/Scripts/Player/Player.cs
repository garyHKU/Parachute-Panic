using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public string carName;
    public float minSpeed = 5f;
    public float maxSpeed = 15f;
    public float acceleration = 5f;
    public int xpToUnlock = 0;

    public int lives = 3;

    public static float Speed;

    [SerializeField]
    private  float steerSpeed = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = minSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical_input = Input.GetAxis("Vertical"); // up/down arrows or W/S
        float horizontal_input = Input.GetAxis("Horizontal");
        Speed += vertical_input * acceleration * Time.deltaTime;
        Speed = Mathf.Clamp(Speed, minSpeed, maxSpeed);

        transform.position = new Vector3(
            transform.position.x + horizontal_input * steerSpeed * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );

    }

    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            // Game Over logic here
            Debug.Log("Game Over!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
            obstacle.OnHitPlayer(this);
    }
}
