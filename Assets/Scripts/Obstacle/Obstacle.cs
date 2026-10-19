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
        transform.position += Vector3.down * PlayerCar.Speed * Time.deltaTime;
        if (transform.position.y < -10f) Destroy(gameObject);
    }

    public abstract void OnHitPlayer(PlayerCar player);

}
