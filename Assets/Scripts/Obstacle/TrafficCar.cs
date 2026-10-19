using UnityEngine;

public class TrafficCar : Obstacle
{
    public Sprite[] sprites;
    public GameObject explosionPrefab;

    void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Length)];
        GetComponent<BoxCollider2D>().size = sr.sprite.bounds.size;
    }

    public override void OnHitPlayer(PlayerCar player)
    {
        player.LoseLife();
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}