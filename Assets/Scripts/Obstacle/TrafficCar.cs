using UnityEngine;

public class TrafficCar : Obstacle
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float driveSpeed = 4f;
    [SerializeField] private GameObject explosionPrefab;


    protected virtual void Start()
    {
        var sr = GetComponent<SpriteRenderer>();

        // Randomly select a sprite for the traffic car (this is if it has multiple sprites, which can be different colors)
        if (sprites.Length > 0)
            sr.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    protected override float DriveSpeed => driveSpeed;

    public override void OnHitPlayer(PlayerCar player)
    {
        // Remove a life form the player
        player.LoseLife();

        // go boom
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}