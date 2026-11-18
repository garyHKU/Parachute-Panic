using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f; // destroy the explosion after x seconds

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // stay on the road instead of riding along with the player
        transform.position += Vector3.down * PlayerCar.Speed * Time.deltaTime;
    }
}