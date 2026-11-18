using UnityEngine;

public class RoadScroll : MonoBehaviour
{
    private float height;

    void Start()
    {
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {

        // Move the road down based on the player's speed
        transform.position += Vector3.down * PlayerCar.Speed * Time.deltaTime;

        if (transform.position.y <= -height)
            transform.position += Vector3.up * height * 2f;
    }
}