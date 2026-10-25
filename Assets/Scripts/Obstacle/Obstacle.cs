using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move the obstacle down the screen based on the player's speed
        transform.position += Vector3.down * PlayerCar.Speed * Time.deltaTime;
        if (transform.position.y < -10f) Destroy(gameObject);
    }

    // This method is called when the obstacle collides with the player
    public abstract void OnHitPlayer(PlayerCar player);

}
