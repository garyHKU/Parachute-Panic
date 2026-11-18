using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{

    // If a child class has a drive speed it can use this
    protected virtual float DriveSpeed => 0f;

    protected virtual void Update()
    {
        float relativeSpeed = PlayerCar.Speed - DriveSpeed;

        // Move the obstacle down the screen based on the player's speed
        transform.position += Vector3.down * relativeSpeed * Time.deltaTime;


        // If the obstacle goes bellow the screen, destroy it
        if (transform.position.y < -10f) { 
            OnMissed();
            Destroy(gameObject); 
        };

    }

    // This is for other scripts to implement
    public abstract void OnHitPlayer(PlayerCar player);

    // This is for child classes to implement if they want to do something they are missed (like losing points)
    protected virtual void OnMissed() {}
}
