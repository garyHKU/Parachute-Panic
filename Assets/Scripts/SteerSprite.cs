using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SteerSprite : MonoBehaviour
{

    // Far left = 0, straight = 2, far right = 4
    [SerializeField] private Sprite[] steerFrames;
    [SerializeField] private float tiltResponse = 6f;

    private SpriteRenderer spriteRenderer;
    private float tilt;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Call every frame. steer: -1 = full left, 0 = straight, 1 = full right
    public void Steer(float steer)
    {

        // Smoothly move the tilt towards the target steer value
        tilt = Mathf.MoveTowards(tilt, steer, tiltResponse * Time.deltaTime);
        int frame = Mathf.RoundToInt((tilt + 1f) / 2f * (steerFrames.Length - 1));

        spriteRenderer.sprite = steerFrames[frame];
    }
}